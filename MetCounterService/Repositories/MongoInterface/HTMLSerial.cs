using MongoDB.Bson;

namespace Repositories.MongoInterface
{
    public class HTMLSerial
    {
        public ObjectId id { get; set; }
        public string full_serialnumber { get; set; }
    }
}
