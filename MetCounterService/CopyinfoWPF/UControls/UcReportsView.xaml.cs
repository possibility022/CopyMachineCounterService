using CopyinfoWPF.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CopyinfoWPF.UControls
{
    /// <summary>
    /// Interaction logic for UcReportsView.xaml
    /// </summary>
    public partial class UcReportsView : UserControl
    {
        ReportsViewModel _viewModel;

        ReportsViewModel ViewModel { get => _viewModel ?? (_viewModel = (ReportsViewModel)this.DataContext); }

        public UcReportsView()
        {
            InitializeComponent();
            ViewModel.DialogCoordinator = DialogCoordinator.Instance;
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            //ViewModel.RefreshClickAsync(); //todo check this
        }

        private async void OnPrintClick(object sender, RoutedEventArgs e)
        {
            var results = await ViewModel.Print();
        }

        private void dataGridWithRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedRecords = (sender as DataGrid).SelectedItems;
        }
    }
}
