using AutoMapper;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Helpers;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Attributes;

namespace CopyinfoWPF.ViewModels
{
    public class DeviceOverviewViewModel : BindableBase
    {

        public DeviceOverviewViewModel() { }

        [InjectionConstructor]
        public DeviceOverviewViewModel(IMachineRecordService service, IDeviceService deviceService)
        {
            _machineRecordService = service;
            _deviceService = deviceService;
        }

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

        public EventHandler<RecordViewModel> RecordSelected;

        private RecordViewModel _selectedRecord;
        public RecordViewModel SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                if (value != _selectedRecord)
                {
                    SetProperty(ref _selectedRecord, value);
                    RecordSelected?.Invoke(this, value);
                }
            }
        }

        private int _recordToSelect = -1;

        private readonly IMachineRecordService _machineRecordService;
        private readonly IDeviceService _deviceService;

        private void UpdateDevice(UrzadzenieKlient device)
        {
            if (device != null)
            {
                Mapper.Map(device, this);
                var address = _deviceService.GetDeviceAddress(device.IdMiejsceInstalacji);
                Mapper.Map(address, this);
            }
        }

        internal void RecordDataGridLoaded()
        {
            LoadRecordsToList(_machineRecordService.GetRecordsForDevice(SerialNumber));
        }

        public void LoadRecordsToList(IEnumerable<RecordViewModel> records)
        {
            SelectedRecord = null;
            Records.Clear();
            Records.AddRange(records);

            if (_recordToSelect >= 0)
            {
                SelectedRecord = Records.First(r => r.Id == _recordToSelect);
                _recordToSelect = -1;
            }
            else
            {
                SelectedRecord = Records.FirstOrDefault();
            }
        }

        public void OnDeviceSelected(object sender, DeviceViewModel e)
        {
            LoadRecordsToList(_machineRecordService.GetRecordsForDevice(e.SerialNumber));
            UpdateDevice(_machineRecordService.GetDeviceDetails(e.SerialNumber));
        }

        public void SetRecordToSelect(int? id) => _recordToSelect = id ?? -1;
    }
}
