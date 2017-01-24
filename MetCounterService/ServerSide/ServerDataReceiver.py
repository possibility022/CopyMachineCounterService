import socketserver
import time
import sys
import logging
import settings

from Localdatabase import Database
from Authorization import Handshake


settings.init()


class ThreadedTCPRequestHandler(socketserver.BaseRequestHandler):

    emptydata = 0
    command_disconnectmessage = "#|$QUIT_DISCONNECT$|#"
    command_handshake_ok = '#|$HANDSHAKE_OK$|#'
    command_full_data_received = "#|$FULL_DATA_RECEIVED$|#"
    command_receive_machine = "#|$RECEIVE_MACHINE_DATA$|#"
    receiveddata = []
    handshake = None

    def handle(self):

        logging.info('Połączono z klientem: {}'.format(self.client_address))

        key_moduls_encrypted = self.request.recv(256)
        key_exponent_encrypted = self.request.recv(128)

        # Rozpoczecie procedury handshake

        self.handshake = Handshake(key_moduls_encrypted, key_exponent_encrypted)

        if not self.handshake.key_imported:
            logging.critical('OTRZYMANE PARAMETRY KLUCZA SA NIEPRAWIDLOWE')
            return

        self.send(self.handshake.getKeyToSend())
        data = self.request.recv(256)
        receivedkey = str(self.handshake.decrypt(data), 'ascii')

        if not self.handshake.checkReceivedKey(receivedkey):
            logging.critical('HANDSHAKE FAILD')
            return

        clientid = self.receive(128)
        if clientid.__len__() != Database.key_len:
            logging.info('Client id len incorrect')
            return

        self.send(self.command_handshake_ok)

        # KONIEC PROCEDURY HANDSHAKE

        logging.debug('Rec handshake ok')

        while True:
            command = self.receive(128)
            try:
                command = command.decode('utf-8')
                logging.debug('Command: {}'.format(command))
                if command == self.command_disconnectmessage:
                    break
                elif command == self.command_receive_machine:
                    incoming_data = self.receive(128)
                    incoming_data = int.from_bytes(incoming_data, 'little')
                    machine = self.receive(incoming_data, False)
                    self.send(self.command_full_data_received)
                    self.receiveddata.append(machine)
                else:
                    logging.info('Unknown command')
            except Exception as e:
                # print("Unexpected error:", sys.exc_info()[0])
                logging.error('Niespodziewany błąd: {}'.format(sys.exc_info()[0]))
                logging.error(e)
                break

    def finish(self):
        if self.handshake.sucess:
            i = 0
            for data in self.receiveddata:
                workfolder = settings.workfolder
                if isinstance(data, bytearray) or isinstance(data, bytes):
                    f = open(workfolder + "/msg/messages_" + time.strftime("%Y%m%d-%H%M%S" + str(i)), 'wb')
                elif isinstance(data, str):
                    f = open(workfolder + "/msg/msg/messages_" + time.strftime("%Y%m%d-%H%M%S" + str(i)), 'w')
                else:
                    logging.error('Dane nie są: str lub bytearray lub bytes')
                    # print('Data is not an str or bytearray or bytes')
                f.write(data)
                f.close()
                i += 1
        self.receiveddata.clear()
        self.request.close()
        logging.info('Zamknieto połączenie {} :-)'.format(self.client_address))
        # print('Connection closed :)', self.client_address)

    def send(self, data):
        if isinstance(data, str):
            data = data.encode('utf-8')

        encrypted = self.handshake.encrypt(data)
        self.request.send(encrypted)

    def receive(self, size, decrypt=True):
            # print('Odbieranie danych size:', size)
            logging.info('Odbieranie danych: {}'.format(size))

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
                logging.error('Błąd przy odbieraniu. Brakuje danych. Received: {} Expected: {}'.format(len(final), len(size)))
                # print("error in receiving, missing data. Received: {} Expected: {}".format(len(final), len(size)))
            else:
                # print('Full data received')
                logging.debug('Odebrano wszystkie dane')

            if decrypt:
                return self.handshake.decrypt(final)
            else:
                return final

