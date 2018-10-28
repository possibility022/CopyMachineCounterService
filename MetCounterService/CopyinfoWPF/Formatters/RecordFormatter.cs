using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.Workflows.Email;
using System.Collections.Generic;
using System.Text;

namespace CopyinfoWPF.Formatters
{
    public class RecordFormatter : IFormatter<MachineRecordViewModel>, IFormatter<Record>, IFormatter<MimeReader>
    {

        MimeReader _mimeReader = new MimeReader();

        public IEnumerable<string> GetText(IEnumerable<MachineRecordViewModel> records)
        {
            var sb = new StringBuilder();

            foreach (var rec in records)
            {
                sb.Clear();

                RecToString(rec, ref sb);

                yield return sb.ToString();
            }
        }

        public StringBuilder GetText(MachineRecordViewModel record)
        {
            var sb = new StringBuilder();
            RecToString(record, ref sb);
            return sb;
        }

        public IEnumerable<string> GetText(IEnumerable<MimeReader> items)
        {
            var sb = new StringBuilder();

            foreach (var mime in items)
            {
                GetText(mime, ref sb);
                yield return sb.ToString();
                sb.Clear();
            }
        }

        public StringBuilder GetText(MimeReader item)
        {
            var sb = new StringBuilder();
            GetText(item, ref sb);
            return sb;
        }

        private void GetText(MimeReader item, ref StringBuilder sb)
        {
            sb.AppendLine($"Od: {item.From}");
            sb.AppendLine($"Temat: {item.Subject}");
            sb.AppendLine();
            sb.AppendLine(item.TextBody);
        }

        public IEnumerable<string> GetText(IEnumerable<Record> items)
        {
            var sb = new StringBuilder();

            foreach (var item in items)
            {
                sb.Clear();
                GetText(item, ref sb);
                yield return sb.ToString();
            }

        }

        public StringBuilder GetText(Record item)
        {
            var sb = new StringBuilder();
            GetText(item, ref sb);
            return sb;
        }

        private void GetText(Record record, ref StringBuilder sb)
        {
            sb.AppendLine($"Data odczytu: {record?.ReadDatetime.ToString()}");
            sb.AppendLine($"Numer seryjny: {record?.SerialNumber}");
            sb.AppendLine($"Licznik Skanowań: {record?.CounterScanner}");
            sb.AppendLine($"Licznik Czarno-Biały: {record?.CounterBlackAndWhite}");
            sb.AppendLine($"Licznik Kolorowy: {record?.CounterColor}");
            sb.AppendLine($"Toner Cyjan: {record?.TonerLevelCyan}");
            sb.AppendLine($"Toner Magenta: {record?.TonerLevelMagenta}");
            sb.AppendLine($"Toner Yellow: {record?.TonerLevelYellow}");
            sb.AppendLine($"Toner Czarny: {record?.TonerLevelBlack}");
        }

        private void RecToString(MachineRecordViewModel rec, ref StringBuilder sb)
        {
            sb.AppendLine($"Klient: {rec.ClientName} NIP: {rec.Client?.Nip}");
            sb.AppendLine($"Producent: {rec.Device?.ModelUrzadzenia.MarkaUrzadzenia.Nazwa1}");
            sb.AppendLine($"Model: {rec.Device?.ModelUrzadzenia.Nazwa1}");
            sb.AppendLine($"Adres: {rec.Address?.Ulica}");
            GetText(rec.Record, ref sb);

            if (rec.Record?.EmailSource?.Content != null)
            {
                _mimeReader.DeserializeEmail(rec.Record.EmailSource.Content);
                GetText(_mimeReader, ref sb);
            }
        }
    }
}
