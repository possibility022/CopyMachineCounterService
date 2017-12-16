using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Repositories.Enums;
using Repositories.MongoInterface;
using System.Security;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace Repositories.Database
{
    public class TBMongoDatabase : IMongoRepository
    {
        string connectionString;

        MongoCredential _mongoCredential;
        MongoClientSettings _settings;

        private static IMongoClient _client;
        private static IMongoDatabase _database;

        public string DatabaseName { get; private set; }


        public bool Connect()
        {
            _client = new MongoClient(_settings);
            _database = _client.GetDatabase(DatabaseName);

            return true;
        }

        public TBMongoDatabase(string databaseName, string connectionString)
        {
            _settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            DatabaseName = databaseName;
        }

        public TBMongoDatabase(string login, string password, string databaseName, string connectionString)
        {
            this.connectionString = connectionString;
            _mongoCredential = MongoCredential.CreateCredential(databaseName, login, password);
            _settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            _settings.Credentials = new MongoCredential[] { _mongoCredential };
        }

        private bool DeleteOne(ObjectId id, Collections collection)
        {
            IMongoCollection<BsonDocument> col = _database.GetCollection<BsonDocument>(collection.ToString());
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            DeleteResult result = col.DeleteOne(filter);

            return result.DeletedCount == 1;
        }

        /// <summary>
        /// Deletes many items.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="collections">The collections.</param>
        /// <returns>True if count of deleted items is equal to lenght of ids collection</returns>
        private bool DeleteMany(IEnumerable<ObjectId> ids, Collections collections)
        {
            IMongoCollection<BsonDocument> col = _database.GetCollection<BsonDocument>(collections.ToString());
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.In("_id", ids);
            DeleteResult result = col.DeleteMany(filter);
            return result.DeletedCount == ids.Count();
        }


        private BsonDocument GetOne(ObjectId id, Collections collection)
        {
            IMongoCollection<BsonDocument> col = _database.GetCollection<BsonDocument>(collection.ToString());
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var result = col.Find(filter);
            if (result.Count() > 0)
            {
                return result.First();
            }

            return null;
        }

        public bool DeleteHTMLCounter(HTMLCounter html)
        {
            return DeleteOne(html.id, Collections.full_counter);
        }

        public bool DeleteHTMLSerial(HTMLSerial html)
        {
            return DeleteOne(html.id, Collections.full_serial);
        }

        public bool DeleteMachineRecord(MachineRecord r)
        {
            return DeleteOne(r.id, Collections.machine_records);
        }

        public void DeleteMachineRecords(MachineRecord[] r)
        {
            DeleteMany(r.Select(e => e.id), Collections.machine_records);
        }

        public IEnumerable<MachineRecord> GetAllReports(RecordsCollection collectionType, string serialNumber = null)
        {
            string col = RecordsCollection.Normal.ToString();

            switch(collectionType)
            {
                case RecordsCollection.Normal:
                    col = Collections.machine_records.ToString();
                    break;
                case RecordsCollection.Others:
                    col = Collections.machine_records_other.ToString();
                    break;
                case RecordsCollection.Both:
                    return GetAllReports(RecordsCollection.Normal, serialNumber).Concat(GetAllReports(RecordsCollection.Others, serialNumber));
            }

            IMongoCollection<MachineRecord> mongoCollection = _database.GetCollection<MachineRecord>(col);

            if (string.IsNullOrEmpty(serialNumber))
                return mongoCollection.Find(emptyFilter => true).ToList();

            FilterDefinition<MachineRecord> filter = Builders<MachineRecord>.Filter.Eq(e => e.serial_number, serialNumber);

            return mongoCollection.Find(filter).ToList();
        }

        public MachineRecord GetFirstInMonth(string serial_number, DateTime month)
        {
            MachineRecord record = null;
            if (string.IsNullOrEmpty(serial_number) == false)
            {
                var collection = _database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
                var filter = Builders<MachineRecord>.Filter.Gte(e => e.datetime, month) & Builders<MachineRecord>.Filter.Eq(e => e.serial_number, serial_number);

                record = collection.Find(filter).FirstOrDefault();
                record?.InitValues();
            }
            return record;
        }

        public HTMLCounter GetHTMLCounter(ObjectId id)
        {
            BsonDocument doc = GetOne(id, Collections.full_counter);
            if (doc != null)
                return BsonSerializer.Deserialize<HTMLCounter>(doc);
            else
                return null;
        }

        public HTMLSerial GetHTMLSerial(ObjectId id)
        {
            BsonDocument doc = GetOne(id, Collections.full_serial);
            if (doc != null)
                return BsonSerializer.Deserialize<HTMLSerial>(doc);
            else
                return null;
        }

        public MachineRecord GetLatestForDevice(string serial_number)
        {
            MachineRecord record = null;

            if (serial_number != null)
            {
                var collection = _database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
                var sort = Builders<MachineRecord>.Sort.Descending(e => e.datetime);
                var filter = Builders<MachineRecord>.Filter.Eq(e => e.serial_number, serial_number);

                record = collection.Find<MachineRecord>(filter)?.Sort(sort)?.FirstOrDefault();

                record?.InitValues();
            }
            return record;
        }

        public IEnumerable<MachineRecord> GetOtherReports(string serial_number)
        {
            return GetAllReports(RecordsCollection.Others, serial_number);
        }

        public void ReplaceMachineRecrod(MachineRecord r, RecordsCollection recordsCollection)
        {
            if (r != null)
            {
                IMongoCollection<MachineRecord> collection;
                if (recordsCollection == RecordsCollection.Normal)
                {
                    var filter = Builders<MachineRecord>.Filter.Eq(e => e.id, r.id);
                    collection = _database.GetCollection<MachineRecord>(Collections.machine_records.ToString());
                    collection.ReplaceOne(filter, r);
                }
                else if (recordsCollection == RecordsCollection.Others)
                {

                    var filter = Builders<MachineRecord>.Filter.Eq(e => e.id, r.id);
                    collection = _database.GetCollection<MachineRecord>(Collections.machine_records_other.ToString());
                    collection.ReplaceOne(filter, r);
                }else
                {
                    throw new ArgumentException();
                }                
            }
        }
    }
}
