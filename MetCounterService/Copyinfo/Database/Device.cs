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
        public string serial_number { get; set; }                          // numer seryjny
        public int instalation_address { get; set; }       // miejsce isntalacji
        public DateTime instalation_datetime { get; set; }      // data instalacji

        private Address adress { get; set; }

        public Device()
        {
            provider = "";
            model = "";
            instalation_address = -1;
            instalation_datetime = DateTime.Now;
            serial_number = "";
        }

        public Address getAddress()
        {
            if (adress == null)
            {
                adress = FirebirdTB.getAddress(instalation_address);
            }
            return this.adress;
        }
    }
}
