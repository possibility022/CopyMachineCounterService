using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace CopyinfoWPF.Database
{
    public class HTMLCounter
    {
        public ObjectId id { get; set; }
        public string full_counter { get; set; }
    }
}
