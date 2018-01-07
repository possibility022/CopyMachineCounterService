using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Globalization;
using CopyinfoWPF.Common;
using System;

namespace Copyinfo.Other
{
    public static class Printing
    {
        public static void InvokePrinting(string text)
        {
            throw new Exception();

            //PrintDialog printDialog = new PrintDialog();
            //if (printDialog.ShowDialog() == true)
            //{
            //    TextDocumentPaginator myDocumentPaginator = new TextDocumentPaginator(File.ReadAllText(@"D:\Games\Battle.net\World of Warcraft\interface\addons\Singer\core.lua"), printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            //    printDialog.PrintDocument(myDocumentPaginator, "Wydruk");
            //}
        }
    }
}

