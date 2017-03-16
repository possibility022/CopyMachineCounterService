using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace Copyinfo.Database
{
    static class MongoTB
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;

        const string ipadress = "***REMOVED***";
        const string port = "2772";
        const string connectionString = "mongodb://" + ipadress + ":" + port;

        static string login, password, databaseName;

        private const string DATABASE_enc = "***REMOVED***";
        private const string LOGIN_enc = "***REMOVED***";
        private const string PASSWORD_enc = "***REMOVED***";

        private static MongoClient client;
        private static MongoServer server;
        private static MongoDatabase database;
        private static IMongoDatabase iDatabase;

        enum Collections
        {
            machine_records,
            full_counter,
            full_serial,
            emails_binary
        }

        enum CollectionsDeleted
        {
            machine_record_deleted,
            full_counter_deleted,
            full_serial_deleted
        }

        public static void Initialize()
        {
            login = Security.Encrypting.AES_Decrypt(LOGIN_enc);
            password = Security.Encrypting.AES_Decrypt(PASSWORD_enc);
            databaseName = Security.Encrypting.AES_Decrypt(DATABASE_enc);

            MongoUrl url = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(url);

            MongoCredential credentials = MongoCredential.CreateCredential(databaseName, login, password); //TODO encrypt this things
            List<MongoCredential> credentials_list = new List<MongoCredential>();
            credentials_list.Add(credentials);
            settings.Credentials = credentials_list;
            
            client = new MongoClient(settings);
            server = client.GetServer();

            //database = client.GetDatabase(DATABASE_NAME);
            database = server.GetDatabase(databaseName);

            iDatabase = client.GetDatabase(databaseName);

            _client = new MongoClient(settings);
            _database = _client.GetDatabase(databaseName);
        }

        #region GetCollection

        //static public List<Client> getAllClients()
        //{
        //    MongoCollection<Client> collection = database.GetCollection<Client>(Collections.clients.ToString());
        //    MongoCursor<Client> all = collection.FindAll();

        //    List<Client> list = new List<Client>();

        //    foreach (Client c in all)
        //    {
        //        list.Add(c);
        //    }

        //    return list;
        //}

        static internal List<MachineRecord> getAllReports()
        {
            MongoCollection<MachineRecord> collection = database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
            MongoCursor<MachineRecord> all = collection.FindAll();

            List<MachineRecord> list = new List<MachineRecord>();

            foreach ( MachineRecord m in all)
            {
                list.Add(m);
            }

            return list;
        }

        static public List<MachineRecord> getReports(string serial_number)
        {
            if (serial_number != null)
            {
                var collection = database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
                var entityQuery = Query<MachineRecord>.EQ(e => e.serial_number, serial_number);
                

                var members = collection.Find(entityQuery);
                List<MachineRecord> records = new List<MachineRecord>();
                foreach (MachineRecord rec in members)
                {
                    records.Add(rec);
                }

                return records;
            }

            return new List<MachineRecord>();
        }

        static internal MachineRecord getOneInMonth(string serial_number, DateTime month)
        {
            if (serial_number != null)
            {
                //var collection = database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
                //var filter = Builders<MachineRecord>.Filter.Gte(e => e.datetime, month) & Builders<MachineRecord>.Filter.Eq(e => e.serial_number, serial_number);
                //var entityQuery = Query<MachineRecord>(filter);

                var collection = _database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
                var filter = Builders<MachineRecord>.Filter.Gte(e => e.datetime, month) & Builders<MachineRecord>.Filter.Eq(e => e.serial_number, serial_number);

                IMongoCollection<MachineRecord> col;

                MachineRecord record = collection.Find<MachineRecord>(filter).FirstOrDefault();

                return record;
            }

            return null;
        }

        //static public List<Device> getAllDevices()
        //{
        //    MongoCollection<Device> collection = database.GetCollection<Device>(Collections.device.ToString());
        //    MongoCursor<Device> all = collection.FindAll();

        //    List<Device> list = new List<Device>();

        //    foreach (Device m in all)
        //    {
        //        list.Add(m);
        //    }

        //    return list;
        //}

        #endregion

        #region GetSetDocument

        #region Save

        //static public string SaveDevice(Device d)
        //{
        //    if (d != null)
        //    {
        //        if (DeviceExists(d))
        //            return "";
        //        ObjectId adressid = SaveAddress(d.getAddress());
        //        var col = database.GetCollection(Collections.device.ToString());
        //        d.instalation_address = adressid;
        //        var dev = d;
        //        WriteConcernResult res = col.Save(dev);
        //        Global.log("SaveDevice: " +  res.UpdatedExisting.ToString());
        //        return dev.serial_number;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        //static private ObjectId SaveAddress(Address d)
        //{
        //    if (d != null)
        //    {
        //        var col = database.GetCollection(Collections.addresses.ToString());
        //        var obj = d;
        //        WriteConcernResult res = col.Save(obj);
        //        Global.log("SaveAddress: " + res.UpdatedExisting.ToString());
        //        return obj.id;
        //    }
        //    else
        //    {
        //        return new ObjectId();
        //    }
        //}

        static private ObjectId SaveMachineRecord_Deleted(MachineRecord r)
        {
            if (r != null)
            {
                var col = database.GetCollection(CollectionsDeleted.machine_record_deleted.ToString());
                var obj = r;
                WriteConcernResult res = col.Save(obj);
                Global.log("SaveMachineRecord_Deleted: " + res.UpdatedExisting.ToString());
                return (ObjectId) obj.id;
            }
            else
            {
                return new ObjectId();
            }
        }

        static private ObjectId SaveFullHTMLSerial_Deleted(HTMLSerial r)
        {
            if (r != null)
            {
                var col = database.GetCollection(CollectionsDeleted.full_serial_deleted.ToString());
                var obj = r;
                WriteConcernResult res = col.Save(obj);
                Global.log("SaveFullHTMLSerial_Deleted: " + res.UpdatedExisting.ToString());
                return (ObjectId)obj.id;
            }
            else
            {
                return new ObjectId();
            }
        }

        static private ObjectId SaveFullHTMLCounter_Deleted(HTMLCounter r)
        {
            if (r != null)
            {
                var col = database.GetCollection(CollectionsDeleted.full_counter_deleted.ToString());
                var obj = r;
                WriteConcernResult res = col.Save(obj);
                Global.log("SaveFullHTMLCounter_Deleted: " + res.UpdatedExisting.ToString());
                return (ObjectId)obj.id;
            }
            else
            {
                return new ObjectId();
            }
        }

        //static public string SaveClient(Client c)
        //{
        //    if (c != null)
        //    {
        //        c.address = SaveAddress(c.getAddress());
        //        var col = database.GetCollection(Collections.clients.ToString());
        //        var obj = c;
        //        WriteConcernResult res = col.Save(obj);
        //        Global.log("SaveClient: " + res.UpdatedExisting.ToString());
        //        return obj.id;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        #endregion

        #region Get

        //static public Client getClient(string nip_id)
        //{
        //    if (nip_id != null)
        //    {
        //        var col = database.GetCollection<Client>(Collections.clients.ToString());
        //        var query = Query<Device>.EQ(e => e.serial_number, nip_id);
        //        var members = col.Find(query);

        //        foreach (Client d in members)
        //            return d;
        //    }
        //    return new Client();
        //}

        //static public List<Device> getDevices(string[] serialnumbers)
        //{
        //    List<Device> devices = new List<Device>();
        //    if (serialnumbers != null)
        //    {
        //        var col = database.GetCollection<Device>(Collections.device.ToString());
        //        var q = Query.In("_id", BsonArray.Create(serialnumbers));
        //        var items = col.Find(q);

        //        foreach (Device d in items)
        //            devices.Add(d);
        //        return devices;
        //    }

        //    return devices;
        //}

        //static public Device getDevice(string serialnumber)
        //{
        //    if (serialnumber != null)
        //    {
        //        var col = database.GetCollection<Device>(Collections.device.ToString());
        //        var query = Query<Device>.EQ(e => e.serial_number, serialnumber);
        //        var members = col.Find(query);
                
        //        foreach (Device d in members)
        //            return d;
        //    }
        //    return new Device();
        //}

        //static public Address getAddress(ObjectId id)
        //{
        //    if (id != null)
        //    {
        //        var collection = database.GetCollection<Address>(Collections.addresses.ToString());
        //        var entityQuery = Query<Address>.EQ(e => e.id, id);
        //        var members = collection.Find(entityQuery);
        //        foreach (Address test in members)
        //        {
        //            return test;
        //        }
        //    }

        //    return new Address();
        //}

        static public HTMLCounter getHTMLCounter(ObjectId id)
        {
            if (id != null)
            {
                var collection = database.GetCollection<HTMLCounter>(Collections.full_counter.ToString());
                var entityQuery = Query<HTMLCounter>.EQ(e => e.id, id);

                var members = collection.Find(entityQuery);
                foreach (HTMLCounter test in members)
                {
                    return test;
                }
            }

            return new HTMLCounter();
        }

        static public HTMLSerial getHTMLSerial(ObjectId id)
        {
            if (id != null)
            {
                var collection = database.GetCollection<HTMLSerial>(Collections.full_serial.ToString());
                var entityQuery = Query<HTMLSerial>.EQ(e => e.id, id);

                var members = collection.Find(entityQuery);
                foreach (HTMLSerial test in members)
                {
                    return test;
                }
            }

            return new HTMLSerial();
        }

        static public EmailData getEmailData(byte[] id)
        {
            if (id != null)
            {
                BsonArray array = new BsonArray(id);
                var collection = database.GetCollection<EmailData>(Collections.emails_binary.ToString());
                var q = Query<EmailData>.EQ(e => e.id, id);

                var members = collection.Find(q);
                foreach (EmailData e in members)
                    return e;
            }

            return new EmailData();
        }
        #endregion

        //static public bool DeviceExists(Device d)
        //{
        //    if (d == null)
        //        throw new MissingMemberException("The Device in Database.MongoTB.DeviceExists is null");

        //    var collection = database.GetCollection<Device>(Collections.device.ToString());
        //    var entityQuery = Query<Device>.EQ(e => e.serial_number, d.serial_number);
        //    var members = collection.Count(entityQuery);
        //    if (members > 0)
        //        return true;
        //    else
        //        return false;
        //}

        #endregion

        #region DeleteDocument

        //static public bool DeleteDevice(Device d)
        //{
        //    if (d == null)
        //        throw new ArgumentNullException("The Device in Database.MongoTB.DeleteDevice is null");

        //    var collection = database.GetCollection<Device>(Collections.device.ToString());
        //    var entityQuery = Query<Device>.EQ(e => e.serial_number, d.serial_number);
        //    WriteConcernResult result = collection.Remove(entityQuery);
        //    if (result.DocumentsAffected == 1)
        //        return true;
        //    return false;
        //}

        static public void DeleteMachineRecords(MachineRecord[] r)
        {
            foreach(MachineRecord el in r)
                DeleteMachineRecord(el);
        }

        static public bool DeleteMachineRecord(MachineRecord r)
        {
            if (r == null)
                throw new ArgumentNullException("Machine record in Database.MongoTB.DeleteMachineRecord is null");

            var collection = database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
            var entityQuery = Query<MachineRecord>.EQ(e => e.id, r.id);
            WriteConcernResult result = collection.Remove(entityQuery);

            r.id = new ObjectId();
            DeleteHTMLSerial(r.full_serialnumber);
            DeleteHTMLCounter(r.full_counter);
            SaveMachineRecord_Deleted(r);
            

            return true;
        }

        static public bool DeleteHTMLSerial(HTMLSerial html)
        {
            if (html == null)
                throw new ArgumentNullException("HTMLSerial in Database.MongoTB.DeleteMachineRecord is null");

            var collection = database.GetCollection<HTMLSerial>(Collections.full_serial.ToString());
            var Q = Query<HTMLSerial>.EQ(e => e.id, html.id);
            WriteConcernResult result = collection.Remove(Q);

            SaveFullHTMLSerial_Deleted(html);

            return true;
        }

        static public bool DeleteHTMLCounter(HTMLCounter html)
        {
            if (html == null)
                throw new ArgumentNullException("HTMLSerial in Database.MongoTB.DeleteMachineRecord is null");

            var collection = database.GetCollection<HTMLCounter>(Collections.full_counter.ToString());
            var Q = Query<HTMLCounter>.EQ(e => e.id, html.id);
            WriteConcernResult result = collection.Remove(Q);

            SaveFullHTMLCounter_Deleted(html);

            return true;
        }

        static public bool DeleteHTMLSerial(ObjectId id)
        {
            HTMLSerial serial = getHTMLSerial(id);
            bool r = DeleteHTMLSerial(serial);
            SaveFullHTMLSerial_Deleted(serial);
            return r;
        }

        static public bool DeleteHTMLCounter(ObjectId id)
        {
            HTMLCounter counter = getHTMLCounter(id);
            bool r = DeleteHTMLCounter(counter);
            SaveFullHTMLCounter_Deleted(counter);
            return r;
        }

        #endregion

        #region test
        //static public void t()
        //{
        //    //test3dot1();
        //    //test();
        //    Initialize();
        //    //getAllReports();
        //    //test5();


        //    var collection = database.GetCollection<EmailData>(Collections.emails_binary.ToString());


        //    EmailData v = collection.FindOne();

        //    v.parse();
        //    string message = v.getEmail();

        //    List<string> lines = new List<string>();

        //    for (int i = 0; i < v.mail.Count; i++)
        //    {
        //        byte[] array = v.mail[i].AsByteArray;
        //        string line = System.Text.Encoding.Default.GetString(array);
        //        lines.Add(line);
        //    }

        //    Console.WriteLine("blabla");
        //}

        //static public void test()
        //{
        //    // Create a MongoClient object by using the connection string
        //    var client = new MongoClient(connectionString);

        //    //Use the MongoClient to access the server
        //    MongoServer server = client.GetServer();

        //    // Use the server to access the 'machines' database
        //    MongoDatabase database = server.GetDatabase(MongoTB.databaseName);
        //    var collection = database.GetCollection<MachineRecord>("machine_records");
        //    var totalNumberOfPosts = collection.Count();

        //    MongoCursor<MachineRecord> members = collection.FindAll();
        //    foreach (MachineRecord test in members)
        //    {
        //        string author = test.addressIP;
        //    }

        //    //List<Machine> query = collection.AsQueryable<Machine>().Where<Entity>(sb => sb.Name == "Star").ToList();

        //}

        //public async void test3dot1()
        //{
        //    int i = await test3();
        //}

        //public async Task<int> test3()
        //{
        //    var collection = _database.GetCollection<BsonDocument>("machines");

        //    var filter = Builders<BsonDocument>.Filter.Eq("AddressIP", "192.168.1.198");
        //    var count = 0;
        //    using (var cursor = await collection.FindAsync(filter))
        //    {
        //        while (await cursor.MoveNextAsync())
        //        {
        //            var batch = cursor.Current;
        //            foreach (var document in batch)
        //            {
        //                // process document
        //                count++;
        //            }
        //        }
        //    }
        //    return count;
        //}

        //public void Task<MachineRecordBson[]> test4()
        //{
        //    var collection = _database.GetCollection<BsonDocument>(Collections.machine_records.ToString());
        //    //var results = collection.Find<BsonDocument>(new BsonDocument());
        //    //long count = results.Count();

        //    //List<MachineRecordBson> machines = results.ToList<BsonDocument>();

        //}

        //        public void test5()
        //        {
        //            // Create a MongoClient object by using the connection string
        //            var client = new MongoClient(connectionString);

        //            //Use the MongoClient to access the server
        //            MongoServer server = client.GetServer();

        //            // Use the server to access the 'machines' database
        //            MongoDatabase database = server.GetDatabase(MongoTB.databaseName);
        //            var collection = database.GetCollection<MachineRecord>("machine_records");
        //            var totalNumberOfPosts = collection.Count();

        //            MongoCursor<MachineRecord> members = collection.FindAll();
        //            //BsonElement element = new BsonElement("AddressIP", (BsonValue)"192.167.1.198");

        //            var entityQuery = Query<MachineRecord>.EQ(e => e.addressIP, "192.168.1.198");

        //            members = collection.Find(entityQuery);
        //            foreach (MachineRecord test in members)
        //            {
        //                string author = test.addressIP;
        //            }
        //        }

        //        public async void test2()
        //        {
        //            var document = new BsonDocument
        //{
        //    { "address" , new BsonDocument
        //        {
        //            { "street", "2 Avenue" },
        //            { "zipcode", "10075" },
        //            { "building", "1480" },
        //            { "coord", new BsonArray { 73.9557413, 40.7720266 } }
        //        }
        //    },
        //    { "borough", "Manhattan" },
        //    { "cuisine", "Italian" },
        //    { "grades", new BsonArray
        //        {
        //            new BsonDocument
        //            {
        //                { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
        //                { "grade", "A" },
        //                { "score", 11 }
        //            },
        //            new BsonDocument
        //            {
        //                { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
        //                { "grade", "B" },
        //                { "score", 17 }
        //            }
        //        }
        //    },
        //    { "name", "Vella" },
        //    { "restaurant_id", "41704620" }
        //};



        //            var collection = _database.GetCollection<BsonDocument>("restaurants");
        //            await collection.InsertOneAsync(document);
        //        }

        #endregion
    }

}
