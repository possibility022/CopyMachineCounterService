import logging
import json

from ThreadEngine import Engine
import pickle

from Parser import DataParser
import MongoDatabase

import threading
import time
import sys

import os
import traceback

import Parser
import MongoDatabase
import Email

import logging

import settings

from MongoDatabase import MongoTB
from Parser import DataParser
from Email import EmailParser

from Service.Parsing.EmailParsing import EmailParserV2
from Service.SQL.SqlDatabase import TBSQL
from Service.Email.EmailClient import EmailPop3Client
from Service.Parsing.XmlParsing import XMLLoader
from Service.Parsing.XmlParsing import XMLLoaderForHTML
from Service.Exceptions.TBExceptions import ServerException
from Service.Parsing.HTMLParser import HTMLParser

from datetime import datetime, timedelta

import traceback


def testFileConvert(filepath, mongo_database):
    try:
        f = open(filepath, 'r').read()

        device = DataParser(f)
        sucess = mongo_database.import_to_database(device)
        if not sucess:
            mongo_database.global_import_fulldatafaild(f)
    except:
        logging.error('Krytyczny blad w przetwarzaniu zdalnych raportow HTML')
        logging.exception('Error!')



def testEmailParsing():
    sqlDatabase = TBSQL()
    try:
        mails = []
        mailbox = EmailParser()

        sqlDatabase.Connect()

        ids = numMessages = mailbox.get_emails_id()             # Pobranie wszystkich id z serwera pocztowego
        for i in range(1, len(ids)):                            # Dla kazdego id na serwerze
            message = mailbox.get_email_pop3(i)                 # Pobieram wiadomosc w formacie {'_id': bytes_id, 'mail':tablica_bajtow_wiadomosc }
            if message is not None:                             # Jesli wiadomosc pobrano to
                mails.append(message)

                binaryEmail = pickle.dumps(message)
                sqlDatabase.ImportEmailToQueue(binaryEmail)

        # Working with mongo database
        queue = mails

        for mail in queue:
            data = None
            try:
                data = mailbox.parse_email_to_device_data(mail)         # Tutaj parsuje do odpowiednich danych
                print(data)
                data = None
                
            except Exception as ex:
                logging.debug('Jest problem z mailem mail: %s', mail)
                logging.exception('Error!')

        

        mailbox.close()
    except Exception as e:
        logging.error('P1 - Błąd krytyczny w pętli parsowania email. Pętla została przerwana. %s', e)
        logging.error('Error on line {}'.format(sys.exc_info()[-1].tb_lineno))
        logging.exception('Error!')
        logging.error(traceback.format_exc())
        logging.error('Zapisano')

    try:
        sqlDatabase.Connect()
        emailsToParse = sqlDatabase.GetEmailsToParse()
        for binaryEmail in emailsToParse:
            email = pickle.loads(binaryEmail.Content)
            
            data = None
            try:
                data = mailbox.parse_email_to_device_data(mail)         # Tutaj parsuje do odpowiednich danych
                print(data)
                data = None
                
            except Exception as ex:
                logging.debug('Jest problem z mailem mail: %s', mail)
                logging.exception('Error!')

    except Exception as e:
        logging.error('P1.1 - Błąd krytyczny w pętli parsowania email z serwera SQL. Pętla została przerwana. %s', e)
        logging.error('Error on line {}'.format(sys.exc_info()[-1].tb_lineno))
        logging.exception('Error!')
        logging.error(traceback.format_exc())
        logging.error('Zapisano')


# TODO Section

# Ta metoda zaktualizuje wszystkie rekordy tak aby pola zawieraly tonerlevel_x = ""
# Nie będzie potrzeby sprawdzania tego w aplikacji

def SetNullTonerLevelToEmptyString():

    db = MongoTB()
    UpdateRecrods(db.records)
    UpdateRecrods(db.records_other)


