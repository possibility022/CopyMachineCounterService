from Parser import DataParser
from pymongo import MongoClient
from pymongo import errors
from TBExceptions import ServerException
import logging
import settings

settings.init()


class MongoTB:

    def __init__(self):
        workfolder = settings.workfolder

        self.serverip = settings.MongoDatabaseAddress
        self.serverport = 2772
        self.database_name = 'copyinfo'
        self.machine_records = 'machine_records'
        self.full_counter = 'full_counter'
        self.full_serial = 'full_serial'
        self.email_binary = 'emails_binary'
        self.email_parsed_success = 'emails_sucess'
        self.email_parsed_faild = 'emails_faild'
        self.email_parsed_passed = 'emails_passed'
        self.email_suspect = 'emails_suspect'
        self.machine_records_other = 'machine_records_other'

        self.decoded_dir = workfolder + '/decoded'

        self.client = MongoClient(self.serverip, self.serverport)
        self.db = self.client[self.database_name]
        self.db.authenticate('***REMOVED***', '***REMOVED***#121#')
        self.records = self.db[self.machine_records]
        self.countersdata = self.db[self.full_counter]
        self.serialdata = self.db[self.full_serial]
        self.email_binary_db = self.db[self.email_binary]
        self.email_parsed_success_db = self.db[self.email_parsed_success]
        self.email_parsed_faild_db = self.db[self.email_parsed_faild]
        self.email_parsed_passed_db = self.db[self.email_parsed_passed]
        self.email_suspect_db = self.db[self.email_suspect]
        self.records_other = self.db[self.machine_records_other]

        self.empty_full_counter_ID = self.countersdata.find_one({'full_counter':'ParsedFromEmail'})
        self.empty_full_serial_ID = self.serialdata.find_one({'full_serialnumber':'ParsedFromEmail'})

    def import_to_database(self, device):
        if isinstance(device, DataParser):
            printer_data = device.printer_data
        elif isinstance(device, dict):
            printer_data = device
        else:
            raise Exception('The inserting data is not dict or DataParser')

        #if device.exception_in_parsing:
        #    return False
        if not printer_data['parsed']:
            return False

        printer_data.pop('parsed')

        try:
            if printer_data['parsed_by_email']:
                # Jeśli dane zostały wyciągnięte z maila to full counter i full serial wskazują na istniejące już pole.
                printer_data['full_counter'] = self.empty_full_counter_ID['_id']
                printer_data['full_serialnumber'] = self.empty_full_serial_ID['_id']
            else:
                # Wyciagam z [device] dane: [full_counter], [full_serialnumber]
                # Zapisuje je w osobnych kolekcjach
                # wartości w [full_counter], [full_serialnumber] w [device]
                # zamieniam na ObjectID zapisanych nowych dokumentow
                cdata_id = self.countersdata.insert_one({"full_counter": printer_data['full_counter']}).inserted_id
                sdata_id = self.serialdata.insert_one({"full_serialnumber": printer_data['full_serialnumber']}).inserted_id
                printer_data['full_counter'] = cdata_id
                printer_data['full_serialnumber'] = sdata_id

            id = self.get_destination(printer_data).insert_one(printer_data).inserted_id
            
            logging.info('Zapisalem nowy dokument. {}'.format(id))
            return True
        except SyntaxError:
            return False

    def get_destination(self, printer_data):
        record_datetime = printer_data['datetime']
        if record_datetime.day < 6 or record_datetime.day > 28 or record_datetime.weekday() == 0:
            return self.records
        else:
            return self.records_other


    def count(self):
        logging.info('Count: records: {}'.format(self.records.count()))
        logging.info('Count: countersdata: {}'.format(self.countersdata.count()))
        logging.info('Count: serialdata: {}'.format(self.serialdata.count()))

    def insert_email(self, mail):
        try:
            inserted_id = self.email_binary_db.insert_one(mail).inserted_id
            logging.info('Inserted email to binary: {}'.format(inserted_id))
        except errors.DuplicateKeyError as e:
            logging.info('Duplicate Key Error in inser_email(mail).')

    def get_emails(self, source='binary'):
        src = None
        if source == 'binary':
            src = self.email_binary_db
        elif source == 'passed':
            src = self.email_parsed_passed_db
        elif source == 'fail':
            src = self.email_parsed_faild_db
        elif source == 'sucess':
            src = self.email_parsed_success_db

        return src.find()

    def check_id_parsed(self, id_):
        email = self.email_parsed_faild_db.find_one({'_id': id_})
        if email is not None:
            return email
        email = self.email_parsed_passed_db.find_one({'_id': id_})
        if email is not None:
            return email
        email = self.email_parsed_success_db.find_one({'_id': id_})
        if email is not None:
            return email
        return None

    def move_mail_parsed(self, destination, mail_id):

        if isinstance(mail_id, dict):
            _id = mail_id['_id']

        if destination == 'passed':
            dst = self.email_parsed_passed_db
        elif destination == 'fail':
            dst = self.email_parsed_faild_db
        elif destination == 'sucess':
            dst = self.email_parsed_success_db

        if dst is None:
            raise Exception('Błędnie użyłeś move_email_to(destination, mail_id) - miejsce docelowe nie znalezione')

        try:
            dst.insert_one({'_id': _id})
        except Exception as e:
            logging.error('Ops, somethink wrong with inserting email.')

    def get_email(self, mail_id):
        src = self.email_binary_db

        email = src.find_one({'_id': mail_id})

        if email is None:
            src = self.email_parsed_passed_db
            email = src.find_one({'_id': mail_id})
            if email is None:
                src = self.email_parsed_success
                email = src.find_one({'_id': mail_id})
                if email is None:
                    src = self.email_parsed_faild
                    email = src.find_one({'_id': mail_id})
        
        return email

    def insert_email_to_suspect(self, mail_id_in_bytes):
        if isinstance(mail_id_in_bytes, bytearray) or isinstance(mail_id_in_bytes, bytes):
            self.email_suspect_db.insert_one({'_id' : mail_id_in_bytes})
        else:
            raise ServerException('Probowano dodać mail do listy podejrzanych ale parametr nie jest ani bytearray ani bytes')

    def check_email_is_on_suspect_list(self, mail_id_in_bytes):
        if isinstance(mail_id_in_bytes, bytearray) or isinstance(mail_id_in_bytes, bytes):
            email = self.email_suspect_db.find_one({'_id' : mail_id_in_bytes})
            if email is not None:
                return True
            else:
                return False
        else:
            raise ServerException('Probowano sprawdzić mail na liście podejrzanych ale parametr nie jest ani bytearray ani bytes')
        
    #def clear_database(self):
    #    cdata_id = self.countersdata.insert_one({"full_counter": 'parsedfromemail'}).inserted_id
    #    sdata_id = self.serialdata.insert_one({"full_serialnumber": 'parsedfromemail'}).inserted_id

    #    i = 0

    #    all_record = self.records_other.find()
    #    for rec in all_record:
    #        if 'parsed from email' in rec['description']:
    #            self.countersdata.delete_one({'_id': rec['full_counter']})
    #            self.serialdata.delete_one({'_id': rec['full_serialnumber']})
    #            rec['full_counter'] = cdata_id
    #            rec['full_serialnumber'] = sdata_id
    #            rec['parsed_by_email'] = True
    #            print('mail')
    #        else:
    #            print('usluga')
    #            rec['parsed_by_email'] = False
    #        i += 1
    #        print(i)
    #        self.records_other.replace_one({'_id': rec['_id']}, rec, False)
