using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo.Database
{
    class Client
    {
        public string name { get; set; }            // nazwa klienta
        public string NIP { get; set; }             // NIP
        public ObjectId address { get; set; }       // id adresu
        public BsonArray device_ids { get; set; }   // tablica id urządzeń
        public ObjectId id { get; set; }
    }
}
