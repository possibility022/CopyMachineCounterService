from poplib import error_proto
import email
import getpass, poplib
import logging
import base64

from Service.Exceptions.TBExceptions import ServerException

# Sender
# Import smtplib for the actual sending function
from smtplib import SMTP
# Import the email modules we'll need
from email.message import EmailMessage
from email import parser

class EmailPop3Client:

    def __init__(self, connectionSettings):
        self.user = connectionSettings['user']
        self.password = connectionSettings['pass']
        self.Mailbox = poplib.POP3(connectionSettings['ip'], connectionSettings['port'])
        self.Mailbox.user(self.user)
        self.Mailbox.pass_(self.password)
        self.Smtp = SMTP(connectionSettings['ip'])
        self.Smtp.login(connectionSettings['user'], connectionSettings['pass'])
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

    def GetEmailCount(self):
        return len(self.Mailbox.list()[1])

    def SendEmail(self, mail):
        # Open the plain text file whose name is in textfile for reading.
        
        #mapp = map(bytes.decode, mail['mail'])
        
        strContent = []
        for line in mail['mail']:
            strContent.append(str(line))

        content = '\n'.join(strContent)
        
        parsed = parser.BytesParser().parsebytes(b'\n'.join(mail['mail']))
        parsed['To'] = self.user
        parsed['From'] = self.user

        contentType = ''

        if parsed.is_multipart():
            messages = parsed.get_payload()
            for mes in messages:
                print(mes.get_content_type())
                print(mes.get_payload(decode=True))
                print(mes)
                return mes.get_content_type()
        else:
            return parsed.get_content_type()

        

        # Create a text/plain message
        # msg = EmailMessage()
        # msg.set_content(mail['mail'])

        # # me == the sender's email address
        # # you == the recipient's email address
        # msg['Subject'] = 'The contents of %s' % mail['_id']
        # msg['From'] = self.user
        # msg['To'] = self.user

        # Send the message via our own SMTP server.
        #self.Smtp.sendmail(self.user, self.user, parsed)
        #self.Smtp.send_message(parsed)
        #self.Smtp.quit()