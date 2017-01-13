using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo.Database
{
    public class Klient
    {
        public string name { get; set; }
        public string NIP { get; set; }
        public bool ser_agr { get; set; } // umowa serwisowa - service agreement
        public ObjectId address { get; set; }
        public int[] p_numbers { get; set; }
        public int[] f_numbers { get; set; }
        public string[] emails { get; set; }
        public string[] wwwsites { get; set; }
        public string notes { get; set; }
        public string[] devices { get; set; }

        public Klient()
        {
            name = "";
            NIP = "";
            ser_agr = false;
            p_numbers = new int[] { };
            f_numbers = new int[] { };
            emails = new string[] { };
            wwwsites = new string[] { };
            notes = "";
            devices = new string[] { };
        }

    }
}
