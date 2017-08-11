import logging
from ThreadEngine import Engine

from Parser import DataParser
import MongoDatabase


def testFileConvert(filepath, mongo_database):
    try:
        f = open(filepath, 'r').read()

        device = DataParser(f)
        sucess = mongo_database.import_to_database(device)
        if not sucess:
            mongo_database.global_import_fulldatafaild(f)
    except:
        logging.error('Krytyczny blad w przetwarzaniu zdalnych raportow HTML')
        logging.exception('Error!')


if __name__ == "__main__":
    import settings
    settings.init()
    
    mongo = MongoDatabase.MongoTB()


    # Testing File Parse
    path = 'D:\TMP\messages_20170809-0819170'
    testFileConvert(path, mongo)


