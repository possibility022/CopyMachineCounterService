using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.ViewModels
{
    public class DeviceOverviewViewModel : BindableBase
    {
        private string _manufacturer;
        public string Manufacturer
        {
            get { return _manufacturer; }
            set { SetProperty(ref _manufacturer, value); }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        private string _serialNumber = string.Empty;

        public string SerialNumber
        {
            get { return _serialNumber; }
            set { SetProperty(ref _serialNumber, value); }
        }

        private DateTime _installationDate;
        public DateTime InstallationDate
        {
            get { return _installationDate; }
            set { SetProperty(ref _installationDate, value); }
        }

        #region Address

        private string _street;
        public string Street
        {
            get { return _street; }
            set { SetProperty(ref _street, value); }
        }

        private string _houseNumber;
        public string HouseNumber
        {
            get { return _houseNumber; }
            set { SetProperty(ref _houseNumber, value); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }
        }

        private string _postalCity;
        public string PostalCity
        {
            get { return _postalCity; }
            set { SetProperty(ref _postalCity, value); }
        }

        #endregion

        private ObservableCollection<RecordViewModel> _records = new ObservableCollection<RecordViewModel>();
        public ObservableCollection<RecordViewModel> Records
        {
            get { return _records; }
            set { SetProperty(ref _records, value); }
        }

        private readonly IMachineRecordService _machineRecordService;

        public DeviceOverviewViewModel() { }

        public DeviceOverviewViewModel(IMachineRecordService service, UrzadzenieKlient device)
        {
            _machineRecordService = service;
            if (device != null)
            {
                Manufacturer = device.ModelUrzadzenia?.MarkaUrzadzenia?.Nazwa1;
                SerialNumber = device.NrFabryczny;
                InstallationDate = device.DataInstalacji;
                Model = device.ModelUrzadzenia?.Nazwa1;                
            }
        }

        internal void RecordDataGridLoaded()
        {
            Records.AddRange(_machineRecordService.GetRecordsForDevice(SerialNumber));
        }

    }
}
