using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Globalization;
using CopyinfoWPF.Common;
using CopyinfoWPF.Common.Enums;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Copyinfo.Other
{
    public static class Printing
    {
        public static bool InvokePrinting(string text, string description)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                TextDocumentPaginator myDocumentPaginator = new TextDocumentPaginator(text, printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                printDialog.PrintDocument(myDocumentPaginator, description);
                return true;
            }

            return false;
        }

        public static bool InvokePrinting(IEnumerable<string> documents, string description)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                TextDocumentPaginator myDocumentPaginator = new TextDocumentPaginator(documents, printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
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

