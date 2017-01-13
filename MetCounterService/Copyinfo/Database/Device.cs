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
        public string id { get; set; }                          // numer seryjny
        public ObjectId instalation_address { get; set; }       // miejsce isntalacji
        public DateTime instalation_datetime { get; set; }      // data instalacji

        private Address adress { get; set; }

        public Device()
        {
            provider = "";
            model = "";
            instalation_address = new ObjectId();
            instalation_datetime = DateTime.Now;
            id = "";
        }

        public void setAddress(Address a)
        {
            this.adress = a;
            instalation_address = a.id;
        }

        public Address getAddress()
        {
            if (adress == null)
            {
                adress = Global.database.getAddress(instalation_address);
            }
            return this.adress;
        }
    }
}
