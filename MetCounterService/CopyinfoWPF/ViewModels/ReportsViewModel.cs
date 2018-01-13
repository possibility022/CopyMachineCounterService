using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using CopyinfoWPF.Database;
using System.Threading.Tasks;
using Copyinfo.Other;
using CopyinfoWPF.Views;
using System.Windows.Controls;
using CopyinfoWPF.Workflows;

namespace CopyinfoWPF.ViewModels
{
    class ReportsViewModel : BindableBase
    {
        ObservableCollection<MachineRecord> _records;
        public ObservableCollection<MachineRecord> Records
        {
            get { return _records; }
            set { SetProperty(ref _records, value); }
        }

        MachineRecord _selectedRecord;

        public MachineRecord SelectedRecord
        {
            get { return _selectedRecord; }
            set { SetProperty(ref _selectedRecord, value); }
        }

        private System.Collections.IList _selectedRecords;
        private bool _printButtonEnabled;

        public System.Collections.IList SelectedRecords
        {
            get => _selectedRecords;
            internal set
            {
                SetProperty(ref _selectedRecords, value);
                PrintButtonEnabled = _selectedRecords.Count > 0;
            }
        }

        public bool PrintButtonEnabled { get => _printButtonEnabled; private set => SetProperty(ref _printButtonEnabled, value); }

        public ReportsViewModel()
        {

        }

        public void Add()
        {

        }

        public async Task RefreshClickAsync()
        {
            Records = new ObservableCollection<MachineRecord>(await DAO.GetAllReportsAsync());
        }

        public void PrintSelectedItems(DataGrid dataGrid)
        {
            PrintingWorkflow.Print(SelectedRecords.Cast<MachineRecord>().ToList());
        }
    }
}
