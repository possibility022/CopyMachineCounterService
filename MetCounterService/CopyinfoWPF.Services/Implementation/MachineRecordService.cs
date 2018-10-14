using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CopyinfoWPF.Services.Implementation
{
    public class MachineRecordService : IMachineRecordService
    {
        private IGenericReadOnlyRepository<Record> _recordRepository;

        public MachineRecordService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
        }
        

        public IEnumerable<MachineRecord> GetLatestReports()
        {
            var records = new List<MachineRecord>();

            foreach (var rec in _recordRepository.All().OrderByDescending(d => d.ReadDatetime))
            {
                records.Add(new MachineRecord(rec));
            }

            return records;
        }
    }
}
