﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace Copyinfo.Database
{
    class MongoDB
    {

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        const string ipadress = "***REMOVED***";
        const string connectionString = "mongodb://" + ipadress;
        const string DATABASE_NAME = "copyinfo";

        private static MongoClient client;
        private static MongoServer server;
        private static MongoDatabase database;

        enum Collections
        {
            machine_record
        }

        public static void Initialize()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(DATABASE_NAME);

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("machines");
        }

        public List<Machine> getAllReports()
        {
            MongoCollection<Machine> collection = database.GetCollection<Machine>(Collections.machine_record.ToString());
            MongoCursor<Machine> all = collection.FindAll();

            List<Machine> list = new List<Machine>();

            foreach ( Machine m in all)
            {
                list.Add(m);
            }

            return list;
        }

        public void t()
        {
            //test3dot1();
            test();
        }

        public void test()
        {
            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            MongoServer server = client.GetServer();

            // Use the server to access the 'machines' database
            MongoDatabase database = server.GetDatabase(MongoDB.DATABASE_NAME);
            var collection = database.GetCollection<Machine>("machine_record");
            var totalNumberOfPosts = collection.Count();
            
            MongoCursor<Machine> members = collection.FindAll();
            foreach (Machine test in members)
            {
                string author = test.AddressIP;
            }

            //List<Machine> query = collection.AsQueryable<Machine>().Where<Entity>(sb => sb.Name == "Star").ToList();

        }

        public async void test3dot1()
        {
            int i = await test3();
        }

        public async Task<int> test3()
        {
            var collection = _database.GetCollection<BsonDocument>("machines");

            var filter = Builders<BsonDocument>.Filter.Eq("AddressIP", "192.168.1.198");
            var count = 0;
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        // process document
                        count++;
                    }
                }
            }
            return count;
        }

        public async void test2()
        {
            var document = new BsonDocument
{
    { "address" , new BsonDocument
        {
            { "street", "2 Avenue" },
            { "zipcode", "10075" },
            { "building", "1480" },
            { "coord", new BsonArray { 73.9557413, 40.7720266 } }
        }
    },
    { "borough", "Manhattan" },
    { "cuisine", "Italian" },
    { "grades", new BsonArray
        {
            new BsonDocument
            {
                { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                { "grade", "A" },
                { "score", 11 }
            },
            new BsonDocument
            {
                { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                { "grade", "B" },
                { "score", 17 }
            }
        }
    },
    { "name", "Vella" },
    { "restaurant_id", "41704620" }
};



            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);
        }
    }
}
