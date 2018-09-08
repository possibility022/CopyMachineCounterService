import xml.etree.ElementTree as ET

class XMLLoader:

    def __init__(self, settings):
        workfolder = settings.workfolder
        path = workfolder + '/test/emailparser.xml'
        self.tags = {'signature': 'signature', 'datetime': 'datetime','print': 'regprintcounter', 'printcolor': 'regprintcountercolor', 'scan': 'regscancounter', 'serial': 'regserialnumber', 'tonerc': 'regtonerlevelc', 'tonerm': 'regtonerlevelm', 'tonery': 'regtonerlevely', 'tonerk':'regtonerlevelk'}

        self.tree = ET.parse(path)
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