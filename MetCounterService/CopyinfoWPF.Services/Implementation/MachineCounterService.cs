using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CopyinfoWPF.Services.Implementation
{
    public class MachineCounterService : IMachineCounterService
    {
        private IGenericRepository<Record> _recordRepository;

        public MachineCounterService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
        }

        public IList<Record> GetAllRecords()
        {
            return _recordRepository.All().ToList();
        }

        public IList<Record> TakeRecords(int count)
        {
            return _recordRepository.All().Take(count).ToList();
        }
    }
}
