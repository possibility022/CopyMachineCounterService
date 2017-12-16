using MongoDB.Bson;

namespace Repositories.MongoInterface
{
    public class HTMLCounter
    {
        public ObjectId id { get; set; }
        public string full_counter { get; set; }
    }
}
