using CopyinfoWPF.Interfaces;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Workflows.Printing
{
    class PrintContentProvider : IPrintContentProvider
    {
        public IEnumerable<string> GetTextToPrint(IEnumerable<Record> records)
        {

            StringBuilder sb = new StringBuilder();

            //foreach(var rec in records)
            //{
            //    string newLine = "\r\n";
            //    string textToPrint =
            //        "Klient: " + client.name + " NIP: " + client.NIP + newLine +
            //        "Data: " + datetime.ToString(Forms.Style.DateTimeFormat) + newLine +
            //        "Numer Seryjny: " + serial_number + newLine +
            //        "Producent: " + device.provider + newLine +
            //        "Model: " + device.model + newLine +
            //        "Adres: " + address.street + " " + address.house_number + "/" + address.apartment + " " + address.city + newLine +
            //        "Licznik Skanowań: " + scan_counter + newLine +
            //        "Licznik Czarno-Białe: " + print_counter_black_and_white + newLine +
            //        "Licznik Kolorowe: " + print_counter_color + newLine +
            //        "Toner Cyjan: " + tonerlevel_c + newLine +
            //        "Toner Magenta: " + tonerlevel_c + newLine +
            //        "Toner Yellow: " + tonerlevel_m + newLine +
            //        "Toner Black: " + tonerlevel_k + newLine;

            //    if (parsed_by_email)
            //    {
            //        textToPrint += GetEmail().GetEmail();
            //    }
            //}
            return null;
        }
    }
}
