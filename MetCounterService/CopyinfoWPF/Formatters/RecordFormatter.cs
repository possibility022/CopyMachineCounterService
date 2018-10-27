using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Interfaces.Formatters;
using System.Collections.Generic;
using System.Text;

namespace CopyinfoWPF.Formatters
{
    public class RecordFormatter : IRecordToTextFormatter
    {
        public IEnumerable<string> GetText(IEnumerable<MachineRecordViewModel> records)
        {
            var sb = new StringBuilder();

            foreach(var rec in records)
            {
                sb.Clear();

                RecToString(rec, sb);

                yield return sb.ToString();

                //"Klient: " + rec.ClientName + " NIP: " + client.NIP + newLine +
                //"Data: " + datetime.ToString(Forms.Style.DateTimeFormat) + newLine +
                //"Numer Seryjny: " + serial_number + newLine +
                //"Producent: " + device.provider + newLine +
                //"Model: " + device.model + newLine +
                //"Adres: " + address.street + " " + address.house_number + "/" + address.apartment + " " + address.city + newLine +
                //"Licznik Skanowań: " + scan_counter + newLine +
                //"Licznik Czarno-Białe: " + print_counter_black_and_white + newLine +
                //"Licznik Kolorowe: " + print_counter_color + newLine +
                //"Toner Cyjan: " + tonerlevel_c + newLine +
                //"Toner Magenta: " + tonerlevel_c + newLine +
                //"Toner Yellow: " + tonerlevel_m + newLine +
                //"Toner Black: " + tonerlevel_k + newLine;
            }
        }

        public StringBuilder GetText(MachineRecordViewModel record)
        {
            var sb = new StringBuilder();
            RecToString(record, sb);
            return sb;
        }

        private void RecToString(MachineRecordViewModel rec, StringBuilder sb)
        {
            sb.AppendLine($"Klient: {rec.ClientName} NIP: {rec.Client?.Nip}");
            sb.AppendLine($"Data: {rec.Record?.ReadDatetime.ToString()}");
            sb.AppendLine($"Numer seryjny: {rec.Record?.SerialNumber}");
            sb.AppendLine($"Producent: {rec.Device?.ModelUrzadzenia.MarkaUrzadzenia.Nazwa1}");
            sb.AppendLine($"Model: {rec.Device?.ModelUrzadzenia.Nazwa1}");
            sb.AppendLine($"Adres: {rec.Address?.Ulica}");
            sb.AppendLine($"Licznik Skanowań: {rec.Record?.CounterScanner}");
            sb.AppendLine($"Licznik Czarno-Biały: {rec.Record?.CounterBlackAndWhite}");
            sb.AppendLine($"Licznik Kolorowy: {rec.Record?.CounterColor}");
            sb.AppendLine($"Toner Cyjan: {rec.Record?.TonerLevelCyan}");
            sb.AppendLine($"Toner Magenta: {rec.Record?.TonerLevelMagenta}");
            sb.AppendLine($"Toner Yellow: {rec.Record?.TonerLevelYellow}");
            sb.AppendLine($"Toner Czarny: {rec.Record?.TonerLevelBlack}");
        }
    }
}
