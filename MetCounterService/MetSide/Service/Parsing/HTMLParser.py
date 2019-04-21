import logging
import xml.etree.ElementTree as ET
import re
from datetime import datetime as time


class HTMLParser:

    delimiter_start = '#|$'
    delimiter_end = '$|#'

    addressIP_Regex = '\\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\b'

    def __init__(self, XMLLoaderForHTML):
        
        self.xmlLoader = XMLLoaderForHTML

    def parse_single_data(self, data):
        prefix = self.getprefix(data)
        self.printer_data[prefix] = data[len(prefix) + 2:]

    def parse_using_regex(self, data, reggroup):
        order = int(reggroup[0])
        group = int(reggroup[2])
        alldata = re.findall(reggroup[1], data)
        return alldata[order][group]

    def parse_counter(self):
        regcollection = self.xmlLoader.get_printer_counter(self.printer_data['addressMAC'])
        counter = 0
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_counter'], regexgroup)
            parsed = parsed.replace(' ', '')
            counter += int(parsed)
        self.printer_data['print_counter_black_and_white'] = counter

    def parse_counter_color(self):
        regcollection = self.xmlLoader.get_printer_counter_color(self.printer_data['addressMAC'])
        counter = 0
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_counter'], regexgroup)
            parsed = parsed.replace(' ', '')
            counter += int(parsed)
        self.printer_data['print_counter_color'] = counter

    def parse_counter_scaner(self):
        regcollection = self.xmlLoader.get_scaner_counter(self.printer_data['addressMAC'])
        counter = 0
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_counter'], regexgroup)
            parsed = parsed.replace(' ', '')
            counter += int(parsed)
        self.printer_data['scan_counter'] = counter

    def parse_serial_number(self):
        regcollection = self.xmlLoader.get_serialnumber(self.printer_data['addressMAC'])
        ser = ''
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_serialnumber'], regexgroup)
            parsed = parsed.replace(' ', '')
            ser += parsed

        self.printer_data['serial_number'] = ser
        if ser == '':
            self.appendErrorMessage('Serial number is empty.')


    def parse_datetime_format(self, time_format):
        #26.09.2016 09:44
        #ok, najprawdopodobniej jest błąd w kodzie usługi. Jest tam użyty format daty który jest obecnie na systemie. Więc w zależności od systemu delimiter może się zmieniać ; /. Należy to poprawić.
        data = self.printer_data['datetime']
        value = time.strptime(data, time_format)
        self.printer_data['datetime'] = value

    def parse_datetime(self):
        try:
            self.parse_datetime_format('%d.%m.%Y %H:%M')
            return
        except ValueError:
            pass
            #logging.error('Błąd przy parsowaniu daty. Problem usługi klienta? Format: %d.%m.%Y %H:%M')

        try:
            self.parse_datetime_format('%d-%m-%Y %H:%M')
            return
        except ValueError:
            pass
            #logging.error('Błąd przy parsowaniu daty. Problem usługi klienta? Format: %d-%m-%Y %H:%M')

    def match_ip_address(self):
        val = self.printer_data['addressIP']
        matches = re.findall(self.addressIP_Regex, val)
        if len(matches) > 1:
            self.appendErrorMessage('Znaleziono więcej niz jeden address ip.' + ' '.join(matches))
        elif len(matches) == 1:
            self.printer_data['addressIP'] = '.'.join(matches[0])
        elif len(matches) == 0:
            self.appendErrorMessage('Nie znaleziono adresu IP')

    def parse(self, data):
        
        self.parsingResults = {
                'sourceCounterHTML': None,
                'sourceSerialHTML': None,
                'sucess': False, 
                'parsingErrorMessage': [],
                'record': None
            }

        self.printer_data = {
            "datetime": "",
            "description": "",
            "addressIP": "",
            "addressMAC": "",
            "serial_number": "",
            "full_serialnumber": "",
            "full_counter": "",
            "scan_counter": "",
            "print_counter_black_and_white": "",
            "print_counter_color": ""}

        try:
            while data.startswith(self.delimiter_start):
                data = data[3:]

            while data.endswith(self.delimiter_end):
                data = data[:-3]

            tablica = data.split(self.delimiter_end + self.delimiter_start)

            for single_data in tablica:
                self.parse_single_data(single_data)

            self.match_ip_address()
            self.parse_counter_color()
            self.parse_counter()
            self.parse_counter_scaner()
            self.parse_serial_number()
            self.parse_datetime()
            #self.printer_data['parsed'] = True

        except IndexError:
            logging.info('Index Error in parse(data) in Parser.')
            self.appendErrorMessage('Index Error in parse(data) in Parser.')
            
        except Exception as e:
            logging.info('Some error in parse(data) in Parser.')
            self.appendErrorMessage('Some error in parse(data) in Parser.')
        
        finally:
            self.parsingResults['sourceCounterHTML'] = self.printer_data['full_counter']
            self.parsingResults['sourceSerialHTML'] = self.printer_data['full_serialnumber']
            self.printer_data.pop('full_serialnumber')
            self.printer_data.pop('full_counter')
            self.parsingResults['record'] = self.printer_data
            self.parsingResults['sucess'] = len(self.parsingResults['parsingErrorMessage']) == 0
        
        return self.parsingResults

    @staticmethod
    def getprefix(string):
        if string.index('|') != 0:
            return None

        ending = string.index('|', 2)

        prefix = string[1:ending]
        return prefix

    def appendErrorMessage(self, message):
        self.parsingResults['parsingErrorMessage'].append(message)