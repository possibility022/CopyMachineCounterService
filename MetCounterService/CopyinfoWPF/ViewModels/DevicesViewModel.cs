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
            RefreshCommand = new AsyncCommand(Refresh, CanExecute);
            DataGridDoubleClickCommand = new BaseCommand(ShowDetails);
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
        bool _canRefresh = true;
        
        public override string ViewName => "Devices";

        private async Task Refresh()
        {
            _canRefresh = false;
            var records = await Task.Factory.StartNew(GetRecords);
            _sourceCollection.Clear();
            _sourceCollection.AddRange(records);
            Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            Collection.Filter = FilterLogic;
            _canRefresh = true;
        }

        private bool FilterLogic(object o)
        {
            var row = (DeviceRowView)o;

            return row.Address.ToLower().Contains(FilterText) ||
                row.ClientName.ToLower().Contains(FilterText) ||
                row.InstallationDateTime.ToString().ToLower().Contains(FilterText) ||
                row.SerialNumber.ToLower().Contains(FilterText);
        }

        private bool CanExecute(object parameter)
        {
            return _canRefresh;
        }

        private IEnumerable<DeviceRowView> GetRecords()
        {
            return _deviceService.GetAll();
        }


    }
}
