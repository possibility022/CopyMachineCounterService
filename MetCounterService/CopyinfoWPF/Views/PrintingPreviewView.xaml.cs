﻿using CopyinfoWPF.Common;
using CopyinfoWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace CopyinfoWPF.Views
{
    /// <summary>
    /// Interaction logic for PrintingPreview.xaml
    /// </summary>
    public partial class PrintingPreviewView : Window
    {

        PrintingPreviewViewModel _viewModel;

        PrintingPreviewViewModel ViewModel { get => _viewModel ?? (_viewModel = (PrintingPreviewViewModel)DataContext); }

        public PrintingPreviewView()
        {
            InitializeComponent();

            DocumentPaginator paginator = new TextDocumentPaginator(File.ReadAllText(@"D:\Games\Battle.net\World of Warcraft\interface\addons\Singer\core.lua"), 1000, 1000);

            MemoryStream stream = new MemoryStream();

            Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);

            var uri = new Uri(@"memorystream://myXps.xps");
            PackageStore.AddPackage(uri, package);
            var xpsDoc = new XpsDocument(package);

            xpsDoc.Uri = uri;
            XpsDocument.CreateXpsDocumentWriter(xpsDoc).Write(paginator);

            ViewModel.Document = xpsDoc.GetFixedDocumentSequence();
        }
    }
}
