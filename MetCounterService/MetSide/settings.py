import logging

def init():
    global workfolder
    global GlobalMongoDatabaseAddress
    global LocalMongoDatabaseAddress

    workfolder = 'D:\\Programowanie\\C#2017\\LicznikMetService\\MetCounterService\\MetSide'
    GlobalMongoDatabaseAddress = '***REMOVED***'
    #LocalMongoDatabaseAddress = '192.168.1.246' # MET
    LocalMongoDatabaseAddress = '192.168.0.42' # HOME
