import logging
from ThreadEngine import Engine

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
        ids = numMessages = mailbox.get_emails_id()             # Pobranie wszystkich id z serwera pocztowego
        for i in range(1, len(ids)):                            # Dla kazdego id na serwerze
            message = mailbox.get_email_pop3(i)                 # Pobieram wiadomosc w formacie {'_id': bytes_id, 'mail':tablica_bajtow_wiadomosc }
            if message is not None:                             # Jesli wiadomosc pobrano to
                mails.append(message)

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


if __name__ == "__main__":
    import settings
    settings.init()
    
    #eng = Engine()

    #eng.parse_loop_email()

    testEmailParsing()
