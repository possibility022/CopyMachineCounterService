import threading
import time
import sys

import os
import traceback

import Parser
import MongoDatabase
import Email

import logging

from MongoDatabase import MongoTB
from Parser import DataParser
from Email import EmailParser

import traceback




class Engine(object):
    """ Threading example class
    The run() method will be started and it will run in the background
    until the application exits.
    """

    def parse_loop_email(self):    
        try:
            mailbox = EmailParser()
            ids = numMessages = mailbox.get_emails_id()             # Pobranie wszystkich id z serwera pocztowego
            for i in range(1, len(ids)):                            # Dla kazdego id na serwerze
                message = mailbox.get_email_pop3(i)                 # Pobieram wiadomosc w formacie {'_id': bytes_id, 'mail':tablica_bajtow_wiadomosc }
                if message is not None:                             # Jesli wiadomosc pobrano to
                    mailbox.insert_email_to_queue(message)          # Zapisujemy ja do kolejki
                    mailbox.del_email(i)                            # I usuwamy z serwera

            queue = mailbox.get_queue()

            mails_to_delete = []

            for mail in queue:
                data = None
                try:
                    email = mailbox.check_email_parsed(mail['_id'])             # Tutaj sprawdzam czy mail jest juz przerobiony
                    if email is None:
                        data = mailbox.parse_email_to_device_data(mail)         # Tutaj parsuje do odpowiednich danych
                    mails_to_delete.append(mail)
                except Exception as ex:
                    logging.debug('Jest problem z mailem mail: %s', mail)
                    logging.exception('Error!')

                self.mongo.import_to_database(data)                                  # Tutaj zapisuje juz do prawidlowej kolekcji

            for mail in mails_to_delete:
                self.mongo.email_toparse_db.delete_one(mail)                         # Usuwam maile z kolejki
                

            mailbox.close()
        except Exception as e:
            logging.error('P1 - Błąd krytyczny w pętli parsowania email. Pętla została przerwana. %s', e)
            logging.error('Error on line {}'.format(sys.exc_info()[-1].tb_lineno))
            logging.exception('Error!')
            logging.error(traceback.format_exc())
            logging.error('Zapisano')

    def parse_loop(self):
        try:
            files = self.mongo.global_get_fulldata()
            for f in files:
                device = DataParser(f)
                sucess = self.mongo.import_to_database(device)
                if not sucess:
                    self.mongo.global_import_fulldatafaild(f)
        except:
            logging.log('Krytyczny blad w przetwarzaniu zdalnych raportow HTML')
            logging.exception('Error!')

    def __init__(self, interval=5*60):
        """ Constructor
        :type interval: int
        :param interval: Check interval, in seconds
        """

        self.mongo = MongoTB()

        self.interval = interval

        #thread = threading.Thread(target=self.run, args=())
        #thread.daemon = True                            # Daemonize thread
        #thread.start()                                  # Start the execution
        #thread.join()

    def run(self):
        """ Method that runs forever """
        logging.info('Startujemy petle')
        while True:
            try:
                self.parse_loop()
                self.parse_loop_email()

                time.sleep(self.interval)
            except:
                logging.exception("Błąd krytyczny")
                break

    def start_newthread(self):
        thread = threading.Thread(target=self.run, args=())
        thread.daemon = False                           
        thread.start()                                  
        thread.join()

    def test_email_loop(self):
        while True:
            self.parse_loop_email()
            time.sleep(60)

    def test_html_loop(self):
        while True:
            self.parse_loop()
            time.sleep(60)

