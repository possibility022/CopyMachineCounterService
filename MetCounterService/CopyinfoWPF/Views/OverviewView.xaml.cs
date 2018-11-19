using CopyinfoWPF.ViewModels;
using MahApps.Metro.Controls;

namespace CopyinfoWPF.Views
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : MetroWindow
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
