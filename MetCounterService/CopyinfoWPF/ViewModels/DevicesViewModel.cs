using CompareThis;
using CopyinfoWPF.Commands;
using CopyinfoWPF.Configuration;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Services.Interfaces;
using CopyinfoWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CopyinfoWPF.ViewModels
{
    public class DevicesViewModel : PageViewBase<DeviceRowView>
    {
        public DevicesViewModel() : this(UnityConfiguration.Resolve<IDeviceService>())
        {

        }

        public DevicesViewModel(IDeviceService deviceService) : base(deviceService)
        { 
            _deviceService = deviceService;
            RefreshCommand = new AsyncCommand(Refresh, CanRefresh);
            DataGridDoubleClickCommand = new BaseCommand(ShowDetails);
            FilterTextKeyUpCommand = new BaseCommand(ApplyFilters);
            var settings = ConfigureFilterSettings();
            FilterLogic = CompareFactory.BuildContainsFunc<DeviceRowView>();
        }

        private static Settings ConfigureFilterSettings()
        {
            var settings = new Settings()
            {
                StringCompareOptions = System.Globalization.CompareOptions.IgnoreCase,
                Deep = 1
            };

            settings.SetStandardWhiteList();
            return settings;
        }

        private void ShowDetails()
        {
            var clientOverviewViewModel = new ClientOverviewViewModel(); // Selected items is a HasSet. So FirstOrDefault will return "random".
            var deviceOverviewViewModel = UnityConfiguration.Resolve<DeviceOverviewViewModel>();
            var reportOverviewViewModel = UnityConfiguration.Resolve<ReportOverviewViewModel>();

            var device = _deviceService.GetDevice((SelectedItems.FirstOrDefault() as DeviceRowView)?.SerialNumber);
            clientOverviewViewModel.LoadClient(device.IdKlient);

            clientOverviewViewModel.DeviceSelected += deviceOverviewViewModel.OnDeviceSelected;
            deviceOverviewViewModel.RecordSelected += reportOverviewViewModel.OnRecordSelected;

            new OverviewView(clientOverviewViewModel, deviceOverviewViewModel, reportOverviewViewModel)
                .Show();
        }

        IDeviceService _deviceService;

        public override string ViewName => "Devices";

        private async Task Refresh()
        {
            _canRefresh = false;
            var records = await Task.Factory.StartNew(GetRecords);
            _sourceCollection.Clear();
            _sourceCollection.AddRange(records);
            Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            Collection.Filter = FilterCollection;
            _canRefresh = true;
        }

        private IEnumerable<DeviceRowView> GetRecords()
        {
            return _deviceService.GetAll();
        }
    }
}
