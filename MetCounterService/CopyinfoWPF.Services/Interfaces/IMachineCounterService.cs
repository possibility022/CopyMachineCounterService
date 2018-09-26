using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IMachineCounterService
    {
        IList<Record> GetAllRecords();

        IList<Record> TakeRecords(int count);
    }
}
