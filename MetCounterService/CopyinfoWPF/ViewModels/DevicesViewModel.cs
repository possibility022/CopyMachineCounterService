using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.ViewModels
{
    public class DevicesViewModel : PageViewBase<DeviceRowView>
    {

        IDeviceService _deviceService;

        public DevicesViewModel() : base()
        {

        }

        public DevicesViewModel(IDeviceService deviceService) : base(deviceService)
        {
            _deviceService = deviceService;
        }

        public override string ViewName => "Devices";
    }
}
