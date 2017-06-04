using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Copyinfo.Other
{
    class Printing
    {
        private static void Print(string text, PrintDialog dialog)
        {
            string stringToPrint = text;

            PrintDocument p = new PrintDocument();
            p.PrinterSettings = dialog.PrinterSettings;


            p.PrintPage += delegate (object sender1, PrintPageEventArgs e)
            {
                //e1.Graphics.DrawString(s, new Font("Microsoft Sans Serif", 9), new SolidBrush(Color.Black), new RectangleF(p.DefaultPageSettings.Margins.Left, p.DefaultPageSettings.Margins.Right, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                int charactersOnPage = 0;
                int linesPerPage = 0;

                // Sets the value of charactersOnPage to the number of characters 
                // of stringToPrint that will fit within the bounds of the page.
                e.Graphics.MeasureString(stringToPrint, new Font("Microsoft Sans Serif", 9),
                    e.MarginBounds.Size, StringFormat.GenericTypographic,
                    out charactersOnPage, out linesPerPage);

                // Draws the string within the bounds of the page
                e.Graphics.DrawString(stringToPrint, new Font("Microsoft Sans Serif", 9), Brushes.Black,
                    e.MarginBounds, StringFormat.GenericTypographic);

                // Remove the portion of the string that has been printed.
                stringToPrint = stringToPrint.Substring(charactersOnPage);

                // Check to see if more pages are to be printed.
                e.HasMorePages = (stringToPrint.Length > 0);
            };

            try
            {
                PrinterSettings settings = new PrinterSettings();
                PageSettings pageSettings = new PageSettings();

                settings.PrintToFile = true;
                pageSettings.Margins = new Margins(50, 50, 50, 50);
                p.DefaultPageSettings.Margins = pageSettings.Margins;
                p.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

        public static DialogResult Print(string[] texts)
        {
            PrintDialog dialog = new PrintDialog();
            dialog.UseEXDialog = true;

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return result; 

            foreach (string text in texts)
                Print(text, dialog);

            return result;
        }

        public static DialogResult Print(List<string> texts)
        {
            return Print(texts.ToArray());
        }

        public static DialogResult Print(string text)
        {
            PrintDialog dialog = new PrintDialog();
            dialog.UseEXDialog = true;

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return result;

            Print(text, dialog);
            return result;
        }

        public static DialogResult Print(Database.MachineRecord record)
        {
            DialogResult results =  Print(record.GetTextToPrint());
            if (results == DialogResult.OK)
                record.SetPrintedTrue();
            return results;
        }

        public static DialogResult PrintThisMonthReport()
        {
            List<Database.Device> devices = Database.DAO.GetAllDevices();
            List<Database.MachineRecord> toPrint = new List<Database.MachineRecord>();

            toPrint = GetOneRecordPerDevice(devices);

            return Print(toPrint);
        }

        private static List<Database.MachineRecord> GetOneRecordPerDevice(List<Database.Device> devices)
        {
            List<Database.MachineRecord> toPrint = new List<Database.MachineRecord>();
            
            foreach (Database.Device d in devices)
            {
                Database.MachineRecord rec = d.GetOneRecord(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                if (rec != null)
                    if (rec.serial_number != "")
                        toPrint.Add(rec);
            }
            return toPrint;
        }

        private async static Task<List<Database.MachineRecord>> GetOneRecordPerDeviceAsync()
        {
            List<Database.Device> devices = await Database.DAO.GetAllDevicesAsync();

            return await Task.Run(() => GetOneRecordPerDevice(devices));
        }

        public async static void PrintThisMonthReportBackground(Action doWhenFinished)
        {
            List<Database.MachineRecord> rec = await GetOneRecordPerDeviceAsync();
            Print(rec);
            doWhenFinished();
        }

        public static DialogResult Print(List<Database.MachineRecord> records)
        {
            if (MessageBox.Show("Czy wydrukować tylko te dane, które nie były drukowane?", "Co drukujemy?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<string> toPrint = new List<string>();
                foreach(Database.MachineRecord record in records)
                {
                    if (record == null)
                        continue;
                    if (record.printed == true)
                        continue;
                    else
                    {
                        if (record.serial_number != "")
                            toPrint.Add(record.GetTextToPrint());
                    }
                }

                DialogResult results =  Print(toPrint);
                if (results == DialogResult.OK)
                {
                    foreach (Database.MachineRecord record in records)
                        if (record != null)
                            if (record.serial_number != "") record.SetPrintedTrue();
                }

                return results;
            }
            else
            {
                List<string> toPrint = new List<string>();
                foreach (Database.MachineRecord record in records)
                {
                    if (record != null)
                        if (record.serial_number != "")
                            toPrint.Add(record.GetTextToPrint());
                }

                DialogResult results =  Print(toPrint);
                if (results == DialogResult.OK)
                {
                    foreach (Database.MachineRecord record in records)
                        if (record != null)
                            if (record.serial_number != "") record.SetPrintedTrue();
                }

                return results;
            }

        }
    }
}
