using CopyinfoWPF.ViewModels;
using System;
using System.Collections.Generic;
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

namespace CopyinfoWPF.Views
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : Window
    {
        public OverviewView()
        {
            InitializeComponent();
        }

        public OverviewView(ClientOverviewViewModel clientOverviewViewModel, DeviceOverviewViewModel deviceOverviewViewModel, ReportOverviewViewModel reportOverviewViewModel) 
            : this()
        {
            ClientControl.DataContext = clientOverviewViewModel;
            DeviceControl.DataContext = deviceOverviewViewModel;
            ReportControl.DataContext = reportOverviewViewModel;
        }
    }
}
