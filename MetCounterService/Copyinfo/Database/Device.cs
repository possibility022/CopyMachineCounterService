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
        public int instalationAddressId { get; set; }             // miejsce isntalacji
        public bool service_agreement { get; set; }             // umowa serwisowa
        public DateTime instalation_datetime { get; set; }      // data instalacji
        public int status { get; set; }
        public int client_id { get; set; }
        public int id { get; set; }

        public Address address { get; set; }

        public Device()
        {
            provider = "";
            model = "";
            instalationAddressId = -1;
            instalation_datetime = DateTime.Now;
            serial_number = "";
        }

        public MachineRecord GetOneRecord(DateTime datetime)
        {
            return DAO.GetFirstInMonth(serial_number, datetime);
        }

        public List<MachineRecord> GetRecords()
        {
            return DAO.GetReports(serial_number);
        }

        public Client GetClient()
        {
            return DAO.GetClient(client_id, true);
        }
    }
}
