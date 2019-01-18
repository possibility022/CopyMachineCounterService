using System.Collections.Generic;
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
    public class ReportsViewModel : PageViewBase<MachineRecordRowView>
    {
        private string _filterText = string.Empty;
        IFormatter<MachineRecordRowView> _recordFormatter;
        readonly IMachineRecordService _machineRecordService;
        private IDialogCoordinator _dialogCoordinator;
        private bool _canRefresh = true;

        public override string ViewName => "Reports";

        private Func<MachineRecordRowView, bool> SelectOnlyPrintedRecord = (f => f.Printed == false);
        private Func<MachineRecordRowView, bool> SelectAllRecords = (f => true);

        public ICommand PrintOptionCommand { get; private set; }
        public ICommand RefreshFiltersCommand { get; private set; }

        private Image _documentPrinted;
        public Image DocumentPrinted
        {
            get { return _documentPrinted; }
            set { SetProperty(ref _documentPrinted, value); }
        }

        private Image _documentNotPrinted;

        private ICommand _refreshCommand;
        public override ICommand RefreshCommand { get => _refreshCommand; protected set => _refreshCommand = value; }

        public Image DocumentNotPrinted
        {
            get { return _documentNotPrinted; }
            set { SetProperty(ref _documentNotPrinted, value); }
        }

        public string FilterText
        {
            get => _filterText;
            set { SetProperty(ref _filterText, value.ToLower()); }
        }

        public ObservableCollection<string> PrintingOptions { get; private set; }

        public IDialogCoordinator DialogCoordinator
        {
            get => _dialogCoordinator;
            set => SetProperty(ref _dialogCoordinator, value);
        }

        public ReportsViewModel()
        {
            Collection = CollectionViewSource.GetDefaultView(new MachineRecordRowView[] { });
            SetDefaultSorting();
            PrintingOptions = new ObservableCollection<string> { "Podgląd wydruku", "Drukuj wszystkie zaznaczone", "Podgląd wydruku - Wszystkie zaznaczone" };
            PrintOptionCommand = new PrintOptions(PrintOption);
            RefreshFiltersCommand = new BaseCommand(Collection.Refresh);
            RefreshCommand = new AsyncCommand(RefreshClickAsync, CanRefresh);
            _recordFormatter = Configuration.Configuration.Container.Resolve<IFormatter<MachineRecordRowView>>();
            _machineRecordService = Configuration.Configuration.Container.Resolve<IMachineRecordService>();
            _baseService = _machineRecordService;
        }

        public void ApplyFilters()
        {
            Collection.Refresh();
        }

        private PrintingPreview GetPrintingPreview(out ICollection<MachineRecordRowView> selectedRecords, Func<MachineRecordRowView, bool> selector)
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

        internal async Task<bool> PrintPreview(Func<MachineRecordRowView, bool> selector)
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

        public async Task<bool> PrintSelectedItems(Func<MachineRecordRowView, bool> selector)
        {
            ICollection<MachineRecordRowView> selected;
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

        private IEnumerable<MachineRecordRowView> GetSelected(Func<MachineRecordRowView, bool> filter)
        {
            if (SelectedRecords == null)
                yield break;

            foreach (var rec in SelectedRecords)
            {
                var r = rec as MachineRecordRowView;
                if (r != null && filter.Invoke(r))
                    yield return r;
            }
        }

        public void SetRecords(IEnumerable<MachineRecordRowView> records)
        {
            Loaded = true;
            _sourceCollection.Clear();
            _sourceCollection.AddRange(records);
            
            Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            Collection.Filter = FilterLogic;
            SetDefaultSorting();
        }

        public async Task RefreshClickAsync()
        {
            _canRefresh = false;
            var records = await Task.Factory.StartNew(GetRecords);
            _sourceCollection.Clear();
            _sourceCollection.AddRange(records);
            Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            _canRefresh = true;
        }

        private bool CanRefresh()
        {
            return _canRefresh;
        }

        private IEnumerable<MachineRecordRowView> GetRecords()
        {
            _machineRecordService.RefreshCache();
            return _machineRecordService.GetAll();
        }

        private void SetDefaultSorting()
        {
            if (Collection != null && Collection.CanSort == true)
            {
                Collection.SortDescriptions.Clear();
                Collection.SortDescriptions.Add(new SortDescription($"{nameof(MachineRecordRowView.Record)}.{nameof(ORM.MetCounterServiceDatabase.Machine.Record.ReadDatetime)}", ListSortDirection.Descending));
            }
        }

        private bool FilterLogic(object item)
        {
            var rec = item as MachineRecordRowView;

            return rec.Record.ReadDatetime.ToString().ToLower().Contains(FilterText)
                || rec.Record.SerialNumber.ToLower().Contains(FilterText)
                || rec.Record.CounterBlackAndWhite.ToString().ToLower().Contains(FilterText)
                || rec.Record.CounterColor.ToString().ToLower().Contains(FilterText)
                || rec.Record.CounterScanner.ToString().ToLower().Contains(FilterText)
                || rec.ClientName.ToLower().Contains(FilterText)
                || (rec.Address != null && rec.Address.Ulica.ToLower().Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelBlack) == false && rec.Record.TonerLevelBlack.ToLower().Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelCyan) == false && rec.Record.TonerLevelCyan.ToLower().Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelMagenta) == false && rec.Record.TonerLevelMagenta.ToLower().Contains(FilterText))
                || (string.IsNullOrEmpty(rec.Record.TonerLevelYellow) == false && rec.Record.TonerLevelYellow.ToLower().Contains(FilterText));
        }

        internal void OpenSelectedRecord()
        {
            //var clientOverviewViewModel = new ClientOverviewViewModel(SelectedRecord?.Client, _machineRecordService);
            //var deviceOverviewViewModel = new DeviceOverviewViewModel(_machineRecordService, SelectedRecord?.Device);
            //var reportOverviewViewModel = new ReportOverviewViewModel(
            //    Configuration.Configuration.Container.Resolve<IFormatter<EmailMessage>>(),
            //    Configuration.Configuration.Container.Resolve<IFormatter<RecordViewModel>>());

            //clientOverviewViewModel.DeviceSelected += deviceOverviewViewModel.OnDeviceSelected;
            //deviceOverviewViewModel.RecordSelected += reportOverviewViewModel.OnRecordSelected;

            //new OverviewView(clientOverviewViewModel, deviceOverviewViewModel, reportOverviewViewModel)
            //    .Show();
        }
    }
}
