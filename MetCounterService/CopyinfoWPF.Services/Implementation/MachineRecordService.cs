using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CopyinfoWPF.Common.CustomCollections;

namespace CopyinfoWPF.Services.Implementation
{
    public class MachineRecordService : IMachineRecordService
    {
        private IGenericReadOnlyRepository<Record> _recordRepository;
        private IGenericReadOnlyRepository<UrzadzenieKlient> _deviceRepository;

        IConditionalCache<string, UrzadzenieKlient> _deviceCache;
        
        public MachineRecordService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
            _deviceRepository = new GenericRepository<UrzadzenieKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));

            _deviceCache = new Cache<string, UrzadzenieKlient>();

            _deviceCache.UpdateMany(f => f.NrFabryczny, _deviceRepository.All(), k => !string.IsNullOrWhiteSpace(k));
        }

        public IEnumerable<MachineRecord> GetLatestReports()
        {
            var records = new List<MachineRecord>();

            foreach (var rec in _recordRepository.All().OrderByDescending(d => d.ReadDatetime))
            {
                records.Add(new MachineRecord(rec, _deviceCache.Get(rec.SerialNumber)));
            }

            return records;
        }
    }
}
