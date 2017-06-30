import logging
from ThreadEngine import Engine

import MongoDatabase

if __name__ == "__main__":
    import logging
    import settings
    #logging.basicConfig(filename='/home/tomek/deamon.log', level=logging.WARNING, format='%(asctime)s %(message)s', datefmt='%d/%m/%Y %H:%M:%S %p')
    settings.init()
    
    mongo = MongoDatabase.MongoTB()
    emails = mongo.email_binary_db.find()
    for e in emails:
        print(e)
