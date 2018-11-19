using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IMachineRecordService
    {

        IEnumerable<MachineRecordViewModel> GetLatestReports();

        void RefreshCache();

        void RefreshViewModels(IEnumerable<MachineRecordViewModel> records);

        void SetPrinted(IEnumerable<MachineRecordViewModel> records);

        IEnumerable<DeviceViewModel> GetDevicesForClient(int clientId);
        IEnumerable<RecordViewModel> GetRecordsForDevice(string deviceSerialNumber);

        UrzadzenieKlient GetDeviceDetails(string serialNumber);
    }
}
