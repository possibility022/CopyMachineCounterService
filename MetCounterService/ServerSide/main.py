import socket
import threading
import socketserver

import logging

import Authorization
import Decoder as Decoder_File
import Email
import Localdatabase
import MongoDatabase
import Parser
import ServerDataOffer
import ServerDataReceiver

from Decoder import Decoder
from MongoDatabase import MongoTB

from ServerDataReceiver import ThreadedTCPRequestHandler
from ServerDataOffer import ThreadedTCPOfferHandler
from Parser import DataParser
from Email import EmailParser
import Localdatabase
from Localdatabase import Database

import settings
import os
from time import sleep
import _thread



class ThreadedTCPServer(socketserver.ThreadingMixIn, socketserver.TCPServer):
    pass

mongo = MongoTB()
dec = Decoder()


def parse_loop_email():
    logging.info('parse_loop_email started')
    
    try:
        while True:
            if Localdatabase.Database.getthread_pause_value_email():
                sleep(60)
                continue
            mailbox = EmailParser()
            ids = numMessages = mailbox.get_emails_id()             # Pobranie wszystkich id z serwera pocztowego
            for i in range(1, len(ids)):                            # Dla kazdego id na serwerze
                message = mailbox.get_email_pop3(i)                 # Pobieram wiadomosc w formacie {'_id': bytes_id, 'mail':tablica_bajtow_wiadomosc }
                if message is not None:                             # Jesli wiadomosc pobrano to
                    mailbox.insert_email_to_queue(message)          # Zapisujemy ja do kolejki
                    mailbox.del_email(i)                            # I usuwamy z serwera

            mailbox.close()
            queue = mailbox.get_queue()

            mails_to_delete = []

            for mail in queue:
                data = None
                try:
                    email = mailbox.check_email_parsed(mail['_id'])             # Tutaj sprawdzam czy mail jest juz przerobiony
                    if email is not None:
                        logging.debug('Mail był już przetwożony')
                        mails_to_delete.append(mail)              
                    else:
                        data = mailbox.parse_email_to_device_data(mail)         # Tutaj parsuje do odpowiednich danych
                        mails_to_delete.append(mail)
                except Exception as ex:
                    logging.debug('Jest problem z mailem mail: %s', mail)

                mongo.import_to_database(data)                                  # Tutaj zapisuje juz do prawidlowej kolekcji

            for mail in mails_to_delete:
                mongo.email_toparse_db.delete_one(mail)                         # Usuwam maile z kolejki
            #mailbox = EmailParser()
            #ids = mailbox.get_emails_id()
            #for i in range(len(ids)):
            #    email = mailbox.check_email_parsed(ids[i])
            #    if email is None:
            #        data = None                    
            #        logging.info('Nie znalazłem maila. ID: %s', ids[i])
            #        if mongo.check_email_is_on_suspect_list(ids[i]):
            #            logging.info('Mail jest na liście podejrzanych maili, zostanie całkowicie ominięty w obsłudze')
            #            continue
            #        mail = mailbox.get_email_pop3(i + 1)
            #        if mail is None:
            #            mongo.insert_email_to_suspect(ids[i])
            #            logging.info('Mail został dodany na listę maili podejrzanych. ID: %s', ids[i])
            #            continue
            #        try:
            #            data = mailbox.parse_email_to_device_data(mail)
            #        except Exception as e:
            #            logging.error('Błąd przy parsowaniu maila: ' + ids[i])
            #            data = None
            #        if data is not None:
            #            mongo.import_to_database(data)
            #        else:
            #            logging.info('Usuwam maila. Dane po konwersji do danych recordu są puste. %s', ids[i])
            #            mailbox.del_email(i + 1)
            #    else:
            #        logging.info('Mail znaleziony, omijam i usuwam: %s', ids[i])
            #        mailbox.del_email(i + 1)
            mailbox.close()
            sleep(5 * 60)
    except Exception as e:
        logging.error('P1 - Błąd krytyczny w pętli parsowania email. Pętla została przerwana. %s', e)


def parse_loop():
    logging.info('parse_loop started')

    while True:
        if Localdatabase.Database.getthread_pause_value_file():
            sleep(60)
            continue
        dec.decode()
        dec.delete_decoded_files()
        files = os.listdir(mongo.decoded_dir)
        for f in files:
            filepath = mongo.decoded_dir + '/' + f
            reader = open(filepath)
            data = reader.read()
            reader.close()
            device = DataParser(data)
            sucess = mongo.import_to_database(device)
            if sucess:
                os.rename(filepath, settings.workfolder + '/imported/' + f)
            else:
                os.rename(filepath, settings.workfolder + '/faild/' + f)
        sleep(30)

if __name__ == "__main__":

    logging.basicConfig(filename='/home/over/deamon.log', level=logging.DEBUG, format='%(asctime)s %(message)s', datefmt='%d/%m/%Y %H:%M:%S %p')
    settings.init()

    HOST = settings.HOST
    PORT = settings.PORT
    PORTMACTOWEBFILEDOWNLOAD = settings.PORTMACTOWEBFILEDOWNLOAD

    server = ThreadedTCPServer((HOST, PORT), ThreadedTCPRequestHandler)
    server_file = ThreadedTCPServer((HOST,PORTMACTOWEBFILEDOWNLOAD), ThreadedTCPOfferHandler)
    ip, port = server.server_address
    ip_file, port_file = server_file.server_address

    # Start a thread with the server -- that thread will then start one
    # more thread for each request
    server_thread = threading.Thread(target=server.serve_forever)
    server_file_thread = threading.Thread(target=server_file.serve_forever)

    # Exit the server thread when the main thread terminates
    server_thread.daemon = False
    server_thread.start()

    server_file_thread.deamon = False
    server_file_thread.start()

    logging.info("Server loop running in thread: {}".format(server_thread.name))
    logging.info("Server loop running in thread: {}".format(server_file_thread.name))
    
    _thread.start_new_thread(parse_loop, ())
    _thread.start_new_thread(parse_loop_email, ())


