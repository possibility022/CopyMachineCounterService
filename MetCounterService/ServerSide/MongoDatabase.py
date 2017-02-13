from Parser import DataParser
from pymongo import MongoClient
import logging
import settings

settings.init()


class MongoTB:

    def __init__(self):
        workfolder = settings.workfolder

        self.serverip = settings.MongoDatabaseAddress
        self.serverport = 27017
        self.database_name = 'copyinfo'
        self.machine_records = 'machine_records'
        self.full_counter = 'full_counter'
        self.full_serial = 'full_serial'
        self.email_binary = 'emails_binary'
        self.email_parsed_success = 'emails_sucess'
        self.email_parsed_faild = 'emails_faild'
        self.email_parsed_passed = 'emails_passed'

        self.decoded_dir = workfolder + '/decoded'

        self.client = MongoClient(self.serverip, self.serverport)
        self.db = self.client[self.database_name]
        self.records = self.db[self.machine_records]
        self.countersdata = self.db[self.full_counter]
        self.serialdata = self.db[self.full_serial]
        self.email_binary_db = self.db[self.email_binary]
        self.email_parsed_success_db = self.db[self.email_parsed_success]
        self.email_parsed_faild_db = self.db[self.email_parsed_faild]
        self.email_parsed_passed_db = self.db[self.email_parsed_passed]

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
            # Wyciagam z [device] dane: [full_counter], [full_serialnumber]
            # Zapisuje je w osobnych kolekcjach
            # wartości w [full_counter], [full_serialnumber] w [device]
            # zamieniam na ObjectID zapisanych nowych dokumentow
            cdata_id = self.countersdata.insert_one({"full_counter": printer_data['full_counter']}).inserted_id
            sdata_id = self.serialdata.insert_one(
                {"full_serialnumber": printer_data['full_serialnumber']}).inserted_id
            printer_data['full_counter'] = cdata_id
            printer_data['full_serialnumber'] = sdata_id

            id = self.records.insert_one(printer_data).inserted_id
            logging.info('Zapisalem nowy dokument. {}'.format(id))
            return True
        except SyntaxError:
            return False

    def count(self):
        logging.info('Count: records: {}'.format(self.records.count()))
        logging.info('Count: countersdata: {}'.format(self.countersdata.count()))
        logging.info('Count: serialdata: {}'.format(self.serialdata.count()))

    def insert_email(self, mail):
        inserted_id = self.email_binary_db.insert_one(mail).inserted_id
        logging.info('Inserted email to binary: {}'.format(inserted_id))

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
        

