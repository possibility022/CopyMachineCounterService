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

    TextPlainHeader = 'text/plain'
    ContentTransferEncoding = 'Content-Transfer-Encoding'
    AllowedContentToDecode = ['base64'.upper(), 'quoted-printable'.upper()]

    def __init__(self, connectionSettings):
        self.connectionSettings = connectionSettings
        self.user = connectionSettings['user']
        self.password = connectionSettings['pass']
        self.Smtp = SMTP(connectionSettings['ip'])
        self.Smtp.login(connectionSettings['user'], connectionSettings['pass'])
        self.msgcount = 0
        pass

    def ConnectPop3Client(self):
        self.Mailbox = poplib.POP3(self.connectionSettings['ip'], self.connectionSettings['port'])
        self.Mailbox.user(self.user)
        self.Mailbox.pass_(self.password)

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
    
    def close(self):
        try:
            self.Mailbox.quit()
        except Exception as e:
            logging.warning('Błąd przy zamykaniu mailboxa. Być może zamykasz go dwa razy %s', e)
        except:
            logging.warinng('Błąd przy zamykaniu mailboxa. Być może zamykasz go dwa razy %s', sys.exc_info()[0])

    @staticmethod
    def convert_to_bytes(listOfBinary):
        return b'\n'.join(listOfBinary)

    @staticmethod
    def parse(mail):

        byte_message = EmailPop3Client.convert_to_bytes(mail['mail'])
        message = EmailPop3Client.ParseAsMessage(byte_message)
        lines = message.split('\n')

        parsedEmail = {
            'id': base64.b64encode(mail['_id']).decode('utf-8'),
            'body': message,
            'body-lines': lines,
            'body-binary': byte_message
        }

        return parsedEmail

    def GetEmailCount(self):
        return len(self.Mailbox.list()[1])

    @staticmethod
    def ParseAsMessage(mailBytes):
        message = parser.BytesParser().parsebytes(mailBytes)

        text = EmailPop3Client.__GetMessageTextPlain(message)
        return text
        pass

    @staticmethod
    def __GetMessageTextPlain(message):

        text = []

        if (message.is_multipart()):
            payload = message.get_payload()
            for part in payload:
                t = EmailPop3Client.__GetMessageTextPlain(part)
                if t != '':
                    text.append(t)
        else:
            if message.get_content_type() == EmailPop3Client.TextPlainHeader:
                if EmailPop3Client.ContentTransferEncoding in message.keys():
                    
                    encoding = message[EmailPop3Client.ContentTransferEncoding]
                    if encoding.upper() in EmailPop3Client.AllowedContentToDecode:
                        decodedBytes = message.get_payload(decode=True)
                        charset = EmailPop3Client.__FindCharset(message)
                        if charset is None:
                            return decodedBytes.decode()
                        else:
                            return decodedBytes.decode(charset)
                        
                        
                    else:
                        return message.get_payload(decode=False)
                else:
                    return message.get_payload(decode=False)

            else:
                return ''

        return '\n'.join(text)
    
    @staticmethod
    def __FindCharset(message):
        # text/plain;charset= ISO-8859-2
        charsetPrefix = 'charset='
        for el in message.values():
            if charsetPrefix in el:
                parts = el.split(';')
                for part in parts:
                    if charsetPrefix in part:
                        encoding = part.replace(charsetPrefix, '')
                        encoding.strip()
                        return encoding
        return None
                    


    def SendEmail(self, content):
        # Open the plain text file whose name is in textfile for reading.
        
        #mapp = map(bytes.decode, mail['mail'])
        

        self.Smtp = SMTP(self.connectionSettings['ip'])
        self.Smtp.login(self.connectionSettings['user'], self.connectionSettings['pass'])
        

        #Create a text/plain message
        msg = EmailMessage()
        msg.set_content(content)

        # me == the sender's email address
        # you == the recipient's email address
        msg['Subject'] = 'Counter'
        msg['From'] = self.user
        msg['To'] = self.user

        # Send the message via our own SMTP server.
        #self.Smtp.connect()
        self.Smtp.send_message(msg)
        self.Smtp.quit()