using CopyinfoWPF.Common.CustomCollections;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Implementation
{
    public abstract class BaseService<T> : IBaseService<T>
    {
        protected IGenericRepository<Record> _recordRepository;

        protected IGenericReadOnlyRepository<UrzadzenieKlient> _deviceRepository;
        protected IGenericReadOnlyRepository<AdresKlient> _addressRepository;
        protected IGenericReadOnlyRepository<Klient> _clientRepository;
        protected IGenericReadOnlyRepository<UmowaSerwisowa> _serviceAgreementRepository;
        protected IGenericReadOnlyRepository<ZlecenieSerwisowe> _serviceOrderRepository;
        protected IGenericReadOnlyRepository<Pracownik> _employeeRepository;
        
        protected BaseService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
            _deviceRepository = new GenericRepository<UrzadzenieKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _addressRepository = new GenericRepository<AdresKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _clientRepository = new GenericRepository<Klient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _serviceAgreementRepository = new GenericRepository<UmowaSerwisowa>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _serviceOrderRepository = new GenericRepository<ZlecenieSerwisowe>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _employeeRepository = new GenericRepository<Pracownik>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
        }
        
        abstract public ICollection<T> GetAll();
    }
}
