using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo.Database
{
    public class Device
    {
        public string provider { get; set; }                    // producent
        public string model { get; set; }                       // model
        public string serial_number { get; set; }               // numer seryjny
        public ObjectId instalation_address { get; set; }       // miejsce isntalacji
        public DateTime instalation_datetime { get; set; }      // data instalacji
        public ObjectId id { get; set; }                        // _id
    }
}
