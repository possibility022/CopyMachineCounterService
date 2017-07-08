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
