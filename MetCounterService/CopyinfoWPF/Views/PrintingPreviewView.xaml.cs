using CopyinfoWPF.Common;
using CopyinfoWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
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

        private bool _rendered = false;

        public PrintingPreviewView()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DocViewer.FitToHeight();
            DocViewer.FitToWidth();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                if (!_rendered && DocViewer.IsVisible)
                {
                    Dispatcher.Invoke(DocViewer.FitToHeight);
                    Dispatcher.Invoke(DocViewer.FitToWidth);
                    _rendered = true;
                }
            });
        }

        
    }
}
