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
        public int instalation_address { get; set; }             // miejsce isntalacji
        public bool service_agreement { get; set; }             // umowa serwisowa
        public DateTime instalation_datetime { get; set; }      // data instalacji
        public int status { get; set; }
        public int client_id { get; set; }

        private Address address { get; set; }

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
            if (address == null)
            {
                address = FirebirdTB.getAddress(instalation_address);
            }
            return this.address;
        }

        public void setAddress(Address adress)
        {
            this.address = adress;
        }

        public MachineRecord getOneRecord(DateTime datetime)
        {
            return DAO.GetOneRecord(serial_number, datetime);
        }

        public Client getClient()
        {
            return DAO.getClient(client_id);
        }
    }
}
