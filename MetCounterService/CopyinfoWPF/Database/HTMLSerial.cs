using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace CopyinfoWPF.Database
{
    public class HTMLSerial
    {
        public ObjectId id { get; set; }
        public string full_serialnumber { get; set; }
    }
}
