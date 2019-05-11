using CopyinfoWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CopyinfoWPF.UControls
{
    /// <summary>
    /// Interaction logic for UcClientOverview.xaml
    /// </summary>
    public partial class UcDeviceOverview : UserControl
    {

        private DeviceOverviewViewModel _viewModel;
        public DeviceOverviewViewModel ViewModel { get => _viewModel ?? (_viewModel = (DeviceOverviewViewModel)this.DataContext); }


        public UcDeviceOverview()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
