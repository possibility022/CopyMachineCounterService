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
using System.Windows.Input;
using CopyinfoWPF.Commands;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace CopyinfoWPF.ViewModels
{
    class ReportsViewModel : BindableBase
    {
        ICollectionView _records;
        private bool _printButtonEnabled;
        MachineRecordViewModel _selectedRecord;
        private System.Collections.IList _selectedRecords;
        private string _filterText = string.Empty;
        IFormatter<MachineRecordViewModel> _recordFormatter;
        readonly IMachineRecordService _machineRecordService;
        private IDialogCoordinator _dialogCoordinator;

        public ObservableCollection<MachineRecordViewModel> _allRecords = new ObservableCollection<MachineRecordViewModel>();

        private Func<MachineRecordViewModel, bool> SelectOnlyPrintedRecord = (f => f.Printed == false);
        private Func<MachineRecordViewModel, bool> SelectAllRecords = (f => true);

        private ListSortDirection _dateTimeListSortDirection = ListSortDirection.Descending;
        public ListSortDirection DateTimeListSortDirection
        {
            get { return _dateTimeListSortDirection; }
            set { SetProperty(ref _dateTimeListSortDirection, value); }
        }

        public ICommand PrintOptionCommand { get; set; }

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

        public ObservableCollection<string> PrintingOptions { get; private set; }

        public bool PrintButtonEnabled { get => _printButtonEnabled; private set => SetProperty(ref _printButtonEnabled, value); }

        public IDialogCoordinator DialogCoordinator
        {
            get => _dialogCoordinator;
            set => SetProperty(ref _dialogCoordinator, value);
        }

        public ReportsViewModel()
        {
            Records = CollectionViewSource.GetDefaultView(new MachineRecordViewModel[] { });
            PrintingOptions = new ObservableCollection<string> { "Podgląd wydruku", "Drukuj wszystkie zaznaczone", "Podgląd wydruku - Wszystkie zaznaczone" };
            PrintOptionCommand = new PrintOptions(PrintOption);
            _recordFormatter = Configuration.Configuration.Container.Resolve<IFormatter<MachineRecordViewModel>>();
            _machineRecordService = Configuration.Configuration.Container.Resolve<IMachineRecordService>();
        }

        private PrintingPreview GetPrintingPreview(out ICollection<MachineRecordViewModel> selectedRecords, Func<MachineRecordViewModel, bool> selector)
        {
            selectedRecords = GetSelected(selector).ToList();

            if (!selectedRecords.Any())
                return null;

            _machineRecordService.RefreshViewModels(selectedRecords);
            var preview = new PrintingPreview();
            selectedRecords = GetSelected(selector).ToList();
            preview.CreateDocument(_recordFormatter.GetText(selectedRecords));
            return preview;
        }

        private async Task<bool> PrintOption(string option)
        {
            switch (option)
            {
                case "Podgląd wydruku":
                    return await PrintPreview(SelectOnlyPrintedRecord);

                case "Drukuj wszystkie zaznaczone":
                    return await PrintSelectedItems(SelectAllRecords);

                case "Podgląd wydruku - Wszystkie zaznaczone":
                    return await PrintPreview(SelectAllRecords);
            }

            return false;
        }

        internal async Task<bool> PrintPreview(Func<MachineRecordViewModel, bool> selector)
        {
            var printingPreview = GetPrintingPreview(out _, selector);
            if (printingPreview == null)
            {
                //this.ShowMessageAsync("This is the title", "Some message");
                var results = await ShowWrongPrintoutDialog();
                if (results == MessageDialogResult.Affirmative)
                    printingPreview = GetPrintingPreview(out _, SelectAllRecords);
                else
                    return false;
            }

            var dataContext = new PrintingPreviewViewModel(printingPreview);

            var window = new PrintingPreviewView()
            {
                DataContext = dataContext
            };
            window.Show();
            return true;
        }

        public async Task<bool> Print()
        {
            return await PrintSelectedItems(SelectOnlyPrintedRecord);
        }

        private async Task<MessageDialogResult> ShowWrongPrintoutDialog()
        {
            return await DialogCoordinator.ShowMessageAsync(this, string.Empty, "Wybrane liczniki zostały już wydrukowane, czy wydrukować je jeszcze raz?", MessageDialogStyle.AffirmativeAndNegative);
        }

        public async Task<bool> PrintSelectedItems(Func<MachineRecordViewModel, bool> selector)
        {
            ICollection<MachineRecordViewModel> selected;
            var preview = GetPrintingPreview(out selected, selector);
            if (!selected.Any())
            {
                var printAll = await ShowWrongPrintoutDialog();
                if (printAll == MessageDialogResult.Affirmative)
                    preview = GetPrintingPreview(out selected, SelectAllRecords);
                else
                    return false;
            }

            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                printDialog
                    .PrintDocument(preview
                    .XpsDocument
                    .GetFixedDocumentSequence()
                    .DocumentPaginator, "Copyinfo Print");

                _machineRecordService.SetPrinted(selected);
                return true;
            }

            return false;
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
