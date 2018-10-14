using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CopyinfoWPF.Common.CustomCollections;
using System.Diagnostics;

namespace CopyinfoWPF.Services.Implementation
{
    public class MachineRecordService : IMachineRecordService
    {
        private IGenericReadOnlyRepository<Record> _recordRepository;
        private IGenericReadOnlyRepository<UrzadzenieKlient> _deviceRepository;
        private IGenericReadOnlyRepository<AdresKlient> _addressRepository;
        private IGenericReadOnlyRepository<Klient> _clientRepository;

        IConditionalCache<string, UrzadzenieKlient> _deviceCache;
        IConditionalCache<int, AdresKlient> _addressCache;
        IConditionalCache<int, Klient> _clientCache;

        public MachineRecordService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
            _deviceRepository = new GenericRepository<UrzadzenieKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _addressRepository = new GenericRepository<AdresKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _clientRepository = new GenericRepository<Klient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));

            _deviceCache = new Cache<string, UrzadzenieKlient>();
            _addressCache = new Cache<int, AdresKlient>();
            _clientCache = new Cache<int, Klient>();

            _deviceCache.UpdateMany(f => f.NrFabryczny, _deviceRepository.All(), k => !string.IsNullOrWhiteSpace(k));
            _addressCache.UpdateMany(f => f.IdAdresKlient, _addressRepository.All());
            _clientCache.UpdateMany(f => f.IdKlient, _clientRepository.All());
        }

        public IEnumerable<MachineRecord> GetLatestReports()
        {
            var records = new List<MachineRecord>();

            foreach (var rec in _recordRepository.All().OrderByDescending(d => d.ReadDatetime))
            {
                var device = _deviceCache.Get(rec.SerialNumber);
                AdresKlient address = null;
                Klient client = null;

                if (device != null)
                {
                    address = _addressCache.Get(device.IdMiejsceInstalacji);
                    client = _clientCache.Get(device.IdKlient);
                }
                else
                    Debug.WriteLine("Empty device");


                records.Add(new MachineRecord(rec, device, address, client));
            }

            return records;
        }
    }
}
