using CopyinfoWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CopyinfoWPF.UControls
{
    /// <summary>
    /// Interaction logic for UcClientOverview.xaml
    /// </summary>
    public partial class UcClientOverview : UserControl
    {

        private ClientOverviewViewModel _viewModel;
        public ClientOverviewViewModel ViewModel { get => _viewModel ?? (_viewModel = (ClientOverviewViewModel)this.DataContext); }


        public UcClientOverview()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.DeviceDataGridLoaded();
        }
    }
}
