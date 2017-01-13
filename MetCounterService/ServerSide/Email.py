import getpass, poplib
from MongoDatabase import MongoTB
import xml.etree.ElementTree as ET
import re
import os
from datetime import datetime as DATETIME
import logging
import settings
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

    # def load_messages(self):
    #     self.msgcount = len(self.Mailbox.list()[1])
    #
    #     for i in range(self.msgcount - 1):
    #         full_msg = []
    #         doc = self.Mailbox.retr(i + 1)
    #         msg_id = self.Mailbox.uidl(i + 1)
    #
    #         header = self.get_header(i + 1)
    #
    #         encoding = EmailParser.get_encoding(doc[1])
    #
    #         mail = {'mail': [], '_id': ''}
    #         mail['mail'] = doc[1]
    #         mail['_id'] = msg_id
    #         self.mongo.insert_email(mail)

        #     for index in range(header[1].__len__(), doc[1].__len__()):
        #         if isinstance(doc[1][index], bytes):
        #             try:
        #                 decoded = doc[1][index].decode(encoding)
        #             except UnicodeDecodeError:
        #                 decoded = str(doc[1][index])
        #             full_msg.append(decoded)
        #
        #     print('Saving:', msg_id)
        #
        #     f = open(self.messagespath + msg_id.decode('utf-8'), 'wb')
        #     for line in doc[1]:
        #         f.write(line + b'\n')
        #     f.close()
        #
        # files = os.listdir(self.messagespath_binary)
        # for el in files:
        #     f = open(self.messagespath_binary + el, 'rb')
        #     all = f.read()
        #     f.close()
        #     encoding = self.get_encoding(all)
        #     print(encoding)

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

    def parse(self, email):
        encoding = self.get_encoding(email['mail'])
        lines = []
        for line in email['mail']:
            try:
                line = line.decode(encoding)
            except UnicodeDecodeError:
                line = str(line)
            except LookupError:
                line = str(line)
            lines.append(line)

        email['mail'] = lines
        return email

    def get_signature_number(self, mail):
        mail = self.parse(mail)
        signatures = self.xml_loader.get_all_signature()

        for sig_group in range(signatures.__len__()):
            signature_find = 0
            for single_signature in signatures[sig_group]:
                for line in mail['mail']:
                    match = re.findall(single_signature, line)
                    if len(match) > 0:
                        signature_find += 1
            if signature_find == len(signatures[sig_group]):
                return sig_group

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
            "tonerlevel_c": None,
            "tonerlevel_m": None,
            "tonerlevel_y": None,
            "tonerlevel_k": None,
            "email_info": b''
        }

        signature = self.get_signature_number(mail)
        logging.info('Znaleziono sygnature maila: {}'.format(signature))

        if signature is None:
            self.mongo.move_mail_parsed('passed', mail)
            return

        datetime_regex_group = self.xml_loader.get_datetime(signature)
        serial_number_regex_group = self.xml_loader.get_serialnumber(signature)
        scan_counter_regex_group = self.xml_loader.get_scaner_counter(signature)
        print_counter_regex_group = self.xml_loader.get_printer_counter(signature)
        print_counter_color_regex_group = self.xml_loader.get_printer_counter_color(signature)
        tonerc_regex_group = self.xml_loader.get_tonerlevel_c(signature)
        tonerm_regex_group = self.xml_loader.get_tonerlevel_m(signature)
        tonery_regex_group = self.xml_loader.get_tonerlevel_y(signature)
        tonerk_regex_group = self.xml_loader.get_tonerlevel_k(signature)

        data = ''
        for line in mail['mail']:
            data += '\n' + line

        date_time = self.parse_using_regex(data, datetime_regex_group[0])
        serialnumber = self.parse_using_regex(data, serial_number_regex_group[0])
        scancounter = self.addition_regex_group(data, scan_counter_regex_group)
        printcounter = self.addition_regex_group(data, print_counter_regex_group)
        printcountercolor = self.addition_regex_group(data, print_counter_color_regex_group)
        print(tonerc_regex_group)

        tonerc = None
        tonerm = None
        tonery = None
        tonerk = None

        #sprawdzanie tonerów, jeśli nie ma wpliku xml to omijamy.
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
        elif print_counter_color_regex_group[0][3] == 'true' and printcountercolor is None:
            self.mongo.move_mail_parsed('fail', mail)
            return

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
        return printer_data

    @staticmethod
    def parse_using_regex(data, reggroup):
        order = int(reggroup[0])
        group = int(reggroup[2])
        all_data = re.findall(reggroup[1], data)
        try:
            to_return = all_data[order][group]
        except:
            to_return = None
        return to_return

    def addition_regex_group(self, data, reggroup_s):
        value = 0
        for reg_group in reggroup_s:
            parsed = self.parse_using_regex(data, reg_group)
            if parsed is not None:
                value += int(parsed)
            else:
                return None

        if value != 0:
            return value

    def close(self):
        try:
            self.Mailbox.quit()
        except:
            logging.debug('Błąd przy zamykaniu mailboxa')


# eparser = EmailParser()
# s = eparser.load_messages()
# eparser.close()
