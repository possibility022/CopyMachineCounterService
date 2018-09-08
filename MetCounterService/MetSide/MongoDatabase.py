from Parser import DataParser
from pymongo import MongoClient
from pymongo import errors
from Service.Exceptions.TBExceptions import ServerException
import logging
import settings

settings.init()


class MongoTB:

    def __init__(self):
        workfolder = settings.workfolder

        self.serverip = settings.LocalMongoDatabaseAddress
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
        self.email_toparse = 'emails_toparse'
        self.machine_records_other = 'machine_records_other'

        self.client = MongoClient(self.serverip, self.serverport)
        self.db = self.client[self.database_name]
        if settings.AuthentiactionOn:
            self.db.authenticate('***REMOVED***', '***REMOVED***--_][')
        self.records = self.db[self.machine_records]
        self.countersdata = self.db[self.full_counter]
        self.serialdata = self.db[self.full_serial]
        self.email_binary_db = self.db[self.email_binary]
        self.email_parsed_success_db = self.db[self.email_parsed_success]
        self.email_parsed_faild_db = self.db[self.email_parsed_faild]
        self.email_parsed_passed_db = self.db[self.email_parsed_passed]
        self.email_suspect_db = self.db[self.email_suspect]
        self.email_toparse_db = self.db[self.email_toparse]
        self.records_other = self.db[self.machine_records_other]

        self.empty_full_counter_ID = self.countersdata.find_one({'full_counter':'ParsedFromEmail'})
        self.empty_full_serial_ID = self.serialdata.find_one({'full_serialnumber':'ParsedFromEmail'})

        if self.empty_full_counter_ID is None:
            self.empty_full_counter_ID = self.countersdata.find_one({'full_counter':'ParsedFromEmail'})
        if self.empty_full_serial_ID is None:
            self.empty_full_serial_ID = self.serialdata.find_one({'full_serialnumber':'ParsedFromEmail'})

        # Serwer Globalny
        
        self.global_serverip = settings.GlobalMongoDatabaseAddress
        self.global_serverport = 2772
        self.global_database = 'copyinfo'
        self.global_fullrecorddata = 'full_data'
        self.global_fullrecorddata_faild = 'full_data_faild'
        self.global_other = 'other'
        
        self.global_client = MongoClient(self.global_serverip, self.global_serverport)
        self.global_db = self.global_client[self.global_database]
        self.global_db.authenticate('***REMOVED***', '***REMOVED***#121#')
        self.global_fulldata = self.global_db[self.global_fullrecorddata]
        self.global_fulldata_faild = self.global_db[self.global_fullrecorddata_faild]
        self.global_other_db = self.global_db[self.global_other]

    def global_get_fulldata(self, readonly = False):
        records = self.global_fulldata.find()
        to_return = []
        for rec in records:
            try:
                to_return.append(rec['data'])
            except KeyError:
                logging.warning('obiekt rec nie posiada klucza data, to nie powinno miec miesjca')

        if (not readonly):
            records.rewind()
            for rec in records:
                try:
                    self.global_fulldata.delete_one(rec)
                except Exception as ex:
                    logging.warning('Problem z usuwaniem z kolekcji FullData %s', ex)
        return to_return

    def global_import_fulldatafaild(self, data):
        data = {'data':data}
        self.global_fulldata_faild.insert_one(data)

    def global_get_mactoweb(self):
        try:
            document = self.global_other_db.find_one({'key':'mactoweb'})
            if 'data' in document.keys:
                return document['data']
        except Exception as ex:
            logging.error('Błąd w pobieraniu danych XML mactoweb')
            logging.exception('ERROR!')
        return None

    def global_get_emailparser(self):
        try:
            document = self.global_other_db.find_one({'key':'emailparser'})
            if 'data' in document.keys:
                return document['data']
        except Exception as Ex:
            logging.error('Błąd w pobieraniu danych XML emailparser')
            logging.exception('ERROR!')
        return None

    def import_to_database(self, device):

        if device is None:
            return False

        if isinstance(device, DataParser):
            printer_data = device.printer_data
        elif isinstance(device, dict):
            printer_data = device
        else:
            raise ServerException('The inserting data is not dict or DataParser')

        #if device.exception_in_parsing:
        #    return False
        if not printer_data['parsed']:
            return False

        if printer_data['serial_number'] is not None:
            if len(printer_data['serial_number']) == 0:
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
        except SyntaxError as se:
            logging.exception('Dziwny blad')
            return False

    def get_destination(self, printer_data):
        record_datetime = printer_data['datetime']
        if record_datetime.day < 6 or record_datetime.day > 28 or record_datetime.weekday() == 0:
            return self.records
        else:
            return self.records_other

    def insert_email_to_queue(self, email):
        self.email_toparse_db.insert_one(email)

    def get_emails_to_parse(self):
        emails = []
        for email in self.email_toparse_db.find():
            emails.append(email)
        return emails

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
        email = None

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
