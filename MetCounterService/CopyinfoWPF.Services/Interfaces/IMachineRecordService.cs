using CopyinfoWPF.DTO.Models;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IMachineRecordService
    {

        IEnumerable<MachineRecordViewModel> GetLatestReports();

        void RefreshCache();

        void RefreshViewModels(IEnumerable<MachineRecordViewModel> records);

        void SetPrinted(IEnumerable<MachineRecordViewModel> records);
    }
}
