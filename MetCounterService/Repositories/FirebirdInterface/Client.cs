using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FirebirdInterface
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string NIP { get; set; }
        public bool ser_agr { get; set; } // umowa serwisowa - service agreement
        public string[] p_numbers { get; set; }
        public string[] f_numbers { get; set; }
        public string[] emails { get; set; }
        public string[] wwwsites { get; set; }
        public string notes { get; set; }

        public Client()
        {
            name = "";
            NIP = "";
            ser_agr = false;
            p_numbers = new string[] { };
            f_numbers = new string[] { };
            emails = new string[] { };
            wwwsites = new string[] { };
            notes = "";
        }
    }
}
