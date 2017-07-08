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
        self.global_other = 'other'
        
        self.global_client = MongoClient(self.global_serverip, self.global_serverport)
        self.global_db = self.global_client[self.global_database]
        self.global_db.authenticate('***REMOVED***', '***REMOVED***#121#')
        self.global_fulldata = self.global_db[self.global_fullrecorddata]
        self.global_other_db = self.global_db[self.global_other]
        

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

    def import_mactoweb(self, data):
        try:
            document = {'key':'mactoweb','data':data}
            self.global_other_db.insert_one(document)
        except Exception as ex:
            logging.exception("Error inserting mactoweb!")

    def import_emailparser(self, data):
        try:
            document = {'key':'emailparser','data':data}
            self.global_other_db.insert_one(document)
        except Exception as ex:
            logging.exception("Error inserting mactoweb!")