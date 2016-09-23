using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;


namespace Copyinfo.Database
{
    class Mongo
    {
        const string ipadress = "***REMOVED***";
        public void test()
        {
            string connectionString = "mongodb://" + ipadress;

            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            MongoServer server = client.GetServer();

            // Use the server to access the 'machines' database
            MongoDatabase database = server.GetDatabase("machines");
            var collection = database.GetCollection<Machine>("machines");
            var totalNumberOfPosts = collection.Count();
            MongoCursor<Machine> members = collection.FindAll();
            foreach (Machine test in members)
            {
                string author = test.AddressIP;
            }

            var numberOfPostsWith2Comments =
                collection.Count(p => p.Comments.Count == 2);
        }
    }
}
