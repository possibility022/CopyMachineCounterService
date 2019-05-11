using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CopyinfoWPF.DTO.Models;
using System.Windows.Controls;
using System;
using CopyinfoWPF.Views;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.Workflows.Printing;
using CopyinfoWPF.Services.Interfaces;
using System.Linq;
using System.Windows.Input;
using CopyinfoWPF.Commands;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using CopyinfoWPF.Configuration;
using CompareThis;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System.Globalization;

namespace CopyinfoWPF.ViewModels
{
    public class ReportsViewModel : PageViewBase<MachineRecordRowView>
    {
        public ReportsViewModel(IMachineRecordService machineRecordService) : base(machineRecordService)
        {
            FilterLogic = BuidFilterFunc();
            Collection = CollectionViewSource.GetDefaultView(new MachineRecordRowView[] { });
            SetDefaultSorting();
            PrintingOptions = new ObservableCollection<string> { "Podgląd wydruku", "Drukuj wszystkie zaznaczone", "Podgląd wydruku - Wszystkie zaznaczone" };
            PrintCommand = new AsyncCommand(Print, CanPrint);
            PrintOptionCommand = new PrintOptions(PrintOption);
            DataGridDoubleClickCommand = new BaseCommand(OpenSelectedRecord);
            FilterTextKeyUpCommand = new BaseCommand(ApplyFilters);
            RefreshCommand = new AsyncCommand(RefreshAsync, CanRefresh);
            _recordFormatter = UnityConfiguration.Resolve<IFormatter<MachineRecordRowView>>();
            _machineRecordService = machineRecordService;
        }

        public ReportsViewModel() : this(UnityConfiguration.Resolve<IMachineRecordService>())
        {

        }

        IFormatter<MachineRecordRowView> _recordFormatter;
        readonly IMachineRecordService _machineRecordService;
        private IDialogCoordinator _dialogCoordinator;

        public override string ViewName => "Raporty";

        private Func<MachineRecordRowView, bool> SelectOnlyPrintedRecord = (f => f.Printed == false);
        private Func<MachineRecordRowView, bool> SelectAllRecords = (f => true);

        public ICommand PrintCommand { get; private set; }
        public ICommand PrintOptionCommand { get; private set; }

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

        public ObservableCollection<string> PrintingOptions { get; private set; }

        public IDialogCoordinator DialogCoordinator
        {
            get => _dialogCoordinator;
            set => SetProperty(ref _dialogCoordinator, value);
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

        private bool CanPrint(object parameter)
        {
            return SelectedItems.Count > 0;
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
            if (SelectedItems == null)
                yield break;
            
            foreach (var rec in SelectedItems)
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
            Collection.Filter = FilterCollection;
            SetDefaultSorting();
        }

        public async Task RefreshAsync()
        {
            _canRefresh = false;
            var records = await Task.Factory.StartNew(GetRecords);
            _sourceCollection.Clear();
            _sourceCollection.AddRange(records);
            Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            _canRefresh = true;
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

        internal void OpenSelectedRecord()
        {
            var selectedRow = (SelectedItems?.FirstOrDefault() as MachineRecordRowView);

            var clientOverviewViewModel = new ClientOverviewViewModel(selectedRow?.Device.NrFabryczny);
            var deviceOverviewViewModel = UnityConfiguration.Resolve<DeviceOverviewViewModel>();
            var reportOverviewViewModel = UnityConfiguration.Resolve<ReportOverviewViewModel>();

            clientOverviewViewModel.LoadClient(selectedRow?.Client);
            deviceOverviewViewModel.SetRecordToSelect(selectedRow?.Record.Id);

            clientOverviewViewModel.DeviceSelected += deviceOverviewViewModel.OnDeviceSelected;
            deviceOverviewViewModel.RecordSelected += reportOverviewViewModel.OnRecordSelected;

            new OverviewView(clientOverviewViewModel, deviceOverviewViewModel, reportOverviewViewModel)
                .Show();
        }

        private static CompareThis.Settings ConfigureSettings()
        {
            var settings = new CompareThis.Settings()
            {
                StringCompareOptions = CompareOptions.IgnoreCase
            };

            settings.SetStandardWhiteList();
            return settings;
        }

        private static Func<MachineRecordRowView, string, bool> BuidFilterFunc()
        {
            var settings = ConfigureSettings();
            settings.Deep = 1;
            settings.AddPropertyToWhiteList(typeof(int));
            settings.AddPropertyToWhiteList(typeof(string));
            settings.AddPropertyToWhiteList(typeof(DateTime));

            var recFunction = CompareThis.CompareFactory.BuildContainsFunc<Record>(settings);

            var function = new Func<MachineRecordRowView, string, bool>((r, f) => recFunction.Invoke(r.Record, f) 
            || CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.ClientName, f, CompareOptions.IgnoreCase) >= 0
            || ((r.Address.Ulica) != null && CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.Address.Ulica, f, CompareOptions.IgnoreCase) >= 0)
            || ((r.Device?.ModelUrzadzenia?.Nazwa1) != null && CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.Device?.ModelUrzadzenia?.Nazwa1, f, CompareOptions.IgnoreCase) >= 0)
            );

            return function;
        }
    }
}
