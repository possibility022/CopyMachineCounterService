using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IMachineCounterService
    {
        IList<Record> GetAllRecords();

        IList<Record> TakeRecords(int count);

        void SetRecordToPrinted(IEnumerable<Record> record);

        void RefreshRecords(IEnumerable<Record> records);

        EmailSource GetEmailSource(int emailSourceId);
        ServiceSourceCounters GetHtmlCounterSource(int emailSourceId);
    }
}
