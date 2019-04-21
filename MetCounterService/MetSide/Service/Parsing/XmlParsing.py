import xml.etree.ElementTree as ET

class XMLLoader:

    def __init__(self, filePath):
        self.tags = {'signature': 'signature', 'datetime': 'datetime','print': 'regprintcounter', 'printcolor': 'regprintcountercolor', 'scan': 'regscancounter', 'serial': 'regserialnumber', 'tonerc': 'regtonerlevelc', 'tonerm': 'regtonerlevelm', 'tonery': 'regtonerlevely', 'tonerk':'regtonerlevelk'}

        self.tree = ET.parse(filePath)
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
                if el.tag == self.tags['signature']:
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


class XMLLoaderForHTML:

    def __init__(self, filePath):
        self.tree = ET.parse(filePath)
        self.root = self.tree.getroot()

        self.xml_data_tag = {'print': 'regprintcounter', 'printcolor': 'regprintcountercolor', 'scan': 'regscancounter', 'serial': 'regserialnumber'}

    def get_printer_counter(self, mac):
        return self.__get_regex(mac, 'print')

    def get_scaner_counter(self, mac):
        return self.__get_regex(mac, 'scan')

    def get_serialnumber(self, mac):
        return self.__get_regex(mac, 'serial')

    def get_printer_counter_color(self, mac):
        return self.__get_regex(mac, 'printcolor')

    def __get_regex(self, mac, xml_tag):
        for devicegroup in self.root:
            for element in devicegroup:
                text = element.text.strip()
                if text == mac:
                    return self.__collect_regex_data(element, xml_tag)

            for element in devicegroup:
                text = element.text.strip()
                if text == mac[:8]:
                    return self.__collect_regex_data(element, xml_tag)
        return []

    def __collect_regex_data(self, element, xml_tag):
        data_to_download_col = []
        for child in element:
            if child.tag == self.xml_data_tag[xml_tag]:
                order = child.attrib['inorder']
                regex = child.text
                group = child.attrib['group']
                trio = [order, regex, group]
                data_to_download_col.append(trio)
        return data_to_download_col