using CopyinfoWPF.DTO.Models;
using System.Collections.Generic;
using System.Text;

namespace CopyinfoWPF.Interfaces.Formatters
{
    interface IRecordToTextFormatter
    {
        IEnumerable<string> GetText(IEnumerable<MachineRecordViewModel> records);

        StringBuilder GetText(MachineRecordViewModel record);
    }
}
