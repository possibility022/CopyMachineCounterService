using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using CopyinfoWPF.Workflows;

using System.ComponentModel;
using System.Windows.Data;
using CopyinfoWPF.Model;
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
            Records = CollectionViewSource.GetDefaultView(new List<Record>());
        }

        private void ApplyFilter()
        {
            //Records.Clear();
            //Records.AddRange(_allRecords.Where(FilterLogic));
        }

        public void SetRecords(IEnumerable<Record> records)
        {
            _allRecords.Clear();
            _allRecords.AddRange(records);
            Records = CollectionViewSource.GetDefaultView(_allRecords);
            Records.Filter = FilterLogic;
        }

        private bool FilterLogic(object item)
        {
            var rec = item as Record;
            return true;
            //return rec.datetime.ToString().Contains(FilterText)
            //    || rec.serial_number.Contains(FilterText)
            //    || rec.print_counter_black_and_white.ToString().Contains(FilterText)
            //    || rec.print_counter_color.ToString().Contains(FilterText)
            //    || rec.scan_counter.ToString().Contains(FilterText)
            //    || rec.tonerlevel_c.Contains(FilterText)
            //    || rec.tonerlevel_y.Contains(FilterText)
            //    || rec.tonerlevel_m.Contains(FilterText)
            //    || rec.tonerlevel_k.Contains(FilterText);
        }
    }
}
