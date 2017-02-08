import string
import random
from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_v1_5
import time
import logging
import settings
from TBExceptions import ServerException

settings.init()


class RSAv3:

    private_key = None
    private_cipher = None
    sleep = 0
    slit = 64

    def __init__(self, m_bytes, e_bytes):
        RSAv3.init_private_key()
        m_bytes = int.from_bytes(m_bytes, 'big')
        e_bytes = int.from_bytes(e_bytes, 'big')
        self.client_key = RSA.construct((m_bytes, e_bytes))
        self.client_cipher = PKCS1_v1_5.new(self.client_key)

    def encryp_data(self, bytestoencrypt, slit_allowed=True):
        if isinstance(bytestoencrypt, str):
            bytestoencrypt = bytestoencrypt.encode('utf-8')
        if bytestoencrypt.__len__() > self.slit and not slit_allowed:
            raise Exception('encrypt - bytestoencrypt.len: {}, slitAllowd {}'.format(bytestoencrypt.__len__(), slit_allowed))

        if bytestoencrypt.__len__() > self.slit:
            return self.__encryptbigdata(bytestoencrypt)
        else:
            return self.client_cipher.encrypt(bytestoencrypt)

    def __encryptbigdata(self, input_data):
        parts = []

        arraysize = input_data.__len__() / 64

        if not input_data.__len__() % 64 == 0:
            arraysize += 1

        for i in range(0, int(arraysize)):
            parts.append(input_data[i*64:  (i*64)+64])

        final = bytearray()

        for array in parts:
            encrypted = self.encryp_data(array, False)
            final.extend(encrypted)

        return final

    @staticmethod
    def decrypt(message, allow_slit = True):
        if message.__len__() > 128 and not allow_slit:
            logging.error('Error, slit not allowed. Data len is more than 128 bytes. Authorization.RSAv3.decrypt')
            raise ServerException('Error, slit not allowed. Data len is more than 128 bytes. Authorization.RSAv3.decrypt')

        if message.__len__() == 0:
            logging.debug('Message len == 0. In staticmethod decrypt')
            raise ServerException('Empty message to decrypt')

        if RSAv3.private_cipher is None:
            RSAv3.init_private_key()

        decrypted = None

        try:
            if message.__len__() > 128:
                decrypted = RSAv3.decrypt_big_data(message)
            else:
                decrypted = RSAv3.private_cipher.decrypt(message, False)
        except ServerException:
            logging.log('An error in decrypting')

        return decrypted

    @staticmethod
    def decrypt_big_data(input_bytes):
        if not input_bytes.__len__() % 128 == 0:
            logging.error('Nieprawidlowe dane wejsciowe. Oczekiwana długość tablicy to x * 128. Authorization.RSAv3.decrypt_big_data')
            raise ServerException('Nieprawidlowe dane wejsciowe. Oczekiwana długość tablicy to x * 128. Authorization.RSAv3.decrypt_big_data')
        final = bytearray()
        steps_count = int(input_bytes.__len__() / 128)
        for i in range(0, steps_count):
            datatodecrypt = input_bytes[i * 128: (i * 128) + 128]
            decrypted = RSAv3.decrypt(datatodecrypt, False)
            if RSAv3.sleep > 0:
                time.sleep(0.0050)
            if decrypted is None:
                logging.debug('Nie udało się odszyfrować dużych danych. :-/')
                raise ServerException('Nie udało się odszyfrować dużych danych. :-/')
            elif isinstance(decrypted, str):
                logging.debug('Nieprawidłowe dane wejściowe. Oczekiwane dane: bytes. Otrzymano: str')
                raise ServerException('Nieprawidłowe dane wejściowe. Oczekiwane dane: bytes. Otrzymano: str')
            elif isinstance(decrypted, bool):
                logging.debug('Nieprawidłowe dane wejściowe. Oczekiwane dane: bytes. Otrzymano: str')
                raise ServerException('Nieprawidłowe dane wejściowe. Oczekiwane dane: bytes. Otrzymano: str')
            else:
                try:
                    final.extend(decrypted)
                except Exception:
                    logging.debug('Niepowodzenie przy rozszezaniu tablicy. decrypt_big_data.')
                    raise ServerException('Niepowodzenie przy rozszezaniu tablicy. decrypt_big_data.')

        return final

    @staticmethod
    def init_private_key():
        f = open(settings.workfolder + '/private-server-key.pem', 'r')
        RSAv3.private_key = RSA.importKey(f.read())
        RSAv3.client_key = 0
        RSAv3.private_cipher = PKCS1_v1_5.new(RSAv3.private_key)
        f.close()


class Handshake:

    keylengt = 50
    sucess = False
    key_imported = False

    def __init__(self, m, e):
        try:
            m = RSAv3.decrypt(m)
            e = RSAv3.decrypt(e)
        except ServerException as e:
            logging.info('Ops, decrypting some of parameters faild.')
            self.key_imported = False
            return
        self.handshake_key = self.createkey()
        self.clientRSA = RSAv3(m, e)
        self.key_imported = True

    def encrypt(self, text):
        if self.clientRSA is None:
            raise ServerException('clientRSA is None')
        return self.clientRSA.encryp_data(text)

    def decrypt(self, text):
        try:
            decrypted = RSAv3.decrypt(text)
        except ServerException as e:
            return None
        return decrypted

    def sort(self, text):
        asciivalues = []
        for char in text:
            asciivalues.append(ord(char))
        asciivalues.sort()
        keycharlist = []
        for v in asciivalues:
            keycharlist.append(chr(v))

        return ''.join(keycharlist)

    def createkey(self):
        return ''.join(random.choice(string.ascii_uppercase + string.digits) for _ in range(self.keylengt))

    def getKeyToSend(self):
        return self.handshake_key

    def getHandShakeSucess(self):
        return self.sucess

    def checkReceivedKey(self, receivedKey):
        if (receivedKey == self.sort(self.handshake_key)):
            self.sucess = True
            return True
        else:
            self.sucess = False
            return False
