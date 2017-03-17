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
        public string serialnumber { get; set; }
        public ObjectId fullSerialNumberID { get; set; }
        public ObjectId fullCounterID { get; set; }
        public int scanCounter { get; set; }
        public int printerCounterBlackAndWhite { get; set; }
        public int printerCounterColor { get; set; }
        public ObjectId id { get; set; }
        public string tonerLevelCyan { get; set; }
        public string tonerLevelMagenta { get; set; }
        public string tonerLevelYellow { get; set; }
        public string tonerLevelBlack { get; set; }

        private HTMLCounter html_counter { get; set; }
        private HTMLSerial html_serial { get; set; }
        public byte[] email_info { get; set; } // TO JEST ID OBIEKTU W BAZIE MONGO

        private EmailData email { get; set; }

        public Device GetDevice()
        {
            return DAO.GetDevice(serialnumber);
        }

        public MachineRecord()
        {
            datetime = new DateTime();
            description = "";
            addressIP = "";
            addressMAC = "";
            serialnumber = "";
            fullCounterID = new ObjectId();
            fullSerialNumberID = new ObjectId();
            scanCounter = -1;
            printerCounterBlackAndWhite = -1;
            printerCounterColor = -1;
            id = new ObjectId();
            email = null;
            //email_info = new BsonBinaryData(new byte[] { 0 });
        }

        public bool IsParsedEmail()
        {
            if (email_info == null)
                return false;
            else
                return true;
        }

        public EmailData GetEmail()
        {
            if (email == null)
            {
                email = Database.DAO.GetEmailData(email_info);
                email.Parse();
            }

            return email;
        }

        public HTMLCounter GetCounter()
        {
            if (html_counter == null)
            {
                html_counter = Database.DAO.GetHTMLCounter(fullCounterID);
            }

            return html_counter;
        }

        public HTMLSerial GetSerial()
        {
            if (html_serial == null)
            {
                html_serial = Database.DAO.GetHTMLSerial(fullSerialNumberID);
            }

            return html_serial;
        }

        public void Print()
        {
            Other.Printing.Print(GetTextToPrint());
        }

        public string GetTextToPrint()
        {
            Device device = GetDevice();
            Client client = device.GetClient();
            Address address = device.GetAddress();

            string newLine = "\r\n";
            string textToPrint =
                "Klient: " + client.name + " NIP: " + client.NIP + newLine +
                "Data: " + datetime.ToString(Forms.Style.DateTimeFormat) + newLine +
                "Numer Seryjny: " + serialnumber + newLine +
                "Producent: " + device.provider + newLine +
                "Model: " + device.model + newLine +
                "Adres: " + address.street + " " + address.house_number + "/" + address.apartment + " " + address.city + newLine +
                "Licznik Skanowań: " + scanCounter + newLine +
                "Licznik Czarno-Białe: " + printerCounterBlackAndWhite + newLine +
                "Licznik Kolorowe: " + printerCounterColor + newLine +
                "Toner Cyjan: " + tonerLevelCyan + newLine +
                "Toner Magenta: " + tonerLevelMagenta + newLine +
                "Toner Yellow: " + tonerLevelYellow + newLine +
                "Toner Black: " + tonerLevelBlack + newLine;

            if (IsParsedEmail())
            {
                textToPrint += GetEmail().GetEmail();
            }

            return textToPrint;
        }

        public int GetTotal()
        {
            return printerCounterBlackAndWhite + printerCounterColor;
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
