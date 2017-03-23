using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo.Database
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
        public string address { get { return GetAddress().ToString(); } }
        //public string[] devices { get; set; }

        private Address address_class;

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
            //devices = new string[] { };
        }

        public Address GetAddress()
        {
            return address_class;
        }

        public void GetAddress(Address a)
        {
            address_class = a;
        }

        public List<Device> GetDevices()
        {
            return DAO.GetDevices(this.id);
        }
    }
}
