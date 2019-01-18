using CopyinfoWPF.Commands;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CopyinfoWPF.ViewModels
{
    public class DevicesViewModel : PageViewBase<DeviceRowView>
    {
        public DevicesViewModel() : base()
        {
            
        }

        public DevicesViewModel(IDeviceService deviceService) : base(deviceService)
        {
            _deviceService = deviceService;
            RefreshCommand = new AsyncCommand(Refresh, CanExecute);
        }

        IDeviceService _deviceService;
        bool _canRefresh = true;
        
        public override string ViewName => "Devices";

        ICommand _refreshCommand;
        public override ICommand RefreshCommand { protected set { _refreshCommand = value; } get { return _refreshCommand; } }

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

        private bool CanExecute()
        {
            return _canRefresh;
        }

        private IEnumerable<DeviceRowView> GetRecords()
        {
            return _deviceService.GetAll();
        }


    }
}
