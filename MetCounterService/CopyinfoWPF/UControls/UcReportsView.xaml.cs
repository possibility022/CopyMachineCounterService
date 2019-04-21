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
            DataContextChanged += UcReportsView_DataContextChanged;
        }

        private void UcReportsView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel.DialogCoordinator = DialogCoordinator.Instance;
        }

        private void dataGridWithRecords_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.OpenSelectedRecord();
        }
    }
}
