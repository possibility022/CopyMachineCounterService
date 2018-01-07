using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using CopyinfoWPF.Database;
using System.Threading.Tasks;
using Copyinfo.Other;
using CopyinfoWPF.Views;

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

        ObservableCollection<MachineRecord> _selectedRecords;
        public ObservableCollection<MachineRecord> SelectedRecords
        {
            get { return _selectedRecords; }
            set { SetProperty(ref _selectedRecords, value); }
        }

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

        public void PrintSelectedItems()
        {
            PrintingPreviewView printing = new PrintingPreviewView();
            printing.Show();
            //Printing.InvokePrinting("TEXT");
        }
    }
}
