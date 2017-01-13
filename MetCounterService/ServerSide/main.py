import socket
import threading
import socketserver

from Decoder import Decoder
from MongoDatabase import MongoTB

from ServerDataReceiver import ThreadedTCPRequestHandler
from ServerDataOffer import ThreadedTCPOfferHandler
from Parser import DataParser
from Email import EmailParser
import Localdatabase

import settings
import os
from time import sleep
import _thread
import logging


class ThreadedTCPServer(socketserver.ThreadingMixIn, socketserver.TCPServer):
    pass


logging.basicConfig(filename='/home/over/deamon.log', level=logging.DEBUG, format='%(asctime)s %(message)s', datefmt='%d/%m/%Y %H:%M:%S %p')
settings.init()
mongo = MongoTB()
dec = Decoder()


def parse_loop_email():
    logging.info('parse_loop_email started')
    while True:
        if Localdatabase.Database.getthread_pasue_value_email:
            sleep(60)
            continue
        mailbox = EmailParser()
        ids = mailbox.get_emails_id()
        for i in range(len(ids)):
            email = mailbox.check_email_parsed(ids[i])
            if email is None:
                data = None
                logging.info('Nie znalazłem maila. Pobieram wiadomość.')
                mail = mailbox.get_email_pop3(i + 1)
                logging.debug('Pobrałem wiadomosc')
                try:
                    data = mailbox.parse_email_to_device_data(mail)
                except:
                    logging.error('Błąd przy parsowaniu maila: ' + ids[i])
                    data = None
                if data is not None:
                    mongo.import_to_database(data)
            else:
                logging.info('Mail znaleziony, omijam i usuwam: %s', email['_id'])
                mailbox.del_email(i + 1)
        mailbox.close()
        sleep(5 * 60)


def parse_loop():
    logging.info('parse_loop started')

    while True:
        if Localdatabase.Database.getthread_pasue_value_file:
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
        sleep(30)

if __name__ == "__main__":
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
    _thread.start_new_thread(parse_loop_email(), ())

