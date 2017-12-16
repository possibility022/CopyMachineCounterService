using System;
using System.Collections;

using MongoDB.Bson;
using System.Collections.Generic;
using CopyinfoWPF.Common;

namespace CopyinfoWPF.Database
{
    internal class MachineRecordData : MachineRecord, IComparable<MachineRecordData>
    {
        private HTMLCounter html_counter { get; set; }
        private HTMLSerial html_serial { get; set; }

        private EmailData email { get; set; }

        public string deviceAddress { get; private set; }

        public string modelName { get; private set; }

        public string clientName { get; private set; }

        public Device GetDevice()
        {
            return DAO.GetDevice(serial_number, true);
        }

        public MachineRecordData()
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

            tonerLowLever_C = false;
            tonerLowLever_M = false;
            tonerLowLever_Y = false;
            tonerLowLever_K = false;

            print_counter_black_and_white = 0;
            print_counter_color = 0;

            printed = false;
        }

        public new void InitValues()
        {
            base.InitValues();

            Device dev = GetDevice();
            if (deviceAddress == null)
            {
                if (dev != null)
                {
                    deviceAddress = dev.address.ToString();
                }
            }

            if (modelName == null)
            {
                if (dev != null)
                {
                    modelName = dev.model;
                }
            }

            if (dev != null)
            {
                Client cli = dev.GetClient();
                if (cli != null)
                {
                    clientName = cli.name;
                }
            }
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
            throw new NotImplementedException();
            //Other.Printing.Print(GetTextToPrint());
        }

        public void ShowClient()
        {
            throw new NotImplementedException();
            //try
            //{
            //    Database.Client client = GetDevice().GetClient();
            //    Forms.FClient f = new Forms.FClient(client);
            //    f.Show();

            //}
            //catch (NullReferenceException ex)
            //{
            //    MessageBox.Show("Nie znalazłem klienta.");
            //}
        }

        public void ShowDevice()
        {
            throw new NotImplementedException();
            //try
            //{
            //    GetDevice().ShowDevice();
            //}
            //catch (NullReferenceException ex)
            //{
            //    MessageBox.Show("Nie znaleziono urzadzenia");
            //}
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
                "Data: " + datetime.ToString(CommonData.DateTimeFormat) + newLine +
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

            if (parsed_by_email)
            {
                textToPrint += GetEmail().GetEmail();
            }

            return textToPrint;
        }

        public void SetPrintedTrue()
        {
            if (printed)
                return;

            this.printed = true;
            DAO.ReplaceMachineRecord(this);
        }

        public int GetTotal()
        {
            return print_counter_black_and_white + print_counter_color;
        }

        public int CompareTo(MachineRecordData other)
        {
            return (datetime.CompareTo(other.datetime));
        }

        public static bool operator <(MachineRecordData e1, MachineRecordData e2)
        {
            return e1.CompareTo(e2) < 0;
        }

        public static bool operator >(MachineRecordData e1, MachineRecordData e2)
        {
            return e1.CompareTo(e2) > 0;
        }
    }

    internal class MachineRecordComparer : IComparer<MachineRecordData>
    {
        private int ColumnToSort;
        private CaseInsensitiveComparer ObjectCompare;
        private int[] columnsWithInt = new int[] { };

        public MachineRecordComparer()
        {
            ColumnToSort = 0;
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(MachineRecordData x, MachineRecordData y)
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
