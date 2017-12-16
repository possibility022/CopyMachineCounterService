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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ViewModel.RefreshClickAsync(); //todo check this
        }
    }
}
