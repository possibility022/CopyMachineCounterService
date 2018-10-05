using System.Windows.Controls;
using CopyinfoWPF.Common.Enums;
using System.Collections.Generic;

namespace CopyinfoWPF.Common
{
    public static class Printing
    {
        public static bool InvokePrinting(string text, string description)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var myDocumentPaginator = new TextDocumentPaginator(text, printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                printDialog.PrintDocument(myDocumentPaginator, description);
                return true;
            }

            return false;
        }

        public static bool InvokePrinting(IEnumerable<string> documents, string description)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var myDocumentPaginator = new TextDocumentPaginator(documents, printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                printDialog.PrintDocument(myDocumentPaginator, description);
                return true;
            }

            return false;
        }

        public static TextDocumentPaginator GetA4Preview(string text)
        {
            return new TextDocumentPaginator(text, CommonData.PageSizes[PageSizes.A4]);
        }
    }
}

