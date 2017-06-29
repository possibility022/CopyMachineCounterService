from pymongo import MongoClient
from pymongo import errors
from TBExceptions import ServerException
import logging
import settings

settings.init()


class MongoTB_Global:

    def __init__(self):
        workfolder = settings.workfolder

        # Serwer Globalny
        
        self.global_serverip = settings.GlobalMongoDatabaseAddress
        self.global_serverport = 2772
        self.global_database = 'copyinfo'
        self.global_fullrecorddata = 'full_data'
        
        self.global_client = MongoClient(self.global_serverip, self.global_serverport)
        self.global_db = self.global_client[self.global_database]
        self.global_db.authenticate('***REMOVED***', '***REMOVED***#121#')
        self.global_fulldata = self.global_db[self.global_fullrecorddata]

        self.decoded_dir = workfolder + '/decoded'

    def import_fulldata(self, data):
        try:
            document = {'data':data}
            self.global_fulldata.insert_one(document)
            return True
        except Exception as ex:
            #logging.log("Jakiś problem z importem FullData do bazy danych %s", ex)
            logging.exception("Error!")
            return False

