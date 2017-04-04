import getpass, poplib
from MongoDatabase import MongoTB
import xml.etree.ElementTree as ET
import re
import os
from datetime import datetime as DATETIME
import logging
import settings
import email
# MONGO EMAIL DOCUMENT STYLE
# mail = {'mail': [], '_id': ''}

settings.init()


class XMLLoader:

    workfolder = settings.workfolder
    path = workfolder + '/test/emailparser.xml'
    tags = {'signature': 'signature', 'datetime': 'datetime','print': 'regprintcounter', 'printcolor': 'regprintcountercolor', 'scan': 'regscancounter', 'serial': 'regserialnumber', 'tonerc': 'regtonerlevelc', 'tonerm': 'regtonerlevelm', 'tonery': 'regtonerlevely', 'tonerk':'regtonerlevelk'}

    def __init__(self):
        self.tree = ET.parse(self.path)
        self.root = self.tree.getroot()

    def get_printer_counter(self, which):
        return self.__get_regex(which, 'print')

    def get_scaner_counter(self, which):
        return self.__get_regex(which, 'scan')

    def get_serialnumber(self, which):
        return self.__get_regex(which, 'serial')

    def get_printer_counter_color(self, which):
        return self.__get_regex(which, 'printcolor')

    def get_datetime(self, which):
        return self.__get_regex(which, 'datetime')

    def get_datetime_format(self, which):
        return self.__get_datetime_format(which, 'datetime')

    def get_tonerlevel_c(self, which):
        return self.__get_regex(which, 'tonerc')

    def get_tonerlevel_m(self, which):
        return self.__get_regex(which, 'tonerm')

    def get_tonerlevel_y(self, which):
        return self.__get_regex(which, 'tonery')

    def get_tonerlevel_k(self, which):
        return self.__get_regex(which, 'tonerk')

    def get_all_signature(self):
        signatures = []
        for device in self.root:
            part_of_signature = []
            for el in device:
                if el.tag == XMLLoader.tags['signature']:
                    regex = el.text
                    part_of_signature.append(regex)
            signatures.append(part_of_signature)

        return signatures

    def __get_datetime_format(self, which, xml_tag):
        for child in self.root[which]:
            if child.tag == self.tags[xml_tag]:
                format = child.attrib['format']
                return format

    def __get_regex(self, which, xml_tag):
        data_to_download_col = []
        for child in self.root[which]:
            if child.tag == self.tags[xml_tag]:
                order = child.attrib['inorder']
                regex = child.text
                group = child.attrib['group']
                required = child.attrib['require']
                quatro = [order, regex, group, required]
                data_to_download_col.append(quatro)

        return data_to_download_col


