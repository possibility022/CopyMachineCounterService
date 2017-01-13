import xml.etree.ElementTree as ET
import re
import logging
import settings
from datetime import datetime as time


settings.init()


class DataParser:

    delimiter_start = '#|$'
    delimiter_end = '$|#'

    def __init__(self, data):
        self.exception_in_parsing = False
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

        self.parse(data)

    def parse_single_data(self, data):
        prefix = self.getprefix(data)
        self.printer_data[prefix] = data[len(prefix) + 2:]

    def parse_using_regex(self, data, reggroup):
        order = int(reggroup[0])
        group = int(reggroup[2])
        alldata = re.findall(reggroup[1], data)
        return alldata[order][group]

    def parse_counter(self):
        regcollection = XMLLoader.get_printer_counter(self.printer_data['addressMAC'])
        counter = 0
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_counter'], regexgroup)
            parsed = parsed.replace(' ', '')
            counter += int(parsed)
        self.printer_data['print_counter_black_and_white'] = counter

    def parse_counter_color(self):
        regcollection = XMLLoader.get_printer_counter_color(self.printer_data['addressMAC'])
        counter = 0
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_counter'], regexgroup)
            parsed = parsed.replace(' ', '')
            counter += int(parsed)
        self.printer_data['print_counter_color'] = counter

    def parse_counter_scaner(self):
        regcollection = XMLLoader.get_scaner_counter(self.printer_data['addressMAC'])
        counter = 0
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_counter'], regexgroup)
            parsed = parsed.replace(' ', '')
            counter += int(parsed)
        self.printer_data['scan_counter'] = counter

    def parse_serial_number(self):
        regcollection = XMLLoader.get_serialnumber(self.printer_data['addressMAC'])
        ser = ''
        for regexgroup in regcollection:
            parsed = self.parse_using_regex(self.printer_data['full_serialnumber'], regexgroup)
            parsed = parsed.replace(' ', '')
            ser += parsed
        self.printer_data['serial_number'] = ser

    def parse_datetime(self):
        #26.09.2016 09:44
        data = self.printer_data['datetime']
        time_format = '%d.%m.%Y %H:%M'
        self.printer_data['datetime'] = time.strptime(data, time_format)


    def parse(self, data):
        
        try:
            while data.startswith(self.delimiter_start):
                data = data[3:]

            while data.endswith(self.delimiter_end):
                data = data[:-3]

            tablica = data.split(self.delimiter_end + self.delimiter_start)

            for single_data in tablica:
                self.parse_single_data(single_data)

            self.parse_counter_color()
            self.parse_counter()
            self.parse_counter_scaner()
            self.parse_serial_number()
            self.parse_datetime()
        except IndexError:
            logging.info('Index Error in parse(data) in Parser.')
            self.exception_in_parsing = True
        except:
            logging.info('Some error in parse(data) in Parser.')
            self.exception_in_parsing = True

    @staticmethod
    def getprefix(string):
        if string.index('|') != 0:
            return None

        ending = string.index('|', 2)

        prefix = string[1:ending]
        return prefix


class XMLLoader:
    workfolder = settings.workfolder
    tree = ET.parse(workfolder + '/test/mactoweb.xml')
    root = tree.getroot()

    xml_data_tag = {'print': 'regprintcounter', 'printcolor': 'regprintcountercolor', 'scan': 'regscancounter', 'serial': 'regserialnumber'}

    @staticmethod
    def get_printer_counter(mac):
        return XMLLoader.__get_regex(mac, 'print')

    @staticmethod
    def get_scaner_counter(mac):
        return XMLLoader.__get_regex(mac, 'scan')

    @staticmethod
    def get_serialnumber(mac):
        return XMLLoader.__get_regex(mac, 'serial')

    @staticmethod
    def get_printer_counter_color(mac):
        return XMLLoader.__get_regex(mac, 'printcolor')

    @staticmethod
    def __get_regex(mac, xml_tag):
        for devicegroup in XMLLoader.root:
            for element in devicegroup:
                text = element.text.strip()
                if text == mac:
                    return XMLLoader.__collect_regex_data(element, xml_tag)

            for element in devicegroup:
                text = element.text.strip()
                if text == mac[:8]:
                    return XMLLoader.__collect_regex_data(element, xml_tag)
        return []

    @staticmethod
    def __collect_regex_data(element, xml_tag):
        data_to_download_col = []
        for child in element:
            if child.tag == XMLLoader.xml_data_tag[xml_tag]:
                order = child.attrib['inorder']
                regex = child.text
                group = child.attrib['group']
                trio = [order, regex, group]
                data_to_download_col.append(trio)
        return data_to_download_col

