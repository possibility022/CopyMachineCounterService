import string
import random
import logging
import settings
import os.path

settings.init()


class Database:

    key_len = 20

    @staticmethod
    def getnewid():
        workfolder = settings.workfolder
        key = ''.join(random.choice(string.ascii_uppercase + string.digits) for _ in range(Database.key_len))
        ids = []
        f = open(workfolder + '/ids.list', 'r')
        line = f.readline()
        while line != '':
            ids.append(line)
            line = f.readline()

        if key in ids:
            return Database.getnewid()
        else:
            return key

    @staticmethod
    def getthread_pause_value_email():
        file = settings.workfolder + '/working_threads.cfg'
        if os.path.isfile(file):
            f = open(file, 'r')
            value = f.readline()
            f.close()
            if value == 'true':
                return True
            else:
                return False
        else:
            logging.error('There is no file: ' + file)
        return False

    @staticmethod
    def getthread_pause_value_file():
        file = settings.workfolder + '/working_threads.cfg'
        if os.path.isfile(file):
            f = open(file, 'r')
            value = f.readline()
            value = f.readline()
            f.close()
            if value == 'true':
                return True
            else:
                return False
        else:
            logging.error('There is no file: ' + file)
        return False