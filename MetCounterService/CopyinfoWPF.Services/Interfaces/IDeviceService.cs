using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IDeviceService : IBaseService<DeviceRowView>
    {
        AdresKlient GetDeviceAddress(int idInstalationAddress);

        UrzadzenieKlient GetDevice(string serialNumber);
    }
}
