from sqlalchemy.ext.automap import automap_base
from sqlalchemy.orm import Session
from sqlalchemy import create_engine
from sqlalchemy import inspect

class TBSQL:

    def __init__(self):
        pass

    def Connect(self):
        self.Engine = create_engine("mssql+pyodbc://Superuser:1234567890@WIN-RP56U0UJDMQ/MetCounterService?driver=SQL+Server+Native+Client+11.0")
        
        # reflect the Queue tables
        self.Queue = automap_base()
        self.Queue.prepare(self.Engine, reflect=True, schema='Queue')

        for el in self.Queue.classes.keys():
            print(el) # TODO logging
        
        self.QueueEmail = self.Queue.classes.Email

        # reflect te Machine tables
        self.Machine = automap_base()
        self.Machine.prepare(self.Engine, reflect=True, schema='Machine')

        for el in self.Machine.classes.keys():
            print(el) # TODO logging

        self.MachineRecord = self.Machine.classes.Record
        self.MachineServiceSourceCounters = self.Machine.classes.ServiceSourceCounters
        self.MachineServiceSourceSerialNumber = self.Machine.classes.ServiceSourceSerialNumber
        self.MachineEmailSource = self.Machine.classes.EmailSource

        self.Warehouse = automap_base()
        self.Warehouse.prepare(self.Engine, reflect=True, schema='Warehouse')
        self.WarehouseEmailSource = self.Warehouse.classes.EmailSource

        for el in self.Warehouse.classes.keys():
            print(el) # TODO logging

    def ImportEmailToQueue(self, contentToInsert):
        session = Session(self.Engine)
        session.add(self.QueueEmail(Content=contentToInsert))
        session.commit()
        session.close()

    def GetEmailsToParse(self):
        session = Session(self.Engine)
        records = session.query(self.QueueEmail)
        session.close()
        return records

    def GetAll(self, table):
        session = Session(self.Engine)
        records = session.query(table).all()
        session.close()
        return records


    def InsertMachineRecord(self, recordData, binaryBody):
        session = Session(self.Engine)
        entity = self._mapFromDict(recordData)
        entity.emailsource = self.MachineEmailSource(
            Content = binaryBody
        )
        session.add(entity)
        session.commit()
        session.close()

    def InsertWarehouseEmail(self, binaryBody):
        session = Session(self.Engine)
        entity = self.WarehouseEmailSource(Content = binaryBody)
        session.add(entity)
        session.commit()
        session.close()

    def InsertMachineRecord_HTML(self, recordData, serialNumberSource, countersSource):
        session = Session(self.Engine)
        
        entity = self._mapFromDict(recordData)
        
        entity.servicesourcecounters = self.MachineServiceSourceCounters(
            Content = countersSource
        )

        entity.servicesourceserialnumber = self.MachineServiceSourceSerialNumber(
            Content = serialNumberSource
        )

        session.add(entity)
        session.commit()
        session.close()

    def _mapFromDict(self, data):

        recordEntity = self.MachineRecord(
            CounterBlackAndWhite = self._getFromDictIfExists(data,'print_counter_black_and_white'),
            CounterColor = self._getFromDictIfExists(data,'print_counter_color'),
            CounterScanner = self._getFromDictIfExists(data,'scan_counter'),
            Description = self._getFromDictIfExists(data,'description'),
            AddressIp = self._getFromDictIfExists(data,'addressIP'),
            ReadDatetime = self._getFromDictIfExists(data,'datetime'),
            SerialNumber = self._getFromDictIfExists(data,'serial_number'),
            TonerLevelBlack = self._getFromDictIfExists(data,'tonerlevel_k'),
            TonerLevelCyan = self._getFromDictIfExists(data,'tonerlevel_c'),
            TonerLevelYellow = self._getFromDictIfExists(data,'tonerlevel_y'),
            TonerLevelMagenta = self._getFromDictIfExists(data,'tonerlevel_m'),
            AddressMac = self._getFromDictIfExists(data,'addressMAC')
        )

        return recordEntity
    
    @staticmethod
    def _getFromDictIfExists(dictionary, key):
        if key in dictionary:
            return dictionary[key]
        else:
            return None
        