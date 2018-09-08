import logging

def init():
    global workfolder
    global GlobalMongoDatabaseAddress
    global LocalMongoDatabaseAddress
    global AuthentiactionOn

    workfolder = 'D:\\Programowanie\\C#2017\\MetCounterService\\MetCounterService\\MetSide'
    GlobalMongoDatabaseAddress = '***REMOVED***'
    #LocalMongoDatabaseAddress = '192.168.1.246' # MET
    LocalMongoDatabaseAddress = '192.168.0.119' # HOME
    AuthentiactionOn = False

