from sqlalchemy.ext.automap import automap_base
from sqlalchemy.orm import Session
from sqlalchemy import create_engine
from sqlalchemy import inspect

class SqlRepository:

    def __init__(self):
        pass

    def connect(self):
        Base = automap_base()
        engine = create_engine("mssql+pyodbc://Superuser:1234567890@WIN-RP56U0UJDMQ/MetCounterService?driver=SQL+Server+Native+Client+11.0")

        # reflect the tables
        Base.prepare(engine, reflect=True, schema='Machine')
        
        inspector = inspect(engine)

        for el in Base.classes.keys():
            print(el)

        # Get table information
        print(inspector.get_table_names())


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