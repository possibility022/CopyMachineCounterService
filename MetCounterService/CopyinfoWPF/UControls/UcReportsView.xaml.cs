using CopyinfoWPF.ViewModels;
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
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            //ViewModel.RefreshClickAsync(); //todo check this
        }

        private void OnPrintClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Print();
        }

        private void dataGridWithRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedRecords = (sender as DataGrid).SelectedItems;
        }
    }
}