class EmailParser:

    def __init__(self):
        self.Mailbox = poplib.POP3('***REMOVED***', 110)
        self.Mailbox.user('***REMOVED***')
        self.Mailbox.pass_('***REMOVED***')
        self.msgcount = 0
        self.mongo = MongoTB()
        self.xml_loader = XMLLoader()

    def get_email_pop3(self, which):
        doc = self.Mailbox.retr(which)
        msg_id = self.Mailbox.uidl(which)
        mail = {'mail': doc[1], '_id': msg_id}
        self.insert_mail_to_binary(mail)
        return mail

    def del_email(self, which):
        self.Mailbox.dele(which)

    def get_emails_id(self):
        self.msgcount = len(self.Mailbox.list()[1])
        ids = []
        for i in range(self.msgcount - 1):
            msg_id = self.Mailbox.uidl(i + 1)
            ids.append(msg_id)
        return ids

    def get_header(self, which):
        return self.Mailbox.top(which, 0)

    def insert_mail_to_binary(self, mail):
        self.mongo.insert_email(mail)

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

    def get_emails_from_mongo(self):
        return self.mongo.get_emails()

    def check_email_parsed(self, id_):
        return self.mongo.check_id_parsed(id_)

    def parse(self, mail):
        encoding = self.get_encoding(mail['mail'])

        byte_message = b'\n'.join(mail['mail'])
        message = email.message_from_bytes(byte_message)

        for part in message.walk():
            if part.get_content_type() == 'text/plain':
                body = part.get_payload(decode=True)
                break   

        #body = message.get_payload(0).get_payload(decode=True)
        if encoding is not '':
            body = body.decode(encoding)
        else:
            body = body.decode('utf-8')

        if isinstance(body, str):
            lines = body.split('\n')
        elif isinstance(body,bytes):
            lines = body.split(b'\n')
        else:
            raise Exception('Email został niepoprawnie przetworzony')

        mail['body'] = body
        mail['body-lines'] = lines

        return mail

    def get_signature_number(self, mail):
        try:
            mail = self.parse(mail)
            signatures = self.xml_loader.get_all_signature()

            for sig_group in range(signatures.__len__()):
                signature_find = 0
                for single_signature in signatures[sig_group]:
                    for line in mail['body-lines']:
                        match = re.findall(single_signature, line)
                        if len(match) > 0:
                            signature_find += 1
                            break
                if signature_find == len(signatures[sig_group]):
                    return sig_group
        except Exception as e:
            logging.error('Błąd przy szukaniu sygnatury. EmailParser', e)
            return -1

    def parse_email_to_device_data(self, mail):
        printer_data = {
            "datetime": "",
            "description": "",
            "addressIP": "",
            "addressMAC": "",
            "serial_number": "",
            "full_serialnumber": "",
            "full_counter": "",
            "scan_counter": "",
            "print_counter_black_and_white": "",
            "print_counter_color": "",
            "tonerlevel_c": '',
            "tonerlevel_m": '',
            "tonerlevel_y": '',
            "tonerlevel_k": '',
            "email_info": b'',
            'parsed': False
        }

        signature = self.get_signature_number(mail)

        if signature == -1:
            return printer_data

        if signature is None:
            self.mongo.move_mail_parsed('passed', mail)
            return

        logging.info('Znaleziono sygnature maila: {}'.format(signature))

        try:
            datetime_regex_group = self.xml_loader.get_datetime(signature)
            serial_number_regex_group = self.xml_loader.get_serialnumber(signature)
            scan_counter_regex_group = self.xml_loader.get_scaner_counter(signature)
            print_counter_regex_group = self.xml_loader.get_printer_counter(signature)
            print_counter_color_regex_group = self.xml_loader.get_printer_counter_color(signature)
            tonerc_regex_group = self.xml_loader.get_tonerlevel_c(signature)
            tonerm_regex_group = self.xml_loader.get_tonerlevel_m(signature)
            tonery_regex_group = self.xml_loader.get_tonerlevel_y(signature)
            tonerk_regex_group = self.xml_loader.get_tonerlevel_k(signature)
        except Exception as e:
            logging.error('Błąd przy wczytywaniu regexa. Email parser.')
            return

        data = mail['body']        

        try:
            date_time = self.parse_using_regex(data, datetime_regex_group[0])
            serialnumber = self.parse_using_regex(data, serial_number_regex_group[0])
            scancounter = self.addition_regex_group(data, scan_counter_regex_group)
            printcounter = self.addition_regex_group(data, print_counter_regex_group)
            printcountercolor = self.addition_regex_group(data, print_counter_color_regex_group)
        except Exception as e:
            logging.error('Błąd krytyczny przy parsowaniu regexa')
            return

        tonerc = ''
        tonerm = ''
        tonery = ''
        tonerk = ''

        #sprawdzanie tonerów, jeśli nie ma w pliku xml to omijamy.
        try:
            if len(tonerc_regex_group) > 0:
                tonerc = self.parse_using_regex(data, tonerc_regex_group[0])
                if tonerc_regex_group[0][3] == 'true' and tonerc is None:
                    self.mongo.move_mail_parsed('fail', mail)
            if len(tonerm_regex_group) > 0:
                tonerm = self.parse_using_regex(data, tonerm_regex_group[0])
                if tonerm_regex_group[0][3] == 'true' and tonerm is None:
                    self.mongo.move_mail_parsed('fail', mail)
                    return
            if len(tonery_regex_group) > 0:
                tonery = self.parse_using_regex(data, tonery_regex_group[0])
                if tonery_regex_group[0][3] == 'true' and tonery is None:
                    self.mongo.move_mail_parsed('fail', mail)
                    return
            if len(tonerk_regex_group) > 0:
                tonerk = self.parse_using_regex(data, tonerk_regex_group[0])
                if tonerk_regex_group[0][3] == 'true' and tonerk is None:
                    self.mongo.move_mail_parsed('fail', mail)
                    return
        except Exception as e:
            logging.error('Błąd krytyczny przy wczytywaniu stanu tonerów.')
        

        if datetime_regex_group[0][3] == 'true' and date_time is None:
            self.mongo.move_mail_parsed('fail', mail)
            return
        elif serial_number_regex_group[0][3] == 'true' and serialnumber is None:
            self.mongo.move_mail_parsed('fail', mail)
        elif scan_counter_regex_group[0][3] == 'true' and scancounter is None:
            self.mongo.move_mail_parsed('fail', mail)
            return
        elif print_counter_regex_group[0][3] == 'true' and printcounter is None:
            self.mongo.move_mail_parsed('fail', mail)
            return

        if len(print_counter_color_regex_group) > 0:
            if len(print_counter_color_regex_group[0]) > 2:
                if print_counter_color_regex_group[0][3] == 'true' and printcountercolor is None:
                    self.mongo.move_mail_parsed('fail', mail)
                    return
        else:
            printcountercolor = 0

        printer_data['datetime'] = DATETIME.strptime(date_time, self.xml_loader.get_datetime_format(signature))
        printer_data['serial_number'] = serialnumber
        printer_data['scan_counter'] = scancounter
        printer_data['print_counter_black_and_white'] = printcounter
        printer_data['print_counter_color'] = printcountercolor
        printer_data['description'] = 'parsed from email from server. Signature: ' + str(signature) + 'EMail ID: ' + str(mail['_id'])
        printer_data['email_info'] = mail['_id']
        printer_data['tonerlevel_c'] = tonerc
        printer_data['tonerlevel_m'] = tonerm
        printer_data['tonerlevel_y'] = tonery
        printer_data['tonerlevel_k'] = tonerk

        logging.debug('Parsed data:')
        logging.debug(printer_data)

        self.mongo.move_mail_parsed('sucess', mail)
        printer_data['parsed'] = True
        return printer_data

    @staticmethod
    def parse_using_regex(data, reggroup):
        order = int(reggroup[0])
        group = int(reggroup[2])
        all_data = re.findall(reggroup[1], data)
        try:
            to_return = all_data[order][group]
        except Exception as e:
            to_return = None
        return to_return

    def addition_regex_group(self, data, reggroup_s):
        value = 0
        try:
            for reg_group in reggroup_s:
                parsed = self.parse_using_regex(data, reg_group)
                if parsed is not None:
                    value += int(parsed)
                else:
                    return None

            if value != 0:
                return value
        except Exception as e:
            logging.debug('Błąd przy parsowaniu regexow z sumowaniem wartosci: %s', e.__traceback__)

    def close(self):
        try:
            self.Mailbox.quit()
        except Exception as e:
            logging.debug('Błąd przy zamykaniu mailboxa.')


# eparser = EmailParser()
# s = eparser.load_messages()
# eparser.close()

