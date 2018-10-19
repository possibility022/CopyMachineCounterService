using System.Collections.Generic;
using Prism.Mvvm;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Windows.Data;
using CopyinfoWPF.DTO.Models;
using System.Windows.Controls;
using System;
using CopyinfoWPF.Views;
using System.Text;
using System.IO;

namespace CopyinfoWPF.ViewModels
{
    class ReportsViewModel : BindableBase
    {
        ICollectionView _records;
        private bool _printButtonEnabled;
        MachineRecordViewModel _selectedRecord;
        private System.Collections.IList _selectedRecords;
        private string _filterText = string.Empty;

        public ObservableCollection<MachineRecordViewModel> _allRecords = new ObservableCollection<MachineRecordViewModel>();

        private ListSortDirection _dateTimeListSortDirection = ListSortDirection.Descending;
        public ListSortDirection DateTimeListSortDirection
        {
            get { return _dateTimeListSortDirection; }
            set { SetProperty(ref _dateTimeListSortDirection, value); }
        }

        public ICollectionView Records
        {
            get { return _records; }
            set { SetProperty(ref _records, value); }
        }

        internal void PrintSelectedItems(DataGrid dataGridWithRecords)
        {
            var window = new PrintingPreviewView();
            var dataContext = (PrintingPreviewViewModel)window.DataContext;
            dataContext.CreatePreview(File.ReadAllText("Resources\\LoremIpsum.txt"));

            window.Show();
        }

        public MachineRecordViewModel SelectedRecord
        {
            get { return _selectedRecord; }
            set { SetProperty(ref _selectedRecord, value); }
        }

        public System.Collections.IList SelectedRecords
        {
            get => _selectedRecords;
            internal set
            {
                SetProperty(ref _selectedRecords, value);
                PrintButtonEnabled = _selectedRecords.Count > 0;
            }
        }

        private Image _documentPrinted;
        public Image DocumentPrinted
        {
            get { return _documentPrinted; }
            set { SetProperty(ref _documentPrinted, value); } 
        }

        private Image _documentNotPrinted;
        public Image DocumentNotPrinted
        {
            get { return _documentNotPrinted; }
            set { SetProperty(ref _documentNotPrinted, value); }
        }

        public string FilterText
        {
            get => _filterText;
            set { SetProperty(ref _filterText, value); Records.Refresh(); }
        }

        public bool PrintButtonEnabled { get => _printButtonEnabled; private set => SetProperty(ref _printButtonEnabled, value); }

        public ReportsViewModel()
        {
            Records = CollectionViewSource.GetDefaultView(new MachineRecordViewModel[] { });
        }

        public void SetRecords(IEnumerable<MachineRecordViewModel> records)
        {
            _allRecords.Clear();
            _allRecords.AddRange(records);
            Records = CollectionViewSource.GetDefaultView(_allRecords);
            Records.Filter = FilterLogic;
            SetDefaultSorting();
        }

        private void SetDefaultSorting()
        {
            if (Records != null && Records.CanSort == true)
            {
                Records.SortDescriptions.Clear();
                Records.SortDescriptions.Add(new SortDescription($"{nameof(MachineRecordViewModel.Record)}.{nameof(ORM.MetCounterServiceDatabase.Machine.Record.ReadDatetime)}", ListSortDirection.Descending));
            }
        }

        private bool FilterLogic(object item)
        {
            var rec = item as MachineRecordViewModel;

            return rec.Record.ReadDatetime.ToString().Contains(FilterText)
                || rec.Record.SerialNumber.Contains(FilterText)
                || rec.Record.CounterBlackAndWhite.ToString().Contains(FilterText)
                || rec.Record.CounterColor.ToString().Contains(FilterText)
                || rec.Record.CounterScanner.ToString().Contains(FilterText)
                || (string.IsNullOrEmpty(rec.Record.TonerLevelBlack) == false && rec.Record.TonerLevelBlack.Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelCyan) == false && rec.Record.TonerLevelCyan.Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelMagenta) == false && rec.Record.TonerLevelMagenta.Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelYellow) == false && rec.Record.TonerLevelYellow.Contains(FilterText));
        }
    }
}
