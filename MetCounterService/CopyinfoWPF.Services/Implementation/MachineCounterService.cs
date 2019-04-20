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
        private IGenericRepository<EmailSource> _emailSource;
        private IGenericRepository<ServiceSourceCounters> _serviceSourceCounters;

        public MachineCounterService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
            _emailSource = new GenericRepository<EmailSource>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
            _serviceSourceCounters = new GenericRepository<ServiceSourceCounters>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
        }

        public IList<Record> GetAllRecords()
        {
            return _recordRepository.All().ToList();
        }

        public EmailSource GetEmailSource(int emailSourceId)
        {
            return _emailSource.FindBy(emailSourceId);
        }

        public ServiceSourceCounters GetHtmlCounterSource(int emailSourceId)
        {
            return _serviceSourceCounters.FindBy(emailSourceId);
        }

        public void RefreshRecords(IEnumerable<Record> records)
        {
            foreach (var r in records)
            {
                _recordRepository.Load(r, r.Id);
            }
        }

        public void SetRecordToPrinted(IEnumerable<Record> record)
        {
            foreach (var r in record)
            {
                r.Printed = true;
                _recordRepository.Update(r);
            }
        }

        public IList<Record> TakeRecords(int count)
        {
            return _recordRepository.All().Take(count).ToList();
        }
    }
}
