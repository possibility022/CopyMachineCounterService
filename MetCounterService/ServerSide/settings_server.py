import logging


def init():
    global workfolder
    global HOST
    global PORT
    global PORTMACTOWEBFILEDOWNLOAD
    global MongoDatabaseAddress

    # workfolder = 'V:'
    # MongoDatabaseAddress = '***REMOVED***'

    workfolder = '/home/over/metcounter'
    MongoDatabaseAddress = '127.0.0.1'

    HOST, PORT = "***REMOVED***", 9999
    PORTMACTOWEBFILEDOWNLOAD = 9998


