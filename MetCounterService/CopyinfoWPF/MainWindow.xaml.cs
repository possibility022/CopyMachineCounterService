using CopyinfoWPF.Model;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CopyinfoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ReportsViewModel _reportsViewModel;

        ReportsViewModel ReportsViewModel { get => _reportsViewModel ?? (_reportsViewModel = (ReportsViewModel)Reports.DataContext); }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IEnumerable<Record> records) : this()
        {
            ReportsViewModel.SetRecords(records);
        }
    }
}
