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
            var task = ViewModel.RefreshClickAsync();
        }

        private async void OnPrintClick(object sender, RoutedEventArgs e)
        {
            var results = await ViewModel.Print();
        }

        private void dataGridWithRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedRecords = (sender as DataGrid).SelectedItems;
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                //ViewModel.RefreshFiltersCommand.Execute(null);
                ViewModel.ApplyFilters();
        }

        private void dataGridWithRecords_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.OpenSelectedRecord();
        }
    }
}
