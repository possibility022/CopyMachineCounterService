using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IMachineRecordService : IBaseService<MachineRecordRowView>
    {
        void RefreshCache();

        void RefreshViewModels(IEnumerable<MachineRecordRowView> records);

        void SetPrinted(IEnumerable<MachineRecordRowView> records);

        IEnumerable<DeviceViewModel> GetDevicesForClient(int clientId);
        IEnumerable<RecordViewModel> GetRecordsForDevice(string deviceSerialNumber);

        UrzadzenieKlient GetDeviceDetails(string serialNumber);
    }
}
