using Prism.Mvvm;
using System;

namespace CopyinfoWPF.DTO.Models
{
    public class DeviceViewModel : BindableBase
    {
        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { SetProperty(ref _serialNumber, value); }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private DateTime _installationDate;
        public DateTime InstallationDate
        {
            get { return _installationDate; }
            set { SetProperty(ref _installationDate, value); }
        }
    }
}
