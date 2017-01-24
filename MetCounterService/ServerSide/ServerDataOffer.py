import threading
import socketserver
from Authorization import Handshake
from Localdatabase import Database
import logging
import settings

settings.init()


class ThreadedTCPOfferHandler(socketserver.BaseRequestHandler):

    workfolder = settings.workfolder
    filenamemactoweb = workfolder + "/test/mactoweb.xml"
    handshake = None

    def handle(self):
        logging.info('Incoming request for data')
        key_moduls_encrypted = self.request.recv(256)
        key_exponent_encrypted = self.request.recv(128)
        self.handshake = Handshake(key_moduls_encrypted, key_exponent_encrypted)
        
        if not self.handshake.key_imported:
            logging.critical('OTRZYMANE ELEMENTY KLUCZA SA NIEPRAWIDLOWE')
            return

        self.send(self.handshake.getKeyToSend())
        data = self.request.recv(256)
        receivedkey = str(self.handshake.decrypt(data), 'ascii')

        if not self.handshake.checkReceivedKey(receivedkey):
            logging.critical('OTRZYMANY KLUCZ NIE JEST PRAWIDŁOWY!')
            return

        logging.debug('Klucz ok')

        clientid = self.receive(128)
        if clientid.__len__() != Database.key_len:
            return

        logging.info('Dataoffer: klient-id: {}'.format(clientid))

        self.send('#|$HANDSHAKE_OK$|#')

        logging.debug('Dataoffer: Handshake OK')

        # KONIEC PROCEDURY HANDSHAKE

        data = self.receive(128)
        command = str(data, 'ascii')

        if command == 'XMLO':
            logging.info('XMLO')
            self.sendxmlfile()
        elif command == 'CLID':
            logging.info('CLID')
            self.sendnewclientid()

    def finish(self):
        self.request.close()

    def send(self, data):
        if isinstance(data, str):
            data = data.encode('utf-8')
        encrypted = self.handshake.encrypt(data)
        self.request.send(encrypted)

    def sendnewclientid(self):
        id = Database.getnewid()
        logging.info('Sending new client id: {}'.format(id))
        self.send(id)

    def sendxmlfile(self):
        f = open(self.filenamemactoweb, 'r')
        l = f.read()
        self.send(l)
        f.close()
        logging.debug('offering xml : done')

    def receive(self, size, decrypt=True):
            # print('Odbieranie danych size:', size)
            final = bytearray()

            n = size
            while len(final) < n:
                incoming = size - len(final)
                if incoming > 1024:
                    data = self.request.recv(1024)
                else:
                    data = self.request.recv(incoming)

                if not data:
                    break

                final.extend(data)

            final = bytes(final)

            if len(final) != size:
                logging.error("error in receiving, missing data. Received: {} Expected: {}".format(len(final), len(size)))
            else:
                logging.debug('Full data received')

            if decrypt:
                return self.handshake.decrypt(final)
            else:
                return final