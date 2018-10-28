﻿using System.Collections.Generic;
using Prism.Mvvm;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Windows.Data;
using CopyinfoWPF.DTO.Models;
using System.Windows.Controls;
using System;
using CopyinfoWPF.Views;
using CopyinfoWPF.Interfaces.Formatters;
using Unity;
using CopyinfoWPF.Workflows.Printing;
using CopyinfoWPF.Services.Interfaces;
using System.Linq;

namespace CopyinfoWPF.ViewModels
{
    class ReportsViewModel : BindableBase
    {
        ICollectionView _records;
        private bool _printButtonEnabled;
        MachineRecordViewModel _selectedRecord;
        private System.Collections.IList _selectedRecords;
        private string _filterText = string.Empty;
        IRecordToTextFormatter _recordFormatter;
        readonly IMachineRecordService _machineRecordService;

        public ObservableCollection<MachineRecordViewModel> _allRecords = new ObservableCollection<MachineRecordViewModel>();

        private Func<MachineRecordViewModel, bool> SelectOnlyPrintedRecord = (f => f.Printed == false);
        private Func<MachineRecordViewModel, bool> SelectAllRecords = (f => true);

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
            _recordFormatter = Configuration.Configuration.Container.Resolve<IRecordToTextFormatter>();
            _machineRecordService = Configuration.Configuration.Container.Resolve<IMachineRecordService>();
        }

        private PrintingPreview GetPrintingPreview(out ICollection<MachineRecordViewModel> selectedRecords)
        {
            _machineRecordService.RefreshViewModels(GetSelected(SelectOnlyPrintedRecord));
            var preview = new PrintingPreview();
            selectedRecords = GetSelected(SelectOnlyPrintedRecord).ToList();
            preview.CreateDocument(_recordFormatter.GetText(selectedRecords));
            return preview;
        }

        internal void PrintPreview()
        {
            var dataContext = new PrintingPreviewViewModel(GetPrintingPreview(out _));

            var window = new PrintingPreviewView()
            {
                DataContext = dataContext
            };
            window.Show();
        }

        public void PrintSelectedItems()
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                ICollection<MachineRecordViewModel> selected;
                printDialog.PrintDocument(GetPrintingPreview(out selected).XpsDocument.GetFixedDocumentSequence().DocumentPaginator, "Copyinfo Print");
                _machineRecordService.SetPrinted(selected);
            }
        }

        private IEnumerable<MachineRecordViewModel> GetSelected(Func<MachineRecordViewModel, bool> filter)
        {
            if (SelectedRecords == null)
                yield break;

            foreach (var rec in SelectedRecords)
            {
                var r = rec as MachineRecordViewModel;
                if (r != null && filter.Invoke(r))
                    yield return r;
            }
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
