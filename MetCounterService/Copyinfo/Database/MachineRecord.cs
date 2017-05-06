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
        public bool parsed_by_email { get; set; }

        protected string _clientName { get; set; }
        protected string _deviceAddress { get; set; }
        protected string _modelName { get; set; }


        public string modelName { get
            {
                if (_modelName == null)
                {
                    Device dev = GetDevice();
                    if (dev != null)
                    {
                        _modelName = dev.model;
                        return _modelName;
                    }

                    _modelName = "";
                    return "";
                }
                else
                    return _modelName;
            } }
        public string deviceAddress { get
            {
                if (_deviceAddress == null)
                {
                    Device dev = GetDevice();
                    if (dev != null)
                    {
                        _deviceAddress = dev.address.ToString();
                        return _deviceAddress;
                    }

                    _deviceAddress = "";
                    return "";
                }
                else
                    return _deviceAddress;
            } }
        public string clientName { get {
                if (_clientName == null)
                {
                    Device dev = GetDevice();
                    if (dev != null)
                    {
                        Client cli = dev.GetClient();
                        if (cli != null)
                        {
                            _clientName = cli.name;
                            return cli.name;
                        }
                    }

                    _clientName = "";
                    return "";
                }
                else
                    return _clientName;
            } }


        private HTMLCounter html_counter { get; set; }
        private HTMLSerial html_serial { get; set; }
        public byte[] email_info { get; set; } // TO JEST ID OBIEKTU W BAZIE MONGO

        private EmailData email { get; set; }

        public Device GetDevice()
        {
            return DAO.GetDevice(serial_number, true);
        }

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
            tonerlevel_c = "";
            tonerlevel_m = "";
            tonerlevel_k = "";
            tonerlevel_y = "";
            print_counter_black_and_white = 0;
            print_counter_color = 0;
            //email_info = new BsonBinaryData(new byte[] { 0 });
        }

        public bool IsParsedEmail()
        {
            return parsed_by_email;
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
                html_counter = Database.DAO.GetHTMLCounter(full_counter);
            }

            return html_counter;
        }

        public HTMLSerial GetSerial()
        {
            if (html_serial == null)
            {
                html_serial = Database.DAO.GetHTMLSerial(full_serialnumber);
            }

            return html_serial;
        }

        public void Print()
        {
            Other.Printing.Print(GetTextToPrint());
        }

        public void ShowClient()
        {
            try
            {
                Database.Client client = GetDevice().GetClient();
                Forms.FClient f = new Forms.FClient(client);
                f.Show();

            } catch (NullReferenceException ex)
            {
                MessageBox.Show("Nie znalazłem klienta.");
            }
        }

        public string GetTextToPrint()
        {
            Device device = GetDevice();
            Client client;
            Address address;

            if (device != null)
            {
                client = device.GetClient();
                if (client != null)
                {
                    address = client.GetAddress();
                }
                else
                {
                    address = new Address();
                }
            }
            else
            {
                device = new Device();
                client = new Client();
                address = new Address();
            }


            string newLine = "\r\n";
            string textToPrint =
                "Klient: " + client.name + " NIP: " + client.NIP + newLine +
                "Data: " + datetime.ToString(Forms.Style.DateTimeFormat) + newLine +
                "Numer Seryjny: " + serial_number + newLine +
                "Producent: " + device.provider + newLine +
                "Model: " + device.model + newLine +
                "Adres: " + address.street + " " + address.house_number + "/" + address.apartment + " " + address.city + newLine +
                "Licznik Skanowań: " + scan_counter + newLine +
                "Licznik Czarno-Białe: " + print_counter_black_and_white + newLine +
                "Licznik Kolorowe: " + print_counter_color + newLine +
                "Toner Cyjan: " + tonerlevel_c + newLine +
                "Toner Magenta: " + tonerlevel_c + newLine +
                "Toner Yellow: " + tonerlevel_m + newLine +
                "Toner Black: " + tonerlevel_k + newLine;

            if (IsParsedEmail())
            {
                textToPrint += GetEmail().GetEmail();
            }

            return textToPrint;
        }

        public int GetTotal()
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
