import getpass, poplib
import xml.etree.ElementTree as ET
import re
import os
from datetime import datetime as DATETIME
import logging
from poplib import error_proto
import email
import sys

from Service import *

class EmailParserV2:
    
    def __init__(self, settings):
        self.xml_loader = XMLLoader(settings)
        pass

    def ParseEmailToMachineRecord(self, mail):

        parsingResults = {
            'sucess': True, 
            'parsingErrorMessage': [],
            'parsedEmailBody': None,
            'record': None,
            'binaryBody': None
        }

        if mail is None:
            parsingResults['sucess'] = False
            return parsingResults

        try:
            parsedEmail = self.parse(mail)
        except ServerException:
            parsingResults['sucess'] = False
            return parsingResults

        parsingResults['parsedEmailBody'] = parsedEmail['body']
        parsingResults['binaryBody'] = parsedEmail['body-binary']

        signature = self.get_signature_number(parsedEmail)

        if signature is None:
            parsingResults['sucess'] = False
            return parsingResults

        logging.info('Znaleziono sygnature maila: {}'.format(signature))

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
            "parsed_by_email" : True
        }

        # Wczytywanie wartośći regexów z XMLa
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
            logging.error('Błąd przy wczytywaniu regexa. Email parser. %s', e)
            logging.exception('Błąd w czytywaniu regexa. Sprawdź to :)')
            parsingResults['sucess'] = False
            parsingResults['parsingErrorMessage'].append('Błąd w czytywaniu regexa. Sprawdź to :)')
            return parsingResults

        data = parsedEmail['body']

        # Parsowanie body
        try:
            date_time = self.parse_using_regex(data, datetime_regex_group[0])
            serialnumber = self.parse_using_regex(data, serial_number_regex_group[0])
            scancounter = self.addition_regex_group(data, scan_counter_regex_group)
            printcounter = self.addition_regex_group(data, print_counter_regex_group)
            printcountercolor = self.addition_regex_group(data, print_counter_color_regex_group)
        except Exception as e:
            logging.error('Błąd krytyczny przy parsowaniu regexa. %s', e)
            parsingResults['parsingErrorMessage'].append('Błąd przy parsowaniu "BODY"')
            parsingResults['sucess'] = False
            return parsingResults

        tonerc = ''
        tonerm = ''
        tonery = ''
        tonerk = ''

        #sprawdzanie tonerów, jeśli nie ma w pliku xml to omijamy.
        try:
            if len(tonerc_regex_group) > 0:
                tonerc = self.parse_using_regex(data, tonerc_regex_group[0])
                if tonerc_regex_group[0][3] == 'true' and tonerc is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów C.')
            if len(tonerm_regex_group) > 0:
                tonerm = self.parse_using_regex(data, tonerm_regex_group[0])
                if tonerm_regex_group[0][3] == 'true' and tonerm is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów M.')
            if len(tonery_regex_group) > 0:
                tonery = self.parse_using_regex(data, tonery_regex_group[0])
                if tonery_regex_group[0][3] == 'true' and tonery is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów Y.')
            if len(tonerk_regex_group) > 0:
                tonerk = self.parse_using_regex(data, tonerk_regex_group[0])
                if tonerk_regex_group[0][3] == 'true' and tonerk is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów K.')
        except Exception as e:
            logging.error('Błąd krytyczny przy wczytywaniu stanu tonerów.')
        

        if datetime_regex_group[0][3] == 'true' and date_time is None:
            parsingResults['sucess'] = False
            parsingResults['parsingErrorMessage'].append('Brakuje daty.')

        if serial_number_regex_group[0][3] == 'true' and serialnumber is None:
            parsingResults['sucess'] = False
            parsingResults['parsingErrorMessage'].append('Brakuje numeru seryjnego.')

        if scan_counter_regex_group[0][3] == 'true' and scancounter is None:
            parsingResults['sucess'] = False
            parsingResults['parsingErrorMessage'].append('Brakuje licznkika skanerów.')

        if print_counter_regex_group[0][3] == 'true' and printcounter is None:
            parsingResults['sucess'] = False
            parsingResults['parsingErrorMessage'].append('Brakuje licznika wydruków czarnobiałych.')

        if len(print_counter_color_regex_group) > 0:
            if len(print_counter_color_regex_group[0]) > 2:
                if print_counter_color_regex_group[0][3] == 'true' and printcountercolor is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Brakuje licznika kolorowych wydruków')
                elif print_counter_color_regex_group[0][3] == 'false' and printcountercolor is None:
                    printcountercolor = 0
        else:
            printcountercolor = 0

        try:
            printer_data['datetime'] = DATETIME.strptime(date_time, self.xml_loader.get_datetime_format(signature))
            printer_data['serial_number'] = serialnumber
            printer_data['scan_counter'] = scancounter
            printer_data['print_counter_black_and_white'] = printcounter
            printer_data['print_counter_color'] = printcountercolor
            printer_data['description'] = 'parsed from email server. Signature: ' + str(signature) + ' EMail ID: ' + str(mail['_id'])
            printer_data['email_info'] = mail['_id']
            printer_data['tonerlevel_c'] = tonerc
            printer_data['tonerlevel_m'] = tonerm
            printer_data['tonerlevel_y'] = tonery
            printer_data['tonerlevel_k'] = tonerk
        except ValueError as error:
            logging.info('Jest problem z parsowaniem. Mail: %s', mail['_id'])
            parsingResults['sucess'] = False
            message = 'Value Error' + str(error)
            parsingResults['parsingErrorMessage'].append(message)

        logging.debug('Parsed data:')
        logging.debug(printer_data)
        logging.debug(parsingResults)

        parsingResults['record'] = printer_data

        return parsingResults

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

    def get_signature_number(self, parsedMail):
        try:
            signatures = self.xml_loader.get_all_signature()

            for sig_group in range(signatures.__len__()):
                signature_find = 0
                for single_signature in signatures[sig_group]:
                    for line in parsedMail['body-lines']:
                        match = re.findall(single_signature, line)
                        if len(match) > 0:
                            signature_find += 1
                            break
                if signature_find == len(signatures[sig_group]):
                    return sig_group
        except ServerException as se:
            logging.info('%s', se)
        except Exception as e:
            logging.error('Błąd przy szukaniu sygnatury. EmailParser %s', e)
        return None

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
            logging.warning('Błąd przy parsowaniu regexow z sumowaniem wartosci: %s', e)
        # except:
        #     logging.warning(def addition_regex_group(self, data, reggroup_s))

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
            logging.warning('Błąd przy parsowaniu regexow z sumowaniem wartosci: %s', e)
        except:
            logging.warning('Błąd przy parsowaniu regexow z sumowaniem wartosci: %s', sys.exc_info()[0])


    def parse(self, mail):
        body = None
        encoding = self.get_encoding(mail['mail'])

        # Dane binarne dla windowsa. Tam nowa linia powinna zaczynać się od \r\n
        byte_message = b'\n'.join(mail['mail'])
        
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
            'body': body,
            'body-lines': lines,
            'body-binary': byte_message
        }
        # mail['body'] = body
        # mail['body-lines'] = lines

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