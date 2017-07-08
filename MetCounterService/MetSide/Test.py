import logging
from ThreadEngine import Engine
from datetime import datetime, timedelta, date, time

import settings
import MongoDatabase

settings.init()
#if __name__ == "__main__":
#    import logging
#    import settings
#    #logging.basicConfig(filename='/home/tomek/deamon.log', level=logging.WARNING, format='%(asctime)s %(message)s', datefmt='%d/%m/%Y %H:%M:%S %p')
#    settings.init()
    
#    mongo = MongoDatabase.MongoTB()
#    emails = mongo.email_binary_db.find()
#    for e in emails:
#        print(e)

todaymidnight = datetime.today()


last_file_update = date.today() - timedelta(days=1)

print(time(0,30))

print(time(0,30) <= datetime.now().time() <= time(19,30))

if last_file_update < date.today():
    if time(0,30) <= datetime.now().time() <= time(1,30):
        #data = None #self.mongo.global_get_emailparser()
        if data is not None:
            f = open(settings.workfolder + '/test/emailparser.xml', 'w')
            f.write(data)
            f.close()
        data = None #self.mongo.global_get_mactoweb()
        if data is not None:
            f = open(settings.workfolder + '/test/mactoweb.xml', 'w')
            f.write(data)
            f.close()
        last_file_update = date.today()
        logging.info('zaktualizowano pliki mactoweb i emailparser')
