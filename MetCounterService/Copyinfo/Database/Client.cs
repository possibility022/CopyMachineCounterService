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
        public string name { get; set; }
        public string id { get; set; }
        public bool ser_agr { get; set; } // umowa serwisowa - service agreement
        public int address { get; set; }
        public string[] p_numbers { get; set; }
        public string[] f_numbers { get; set; }
        public string[] emails { get; set; }
        public string[] wwwsites { get; set; }
        public string notes { get; set; }
        public string[] devices { get; set; }

        private Address address_class;

        public Client()
        {
            name = "";
            id = "";
            ser_agr = false;
            p_numbers = new string[] { };
            f_numbers = new string[] { };
            emails = new string[] { };
            wwwsites = new string[] { };
            notes = "";
            devices = new string[] { };
        }

        public Address getAddress()
        {
            if (this.address_class == null)
                this.address_class = Database.DAO.getAddress(address);
            return address_class;
        }

        public void setAddress(Address a)
        {
            address_class = a;
            address = a.id;
        }

        public List<Device> getDevices()
        {
            return DAO.getDevices(this.devices);
        }

        public void addDevices(string[] devicesSerals)
        {
            foreach (string device in devicesSerals)
                addDevice(device);
        }

        public void addDevice(string deviceSerialNumber)
        {
            if (devices.Contains(deviceSerialNumber) == false)
            {
                devices = Other.ArrayOperations.addToArray(devices, ref deviceSerialNumber);
            }
        }
        
    }
}
