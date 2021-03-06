﻿using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.Workflows.Email;
using System.Collections.Generic;
using System.Text;

namespace CopyinfoWPF.Formatters
{
    public class RecordFormatter : IFormatter<MachineRecordRowView>, IFormatter<Record>, IFormatter<EmailMessage>, IFormatter<RecordViewModel>
    {

        public IEnumerable<string> GetText(IEnumerable<MachineRecordRowView> records)
        {
            var sb = new StringBuilder();

            foreach (var rec in records)
            {
                sb.Clear();

                RecToString(rec, ref sb);

                yield return sb.ToString();
            }
        }

        public StringBuilder GetText(MachineRecordRowView record)
        {
            var sb = new StringBuilder();
            RecToString(record, ref sb);
            return sb;
        }

        public IEnumerable<string> GetText(IEnumerable<EmailMessage> items)
        {
            var sb = new StringBuilder();

            foreach (var mime in items)
            {
                GetText(mime, ref sb);
                yield return sb.ToString();
                sb.Clear();
            }
        }

        public StringBuilder GetText(EmailMessage item)
        {
            var sb = new StringBuilder();
            GetText(item, ref sb);
            return sb;
        }

        private void GetText(EmailMessage item, ref StringBuilder sb)
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

        private void RecToString(MachineRecordRowView rec, ref StringBuilder sb)
        {
            sb.AppendLine($"Klient: {rec.ClientName} NIP: {rec.Client?.Nip}");
            sb.AppendLine($"Producent: {rec.Device?.ModelUrzadzenia?.MarkaUrzadzenia?.Nazwa1}");
            sb.AppendLine($"Model: {rec.Device?.ModelUrzadzenia?.Nazwa1}");
            sb.AppendLine($"Adres: {rec.Address?.Ulica}");
            GetText(rec.Record, ref sb);

            if (rec.Record?.EmailSource?.Content != null)
            {
                var email = new EmailMessage(rec.Record.EmailSource.Content);
                GetText(email, ref sb);
            }
        }

        public IEnumerable<string> GetText(IEnumerable<RecordViewModel> items)
        {
            var sb = new StringBuilder();
            
            foreach(var r in items)
            {
                RecViewModelToString(r, ref sb);
                yield return sb.ToString();
                sb.Clear();
            }
        }

        public StringBuilder GetText(RecordViewModel item)
        {
            var sb = new StringBuilder();
            RecViewModelToString(item, ref sb);
            return sb;
        }

        private void RecViewModelToString(RecordViewModel rec, ref StringBuilder sb)
        {
            if (rec.Source == ORM.DatabaseType.Assystent)
            {
                sb.AppendLine($"Serwisant: {rec.ServiceMan}");
                sb.AppendLine($"Data: {rec.DateTime}");
                sb.AppendLine();
                sb.AppendLine(rec.TextContent);
            }
        }
    }
}
