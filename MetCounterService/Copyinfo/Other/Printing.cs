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
        private static void print(string text, PrintDialog dialog)
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

        public static void print(string[] texts)
        {
            PrintDialog dialog = new PrintDialog();
            dialog.UseEXDialog = true;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string text in texts)
                print(text, dialog);
        }

        public static void print(List<string> texts)
        {
            print(texts.ToArray());
        }

        public static void print(string text)
        {
            PrintDialog dialog = new PrintDialog();
            dialog.UseEXDialog = true;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            print(text, dialog);
        }
    }
}
