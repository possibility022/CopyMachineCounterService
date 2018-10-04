using System.Collections.Generic;
using Prism.Mvvm;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Windows.Data;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;

namespace CopyinfoWPF.ViewModels
{
    class ReportsViewModel : BindableBase
    {
        ICollectionView _records;
        private bool _printButtonEnabled;
        Record _selectedRecord;
        private System.Collections.IList _selectedRecords;
        private string _filterText = string.Empty;

        public ObservableCollection<Record> _allRecords = new ObservableCollection<Record>();

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

        public Record SelectedRecord
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

        public string FilterText
        {
            get => _filterText;
            set { SetProperty(ref _filterText, value); Records.Refresh(); }
        }

        public bool PrintButtonEnabled { get => _printButtonEnabled; private set => SetProperty(ref _printButtonEnabled, value); }

        public ReportsViewModel()
        {
            Records = CollectionViewSource.GetDefaultView(new Record[] { });
        }

        public void SetRecords(IEnumerable<Record> records)
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
                Records.SortDescriptions.Add(new SortDescription(nameof(Record.ReadDatetime), ListSortDirection.Ascending));
            }
        }

        private bool FilterLogic(object item)
        {
            var rec = item as Record;

            return rec.ReadDatetime.ToString().Contains(FilterText)
                || rec.SerialNumber.Contains(FilterText)
                || rec.CounterBlackAndWhite.ToString().Contains(FilterText)
                || rec.CounterColor.ToString().Contains(FilterText)
                || rec.CounterScanner.ToString().Contains(FilterText)
                || (string.IsNullOrEmpty(rec.TonerLevelBlack) == false && rec.TonerLevelBlack.Contains(FilterText))
                || (string.IsNullOrEmpty(rec.TonerLevelCyan) == false && rec.TonerLevelCyan.Contains(FilterText))
                || (string.IsNullOrEmpty(rec.TonerLevelMagenta) == false && rec.TonerLevelMagenta.Contains(FilterText))
                || (string.IsNullOrEmpty(rec.TonerLevelYellow) == false && rec.TonerLevelYellow.Contains(FilterText));
        }
    }
}
