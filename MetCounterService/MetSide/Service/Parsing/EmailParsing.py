import xml.etree.ElementTree as ET
import re
import os
from datetime import datetime as DATETIME
import logging
import sys

from Service import *

class EmailParserV2:
    
    def __init__(self, xmlLoader):
        self.xml_loader = xmlLoader
        pass

    def ParseEmailToMachineRecord(self, mail):

        parsingResults = {
            'sourceEmail': mail,
            'sucess': True, 
            'parsingErrorMessage': [],
            'record': None
        }

        if mail is None:
            return self.GetFailedResutls(parsingResults)

        signature = self.get_signature_number(mail)

        if signature is None:
            return self.GetFailedResutls(parsingResults)

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
            "tonerlevel_k": ''
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

        # Parsowanie body
        try:
            date_time = self.parse_using_regex(mail['body'], datetime_regex_group[0])
            serialnumber = self.parse_using_regex(mail['body'], serial_number_regex_group[0])
            scancounter = self.addition_regex_group(mail['body'], scan_counter_regex_group)
            printcounter = self.addition_regex_group(mail['body'], print_counter_regex_group)
            printcountercolor = self.addition_regex_group(mail['body'], print_counter_color_regex_group)
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
                tonerc = self.parse_using_regex(mail['body'], tonerc_regex_group[0])
                if tonerc_regex_group[0][3] == 'true' and tonerc is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów C.')
            if len(tonerm_regex_group) > 0:
                tonerm = self.parse_using_regex(mail['body'], tonerm_regex_group[0])
                if tonerm_regex_group[0][3] == 'true' and tonerm is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów M.')
            if len(tonery_regex_group) > 0:
                tonery = self.parse_using_regex(mail['body'], tonery_regex_group[0])
                if tonery_regex_group[0][3] == 'true' and tonery is None:
                    parsingResults['sucess'] = False
                    parsingResults['parsingErrorMessage'].append('Wymagany jest poziom tonerów Y.')
            if len(tonerk_regex_group) > 0:
                tonerk = self.parse_using_regex(mail['body'], tonerk_regex_group[0])
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
            printer_data['description'] = 'parsed from email server. Signature: ' + str(signature) + ' EMail ID: ' + mail['id']
            printer_data['tonerlevel_c'] = tonerc
            printer_data['tonerlevel_m'] = tonerm
            printer_data['tonerlevel_y'] = tonery
            printer_data['tonerlevel_k'] = tonerk
        except ValueError as error:
            logging.info('Jest problem z parsowaniem. Mail: %s', mail['id'])
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
        
        if reggroup[1] == '' or reggroup[1] is None:
            return None

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

    def GetFailedResutls(self, parsingResults):
        parsingResults['sucess'] = False
        return parsingResults