def UpdateRecrods(source):
    
    null_c = source.find({'tonerlevel_c':None})
    null_m = source.find({'tonerlevel_m':None})
    null_y = source.find({'tonerlevel_y':None})
    null_k = source.find({'tonerlevel_k':None})

    print(null_c.count)
    print(null_m.count)
    print(null_y.count)
    print(null_k.count)

    for rec in null_c:
        if ('tonerlevel_c' in rec.keys() and rec['tonerlevel_c'] == None):
            rec['tonerlevel_c'] = ''
        else:
            rec['tonerlevel_c'] = ''
        print(rec['tonerlevel_c'])
        source.replace_one({'_id':rec['_id']} ,rec)

    for rec in null_m:
        if ('tonerlevel_m' in rec.keys() and rec['tonerlevel_m'] == None):
            rec['tonerlevel_m'] = ''
        else:
            rec['tonerlevel_m'] = ''
        print(rec['tonerlevel_m'])
        source.replace_one({'_id':rec['_id']} ,rec)
    
    for rec in null_y:
        if ('tonerlevel_y' in rec.keys() and rec['tonerlevel_y'] == None):
            rec['tonerlevel_y'] = ''
        else:
            rec['tonerlevel_y'] = ''
        print(rec['tonerlevel_y'])
        source.replace_one({'_id':rec['_id']} ,rec)
    
    for rec in null_k:
        if ('tonerlevel_k' in rec.keys() and rec['tonerlevel_k'] == None):
            rec['tonerlevel_k'] = ''
        else:
            rec['tonerlevel_k'] = ''
        print(rec['tonerlevel_k'])
        source.replace_one({'_id':rec['_id']} ,rec)

def MigrateDataFromMongoToSQL():
    j_settings = None
    
    with open('D:\\data.json', 'r') as fp:
        j_settings = json.load(fp)

    mongo = MongoTB()
    sql = TBSQL()
    sql.Connect()
    xmlLoader = XMLLoader(j_settings['workfolder'] + j_settings['XmlForEmails'])
    emParser = EmailParserV2(xmlLoader)

    allRecords = mongo.records.find()

    EmailSources = 0
    HTMLSources = 0

    for el in allRecords:
        if el['parsed_by_email'] is True:
            EmailSources = EmailSources + 1
            mail = mongo.email_binary_db.find_one({'_id': el['email_info']})
            mail = EmailPop3Client.parse(mail)
            parsingResults = emParser.ParseEmailToMachineRecord(mail)
            if parsingResults['sucess'] is True:
                sql.InsertMachineRecord(parsingResults['record'], parsingResults['sourceEmail']['body-binary'])
            else:
                raise Exception('Woops')
        else:
            HTMLSources = HTMLSources + 1
    
    print(EmailSources)
    print(HTMLSources)

def SQLTest_TestingInsertingRecords():
    
    j_settings = None
    
    with open('D:\\data.json', 'r') as fp:
        j_settings = json.load(fp)

    mongo = MongoTB()
    sql = TBSQL()
    sql.Connect()
    xmlLoader = XMLLoader(j_settings['workfolder'] + j_settings['XmlForEmails'])
    emailClient = EmailPop3Client(j_settings['emailConnection'])

    #emailParserDocument = mongo.global_get_emailparser()
    #open('D:\\email.xml', 'w', encoding = 'utf-8').write(emailParserDocument)

    emParser = EmailParserV2(xmlLoader)
    

    #sqlRecords = sql.GetAll(sql.MachineRecord)
    #records = mongo.email_parsed_success_db.find()
    records = mongo.email_binary_db.find()

    # rec = mongo.email_binary_db.find_one({'_id': b'+OK 92 000015f458217c35'})
    # el = EmailPop3Client.parse(rec)

    for el in records:
        
        #el = mongo.email_binary_db.find_one({'_id': el['_id']})

        try:
            el = EmailPop3Client.parse(el)
        except ServerException:
            continue
        parsingResults = emParser.ParseEmailToMachineRecord(el)
        if parsingResults['sucess'] is True:
            sql.InsertMachineRecord(parsingResults['record'], parsingResults['sourceEmail']['body-binary'])



    pass

def HTMLParser_Testing():


    j_settings = None
    
    with open('D:\\data.json', 'r') as fp:
        j_settings = json.load(fp)

    xmlHTMLLoader = XMLLoaderForHTML(j_settings['workfolder'] + j_settings['XmlForHTML'])

    path = 'D:\\tmp\\tmp'
    files = os.listdir(path)

    htmlParser = HTMLParser(xmlHTMLLoader)

    

    for f in files:
        print(f)
        data = open(path + '\\' + f).read()
        results = htmlParser.parse(data)
        print (results)

    pass

if __name__ == "__main__":
    import settings
    settings.init()
    
    #eng = Engine()

    #eng.parse_loop_email()

    #testEmailParsing()

    #SetNullTonerLevelToEmptyString()    
    #SQLTest()
    #SQLTest_TestingInsertingRecords()
    #MigrateDataFromMongoToSQL()
    HTMLParser_Testing()

    pass

