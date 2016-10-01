using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo.Database
{
    public class MachineRecord
    {
        //|PrintCounterColor| 0
        //|DateTime| 9.22.2016 09:18:32 AM
        //|Description| komputer Tomka; D
        //|FullSerialnumber| Len > 100
        //|ScanCounter| 25033
        //|PrintCounterBlackAndWhite| 349873
        //|FullCounter| Len > 100
        //|AddressIP| 192.168.1.132
        //|AddressMAC| 00-21-b7-9c-a6-7c
        //|SerialNumber| 7947PD5


            //"datetime": "",
            //"description": "",
            //"dddressIP": "",
            //"addressMAC": "",
            //"serial_number": "",
            //"full_serialnumber": "",
            //"full_counter": "",
            //"scan_counter": "",
            //"print_counter_black_and_white": "",
            //"print_counter_color": ""}

        public DateTime datetime { get; set; }
        public string description { get; set; }
        public string addressIP { get; set; }
        public string addressMAC { get; set; }
        public string serial_number { get; set; }
        public ObjectId full_serialnumber { get; set; }
        public ObjectId full_counter { get; set; }
        public int scan_counter { get; set; }
        public int print_counter_black_and_white { get; set; }
        public int print_counter_color { get; set; }
    }
}
