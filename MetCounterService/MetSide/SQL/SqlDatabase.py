from sqlalchemy.ext.automap import automap_base
from sqlalchemy.orm import Session
from sqlalchemy import create_engine
from sqlalchemy import inspect

class TBSQL:

    def __init__(self):
        pass

    def Connect(self):
        self.Base = automap_base()
        self.Engine = create_engine("mssql+pyodbc://Superuser:1234567890@WIN-RP56U0UJDMQ/MetCounterService?driver=SQL+Server+Native+Client+11.0")

        # reflect the tables
        self.Base.prepare(self.Engine, reflect=True, schema='Queue')
        
        inspector = inspect(self.Engine)

        for el in self.Base.classes.keys():
            print(el)

        # Get table information
        print(inspector.get_table_names())

        self.QueueEmail = self.Base.classes.Email


        ## mapped classes are now created with names by default
        ## matching that of the table name.
        #MachineRecords = Base.classes.MachineRecord
        

        #session = Session(engine)

        ## rudimentary relationships are produced
        #session.add(Address(email_address="foo@bar.com", user=User(name="foo")))
        #session.commit()

        ## collection-based relationships are by default named
        ## "<classname>_collection"
        #print (u1.address_collection)
        print();

    def ImportEmailToQueue(self, contentToInsert):
        session = Session(self.Engine)
        session.add(self.QueueEmail(Content=contentToInsert))
        session.commit()
        session.close()

    def GetEmailsToParse(self):
        session = Session(self.Engine)
        return session.query(self.QueueEmail)
        