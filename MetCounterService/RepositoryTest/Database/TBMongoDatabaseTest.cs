using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories.MongoInterface;
using Repositories.Database;

namespace RepositoryTest.Database
{
    [TestClass]
    public class TBMongoDatabaseTest
    {
        const string connectionString = "mongodb://" + ipadress + ":" + port;
        const string ipadress = "192.168.0.42";
        const string port = "2772";

        [TestMethod]
        public void DeleteOneRecord()
        {
            IMongoRepository database = new TBMongoDatabase("copyinfo", connectionString);
            database.Connect();

            HTMLCounter counter = new HTMLCounter();
            counter.id = new MongoDB.Bson.ObjectId("58a1a7d5261251123197430f");
            Assert.IsTrue(database.DeleteHTMLCounter(counter));
        }
    }
}
