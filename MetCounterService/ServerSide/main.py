import socket
import threading
import socketserver

import logging

import Authorization
import Decoder as Decoder_File
import Localdatabase
import MongoDatabase_Global
import ServerDataOffer
import ServerDataReceiver

from Decoder import Decoder
from MongoDatabase_Global import MongoTB_Global

from ServerDataReceiver import ThreadedTCPRequestHandler
from ServerDataOffer import ThreadedTCPOfferHandler
import Localdatabase
from Localdatabase import Database

import settings
import os
from time import sleep
import _thread

from datetime import date, timedelta



class ThreadedTCPServer(socketserver.ThreadingMixIn, socketserver.TCPServer):
    pass

mongo = MongoTB_Global()
dec = Decoder()

def parse_loop():
    logging.info('parse_loop started')
    
    print(date.today())
    last_file_update = date.today() - timedelta(days=1)

    while True:
        dec.decode()
        dec.delete_decoded_files()
        files = os.listdir(mongo.decoded_dir)
        for f in files:
            filepath = mongo.decoded_dir + '/' + f
            reader = open(filepath)
            data = reader.read()
            reader.close()
            sucess = mongo.import_fulldata(data)
            if sucess:
                os.rename(filepath, settings.workfolder + '/imported/' + f)
            else:
                os.rename(filepath, settings.workfolder + '/faild/' + f)

        #Update plikow .xml
        if last_file_update < date.today():
            file = open(settings.workfolder + '/test/mactoweb.xml', 'r')
            mactoweb = file.read()
            file.close()
            
            mongo.import_emailparser(mactoweb)

            file = open(settings.workfolder + '/test/emailparser.xml', 'r')
            emailparser = file.read()
            file.close()

            mongo.import_emailparser(emailparser)
        
        sleep(30)

if __name__ == "__main__":

    logging.basicConfig(filename='/home/over/deamon.log', level=logging.WARNING, format='%(asctime)s %(message)s', datefmt='%d/%m/%Y %H:%M:%S %p')
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


