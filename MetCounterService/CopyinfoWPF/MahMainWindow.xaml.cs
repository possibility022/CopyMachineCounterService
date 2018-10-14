using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ViewModels;
using MahApps.Metro.Controls;
using System.Collections.Generic;

namespace CopyinfoWPF
{
    /// <summary>
    /// Interaction logic for MahMainWindow.xaml
    /// </summary>
    public partial class MahMainWindow : MetroWindow
    {

        ReportsViewModel _reportsViewModel;

        ReportsViewModel ReportsViewModel { get => _reportsViewModel ?? (_reportsViewModel = (ReportsViewModel)Reports.DataContext); }


        public MahMainWindow(IEnumerable<MachineRecord> records) : this()
        {
            ReportsViewModel.SetRecords(records);
        }

        public MahMainWindow()
        {
            InitializeComponent();
        }
    }
}
