using MongoDB.Bson;
using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MongoInterface
{
    public class MachineRecord : IComparable<MachineRecord>
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
        public ObjectId id { get; set; }
        public string tonerlevel_c { get; set; }
        public string tonerlevel_m { get; set; }
        public string tonerlevel_y { get; set; }
        public string tonerlevel_k { get; set; }
        public bool parsed_by_email { get; set; }

        public bool tonerLowLever_C { get; set; }
        public bool tonerLowLever_M { get; set; }
        public bool tonerLowLever_Y { get; set; }
        public bool tonerLowLever_K { get; set; }

        public RecordsCollection? collection = null;

        /// <summary>
        /// Aktualizuj ten parametr tylko przez metode SetPrintedTrue
        /// Pobierane z mongo, true jeśli było już drukowane
        /// </summary>
        public bool printed { get; set; } // Pobierane z mongo, true jeśli było już drukowane

        public byte[] email_info { get; set; } // TO JEST ID OBIEKTU W BAZIE MONGO

        public MachineRecord()
        {
            datetime = new DateTime();
            description = "";
            addressIP = "";
            addressMAC = "";
            serial_number = "";
            full_counter = new ObjectId();
            full_serialnumber = new ObjectId();
            scan_counter = -1;
            print_counter_black_and_white = -1;
            print_counter_color = -1;
            id = new ObjectId();
            tonerlevel_c = "";
            tonerlevel_m = "";
            tonerlevel_k = "";
            tonerlevel_y = "";

            tonerLowLever_C = false;
            tonerLowLever_M = false;
            tonerLowLever_Y = false;
            tonerLowLever_K = false;

            print_counter_black_and_white = 0;
            print_counter_color = 0;

            printed = false;
            //email_info = new BsonBinaryData(new byte[] { 0 });
        }

        public void InitValues()
        {
            if (tonerlevel_c != null) tonerLowLever_C = tonerlevel_c.Contains("0-25%");
            if (tonerlevel_m != null) tonerLowLever_M = tonerlevel_m.Contains("0-25%");
            if (tonerlevel_y != null) tonerLowLever_Y = tonerlevel_y.Contains("0-25%");
            if (tonerlevel_k != null) tonerLowLever_K = tonerlevel_k.Contains("0-25%");
        }

        public int CompareTo(MachineRecord other)
        {
            return (datetime.CompareTo(other.datetime));
        }

        public static bool operator <(MachineRecord e1, MachineRecord e2)
        {
            return e1.CompareTo(e2) < 0;
        }

        public static bool operator >(MachineRecord e1, MachineRecord e2)
        {
            return e1.CompareTo(e2) > 0;
        }
    }
}
