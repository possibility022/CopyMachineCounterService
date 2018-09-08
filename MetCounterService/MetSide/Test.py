import logging
from SQL.SqlDatabase import TBSQL

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
    try:
        mails = []
        mailbox = EmailParser()
        sqlDatabase = TBSQL()
        sqlDatabase.Connect()
        ids = numMessages = mailbox.get_emails_id()             # Pobranie wszystkich id z serwera pocztowego
        for i in range(1, len(ids)):                            # Dla kazdego id na serwerze
            message = mailbox.get_email_pop3(i)                 # Pobieram wiadomosc w formacie {'_id': bytes_id, 'mail':tablica_bajtow_wiadomosc }
            if message is not None:                             # Jesli wiadomosc pobrano to
                mails.append(message)
                mailAsBytes = pickle.dumps(message)
                sqlDatabase.ImportEmailToQueue(v)

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
            
def SQLTest():
    mongo = MongoTB()
    s = TBSQL()
    s.Connect()

    # Adding new
    #for el in mongo.email_binary_db.find():
    #    print(el)
    #    v = pickle.dumps(el)
    #    print(type(v))
    #    v2 = pickle.loads(v)
    #    print(v2)
    #    s.ImportEmailToQueue(v)
    #    break
    #pass

    # emails = s.GetEmailsToParse()
    # for el in emails:
    #     v2 = pickle.loads(el.Content)
    #     print (v2)


if __name__ == "__main__":
    import settings
    settings.init()
    
    #eng = Engine()

    #eng.parse_loop_email()

    #testEmailParsing()

    #SetNullTonerLevelToEmptyString()    
    SQLTest()
    pass

