using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.ViewModels;
using MahApps.Metro.Controls;
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

namespace CopyinfoWPF
{
    /// <summary>
    /// Interaction logic for MahMainWindow.xaml
    /// </summary>
    public partial class MahMainWindow : MetroWindow
    {

        ReportsViewModel _reportsViewModel;

        ReportsViewModel ReportsViewModel { get => _reportsViewModel ?? (_reportsViewModel = (ReportsViewModel)Reports.DataContext); }


        public MahMainWindow(IEnumerable<Record> records) : this()
        {
            ReportsViewModel.SetRecords(records);
        }

        public MahMainWindow()
        {
            InitializeComponent();
        }
    }
}
