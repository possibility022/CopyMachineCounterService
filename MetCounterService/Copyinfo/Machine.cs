using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Copyinfo
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

        public string SerialNumber { get; set; }

        public int PrintCounterBlackAndWhite { get; set; }
        public int PrintCounterColor { get; set; }
        public int ScanCounter { get; set; }

        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public string AddressMAC { get; set; }
        public ObjectId FullCounter { get; set; }
        public ObjectId FullSerialnumber { get; set; }
        public string AddressIP { get; set; }
        
        public ObjectId id { get; set; }
    }
}
