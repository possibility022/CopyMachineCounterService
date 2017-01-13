# from Parser import DataParser
# import os
#
# decoded_dir = 'V:\\decoded'
# files = os.listdir(decoded_dir)
#
# for file in files:
#     f = open(decoded_dir + '/' + file)
#     data = f.read()
#     device = DataParser(data)
#     for col in device.printer_data:
#         print(col, device.printer_data[col])
# from Email import XMLLoader
# from Email import EmailParser
# xml = XMLLoader()
# print(xml.get_all_signature())
#
# email = EmailParser()
# emails = email.get_emails_from_mongo()
# for e in emails:
#     # n = email.get_signature_number(e)
#     # print('Signature:', n)
#     data = email.parse_email_to_device_data(e)
#
# from MongoDatabase import MongoTB
#
# mongo = MongoTB()
#
# emails = mongo.get_emails('fail')
# print(emails.count())
# for e in emails:
#     printer_data = email.parse_email_to_device_data(e)
#     if printer_data is not None:
#         print(printer_data)
#         print(mongo.import_to_database(printer_data))
#import logging
#from time import sleep
#from Email import EmailParser
#from MongoDatabase import MongoTB

#mongo = MongoTB()

#logging.basicConfig(filename='deamon.log', level=logging.DEBUG, format='%(asctime)s %(message)s', datefmt='%d/%m/%Y %H:%M:%S %p')

#logging.info('parse_loop_email started')
#while True:
#    mailbox = EmailParser()
#    ids = mailbox.get_emails_id()
#    for i in range(len(ids)):
#        email = mailbox.check_email_parsed(ids[i])
#        if email is None:
#            logging.info('Nie znalazlem maila. Pobieram wiadomosc.')
#            mail = mailbox.get_email_pop3(i + 1)
#            logging.debug('Pobralem wiadomosc')
#            data = mailbox.parse_email_to_device_data(mail)
#            if data is not None:
#                mongo.import_to_database(data)
#        else:
#            logging.info('Mail znaleziony, omijam:', email['_id'])
#    mailbox.close()
#    sleep(30 * 60)
from datetime import datetime as DATETIME

def check_signature(doc):
    if 'email_info' in doc.keys():
        return True
    else:
        return False
        #print(doc)

from pymongo import MongoClient
from MongoDatabase import MongoTB
from bson.objectid import ObjectId

import settings

settings.init()

mongotb = MongoTB()

serverip = settings.MongoDatabaseAddress
serverport = 27017
database_name = 'copyinfo'
machine_records = 'machine_records'
full_counter = 'full_counter'
full_serial = 'full_serial'
email_binary = 'emails_binary'
email_parsed_success = 'emails_sucess'
email_parsed_faild = 'emails_faild'
email_parsed_passed = 'emails_passed'


client = MongoClient(serverip, serverport)
db = client[database_name]
records = db[machine_records]
countersdata = db[full_counter]
serialdata = db[full_serial]
email_binary_db = db[email_binary]
email_parsed_success_db = db[email_parsed_success]
email_parsed_faild_db = db[email_parsed_faild]
email_parsed_passed_db = db[email_parsed_passed]

cursor = records.find()

from Email import EmailParser
mailbox = EmailParser()

f = open('c:\\Tom\\log.log','w')
f.write('test')
for document in cursor:
    if check_signature(document):
        email = mongotb.get_email(document['email_info'])
        if email['_id'] == b'+OK 188 000049a3513ee34f':
            print('toon')
        data = mailbox.parse_email_to_device_data(email)
        if data['tonerlevel_c'] is not None:
            data['_id'] = document['_id']
            records.update_one(data, False)
            f.write(data['_id'])
        print('test')
f.close()


        #'_id': b'+OK 161 00004971513ee34f
#doc = records.delete_one({"_id": ObjectId('58787ee50cdde71118038bc7')})
#doc = records.delete_one({"_id": ObjectId('58776353261251066b778098')})
#doc = records.delete_one({"_id": ObjectId('58776353261251066b77809b')})
#doc = records.delete_one({"_id": ObjectId('58776353261251066b77809e')})
#doc = records.delete_one({"_id": ObjectId('58776353261251066b7780a1')})


from Email import EmailParser

#ids = mailbox.get_emails_id()
#for i in range(len(ids)):
#    email = mailbox.check_email_parsed(ids[i])
#    if email is None:
#        data = None
#        logging.info('Nie znalazlem maila. Pobieram wiadomosc.')
#        mail = mailbox.get_email_pop3(i + 1)
#        logging.debug('Pobralem wiadomosc')
#        data = mailbox.parse_email_to_device_data(mail)
#        if data is not None:
#            mongo.import_to_database(data)
#    else:
#        logging.info('Mail znaleziony, omijam i usuwam: %s', email['_id'])
#        mailbox.del_email(i + 1)
#mailbox.close()
#sleep(5 * 60)

#data = mailbox.parse_email_to_device_data(mail)

#from MongoDatabase import MongoTB
#mongo = MongoTB()

#filepathh = 'C:\\Tom\\c#\\test\\messages_20170110-1144330'

#from Parser import DataParser

#reader = open(filepathh)
#data = reader.read()
#reader.close()
#device = DataParser(data)
#sucess = mongo.import_to_database(device)
#print('stop')