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
using System;

namespace CopyinfoWPF.Services.Implementation
{
    public class MachineRecordService : IMachineRecordService
    {
        private IGenericRepository<Record> _recordRepository;
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

            RefreshCache();
        }

        public IEnumerable<MachineRecordViewModel> GetLatestReports()
        {
            var records = new List<MachineRecordViewModel>();

            foreach (var rec in _recordRepository.All().OrderByDescending(d => d.ReadDatetime))
            {

                AdresKlient address = null;
                Klient client = null;
                UrzadzenieKlient device = null;
                
                device = _deviceCache.Get(rec.SerialNumber);

                if (device != null)
                {
                    address = _addressCache.Get(device.IdMiejsceInstalacji);
                    client = _clientCache.Get(device.IdKlient);
                }
                else
                    Debug.WriteLine("Empty device");


                records.Add(new MachineRecordViewModel(rec, device, address, client));
            }

            return records;
        }

        public void RefreshCache()
        {
            _deviceCache.UpdateMany(f => f.NrFabryczny, _deviceRepository.All(), k => !string.IsNullOrWhiteSpace(k));
            _addressCache.UpdateMany(f => f.IdAdresKlient, _addressRepository.All());
            _clientCache.UpdateMany(f => f.IdKlient, _clientRepository.All());
        }

        public void RefreshViewModels(IEnumerable<MachineRecordViewModel> records)
        {
            foreach (var rec in records)
            {
                rec.Record = _recordRepository.FindBy(rec.Record.Id);
                if (rec.Address != null)
                {
                    rec.Address = _addressRepository.FindBy(rec.Address.IdAdresKlient);
                    _addressCache.Add(rec.Address.IdAdresKlient, rec.Address);
                }
                if (rec.Client != null)
                {
                    rec.Client = _clientRepository.FindBy(rec.Client.IdKlient);
                    _clientCache.Add(rec.Client.IdKlient, rec.Client);
                }
                if (rec.Device != null)
                {
                    rec.Device = _deviceRepository.FindBy(rec.Device.IdUrzadzenieKlient);
                    _deviceCache.Add(rec.Device.NrFabryczny, rec.Device, k => !string.IsNullOrWhiteSpace(k));
                }
            }
        }

        public void SetPrinted(IEnumerable<MachineRecordViewModel> records)
        {
            foreach (var rec in records)
            {
                rec.Record.Printed = true;
                rec.Printed = true;

                _recordRepository.Update(rec.Record);
            }
        }
    }
}
