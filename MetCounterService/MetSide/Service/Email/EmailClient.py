from poplib import error_proto
import email
import getpass, poplib
import logging
import base64

from Service import *

class EmailPop3Client:

    def __init__(self, connectionSettings):
        self.Mailbox = poplib.POP3(connectionSettings['ip'], connectionSettings['port'])
        self.Mailbox.user(connectionSettings['user'])
        self.Mailbox.pass_(connectionSettings['pass'])
        self.msgcount = 0
        pass

    def get_email_pop3(self, which):
        try:
            doc = self.Mailbox.retr(which)
            msg_id = self.Mailbox.uidl(which)
            mail = {'mail': doc[1], '_id': msg_id}
            return mail
        except error_proto as error:
            logging.info('Powstał jakiś problem przy pobieraniu maila. Info: %s', error)
            return None

    def del_email(self, which):
        self.Mailbox.dele(which)

    @staticmethod
    def convert_to_bytes(listOfBinary):
        return b'\n'.join(listOfBinary)

    @staticmethod
    def parse(mail):
        body = None
        encoding = EmailPop3Client.get_encoding(mail['mail'])

        byte_message = EmailPop3Client.convert_to_bytes(mail['mail'])
        
        message = email.message_from_bytes(byte_message)

        for part in message.walk():
            if part.get_content_type() == 'text/plain':
                body = part.get_payload(decode=True)
                break   

        #body = message.get_payload(0).get_payload(decode=True)
        try:
            if body is not None:
                if encoding is not '':
                    body = body.decode(encoding)
                else:
                    body = body.decode('utf-8')
            else:
                raise ServerException('Serwer nie zdekodował wiadomości. Prawdopodobnie jest innego rodzaju niż text/plain')
        except UnicodeDecodeError as e:
            raise ServerException('Serwer nie zdekodował wiadomości. Prawdopodobnie jest innego rodzaju niż text/plain')


        if isinstance(body, str):
            lines = body.split('\n')
        elif isinstance(body,bytes):
            lines = body.split(b'\n')
        else:
            raise ServerException('Email został niepoprawnie przetworzony')

        parsedEmail = {
            'id': base64.b64encode(mail['_id']).decode('utf-8'),
            'body': body,
            'body-lines': lines,
            'body-binary': byte_message
        }

        return parsedEmail

    @staticmethod
    def get_encoding(doc):
        if isinstance(doc, tuple):
            msg = doc[1]
        elif isinstance(doc, list):
            msg = doc
        else:
            msg = []

        for el in msg:
            s = el.decode('utf-8')
            if s.startswith('Content-Type:'):
                s = s.replace('Content-Type:', '')
                parts = s.split(';')
                for part in parts:
                    if part.__contains__('charset='):
                        part = part.strip()
                        part = part.replace('charset=', '')
                        return part

        return ''