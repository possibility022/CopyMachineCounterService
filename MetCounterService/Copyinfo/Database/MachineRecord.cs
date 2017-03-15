using System;
using System.Collections;
using System.Windows.Forms;


using MongoDB.Bson;
using System.Collections.Generic;

namespace Copyinfo.Database
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

        private HTMLCounter html_counter { get; set; }
        private HTMLSerial html_serial { get; set; }
        public byte[] email_info { get; set; } // TO JEST ID OBIEKTU W BAZIE MONGO



        private EmailData email { get; set; }

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
            email = null;
            //email_info = new BsonBinaryData(new byte[] { 0 });
        }

        public bool isParsedEmail()
        {
            if (email_info == null)
                return false;
            else
                return true;
        }

        public EmailData getEmail()
        {
            if (email == null)
            {
                email = Database.DAO.getEmailData(email_info);
                email.parse();
            }

            return email;
        }

        public HTMLCounter getCounter()
        {
            if (html_counter == null)
            {
                html_counter = Database.DAO.getHTMLCounter(full_counter);
            }

            return html_counter;
        }

        public HTMLSerial getSerial()
        {
            if (html_serial == null)
            {
                html_serial = Database.DAO.getHTMLSerial(full_serialnumber);
            }

            return html_serial;
        }

        public int getTotal()
        {
            return print_counter_black_and_white + print_counter_color;
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

    public class MachineRecordComparer : IComparer<Database.MachineRecord>
    {
        private int ColumnToSort;
        private SortOrder OrderOfSort;
        private CaseInsensitiveComparer ObjectCompare;
        private int[] columnsWithInt = new int[] { };

        public MachineRecordComparer()
        {
            ColumnToSort = 0;
            OrderOfSort = SortOrder.None;
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(MachineRecord x, MachineRecord y)
        {
            // Sprawdzanie datą

            int datetime_compare = DateTime.Compare(x.datetime, y.datetime);

            if (datetime_compare > 0)
                return 1;
            else if (datetime_compare < 0)
                return -1;

            return 0;
        }
    }
}
