import threading
import time
import sys

import os
import traceback

import json

import Parser
import MongoDatabase
import Email

import logging

import settings

from Service.SQL.SqlDatabase import TBSQL
from Service.Email.EmailClient import EmailPop3Client
from Service.Parsing.EmailParsing import EmailParserV2
from Service.Parsing.HTMLParser import HTMLParser
from Service.Parsing.XmlParsing import XMLLoader
from Service.Parsing.XmlParsing import XMLLoaderForHTML

from MongoDatabase import MongoTB
from Parser import DataParser
from Email import EmailParser

from datetime import datetime, timedelta

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
                    #mailbox.del_email(i)                            # I usuwamy z serwera

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

    def parse_loop_emailV2(self):
        try:
            self.emailClient.ConnectPop3Client()
            emailsCount = self.emailClient.GetEmailCount()

            if (emailsCount > 50):
                emailsCount = 50;

            for i in range(1, emailsCount):
                mail = self.emailClient.get_email_pop3(i)
                if mail is not None:
                    mail = self.emailClient.parse(mail)
                    try:
                        parsingResults = self.emailParserV2.ParseEmailToMachineRecord(mail)
                        if parsingResults['sucess'] is True:
                            self.sql.InsertMachineRecord(parsingResults['record'], parsingResults['sourceEmail']['body-binary'])
                            self.emailClient.del_email(i)
                        else:
                            self.sql.InsertWarehouseEmail(mail['body-binary'])
                            self.emailClient.del_email(i)
                    except Exception as e2:
                        logging.error('P2.2 - Parsowanie lub zapis nie powiodły sie.')
                        logging.error(e2)
                        # ToDo, send email to Tomek :)

        except Exception as e:
            logging.error('P1.1 - Błąd krytyczny w pętli parsowania email. Pętla została przerwana. %s', e)
            logging.error('Error on line {}'.format(sys.exc_info()[-1].tb_lineno))
            logging.exception('Error!')
            logging.error(traceback.format_exc())
            logging.error('Zapisano')
            # ToDo, send email to Tomek :)
        
        self.emailClient.close()

    def parse_loop(self):
        try:
            files = self.mongo.global_get_fulldata(True)
            for f in files:
                device = DataParser(f)
                sucess = self.mongo.import_to_database(device)
                if not sucess:
                    self.mongo.global_import_fulldatafaild(f)
        except:
            logging.error('Krytyczny blad w przetwarzaniu zdalnych raportow HTML')
            logging.exception('Error!')

    def parse_loopV2(self):
        try:
            files = self.mongo.global_get_fulldata()
            for f in files:

                results = self.htmlParser.parse(f)
                if results['sucess'] is True:
                    self.sql.InsertMachineRecord_HTML(results['record'], results['sourceSerialHTML'], results['sourceCounterHTML'])
                else:
                    self.sql.InsertWarehouseHTML(results['record'], results['sourceSerialHTML'], results['sourceCounterHTML'])

        except Exception as e:
            logging.error('P2.1 - Błąd krytyczny w pętli parsowania HTML. %s', e)
            logging.error('Error on line {}'.format(sys.exc_info()[-1].tb_lineno))
            logging.exception('Error!')
            logging.error(traceback.format_exc())
            logging.error('Zapisano')

    def file_sync(self):
        if self.last_file_update < datetime.today():
            if datetime.now() > datetime.today() + timedelta(hours=1):
                data = self.mongo.global_get_emailparser()
                if data is not None:
                    f = open(settings.workfolder + '/test/emailparser.xml', 'w', encoding = 'utf-8')
                    f.write(data)
                    f.close()
                data = self.mongo.global_get_mactoweb()
                if data is not None:
                    f = open(settings.workfolder + '/test/mactoweb.xml', 'w', encoding = 'utf-8')
                    f.write(data)
                    f.close()
                self.last_file_update = datetime.today()
                logging.info('zaktualizowano pliki mactoweb i emailparser')
            

    def __init__(self, interval=5*60):
        """ Constructor
        :type interval: int
        :param interval: Check interval, in seconds
        """

        j_settings = None
    
        path = 'data.json'

        if (os.path.isfile('D:\data.json')):
            path = 'D:\data.json'
        elif os.path.isfile('/home/tomek/metcounter/settingsv2.json'):
            path = '/home/tomek/metcounter/settingsv2.json'

        with open(path, 'r') as fp:
                j_settings = json.load(fp)
        

        self.sql = TBSQL()
        self.sql.Connect(j_settings['SQL_ConnectionString'])
        
        # Initialize Email domain
        xmlLoader = XMLLoader(j_settings['workfolder'] + j_settings['XmlForEmails'])
        self.emailParserV2 = EmailParserV2(xmlLoader)
        self.emailClient = EmailPop3Client(j_settings['emailConnection'])

        # Initialize HTML domain
        xmlHTMLLoader = XMLLoaderForHTML(j_settings['workfolder'] + j_settings['XmlForHTML'])
        self.htmlParser = HTMLParser(xmlHTMLLoader)

        self.interval = interval
        self.last_file_update = datetime.today() - timedelta(days=1)

        #thread = threading.Thread(target=self.run, args=())
        #thread.daemon = True                            # Daemonize thread
        #thread.start()                                  # Start the execution
        #thread.join()

    def run(self):
        """ Method that runs forever """
        logging.info('Startujemy petle')
        while True:
            try:
                self.file_sync()
                self.parse_loopV2()
                self.parse_loop_emailV2()

                time.sleep(self.interval)
            except:
                logging.exception("Błąd krytyczny")
                break

    def start_newthread(self):
        thread = threading.Thread(target=self.run, args=())
        thread.daemon = False                           
        thread.start()                                  
        thread.join()

    def test_filesync(self):
        self.file_sync()

    def test_html_loopV2(self):
        while True:
            self.parse_loopV2()
            time.sleep(60)

    def test_email_loopV2(self):
        while True:
            self.parse_loop_emailV2()
            time.sleep(60)

