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
        private IGenericRepository<Record> _recordRepository;

        private IGenericReadOnlyRepository<UrzadzenieKlient> _deviceRepository;
        private IGenericReadOnlyRepository<AdresKlient> _addressRepository;
        private IGenericReadOnlyRepository<Klient> _clientRepository;
        private IGenericReadOnlyRepository<UmowaSerwisowa> _serviceAgreementRepository;
        private IGenericReadOnlyRepository<ZlecenieSerwisowe> _serviceOrderRepository;
        private IGenericReadOnlyRepository<Pracownik> _employeeRepository;
        

        IConditionalCache<string, UrzadzenieKlient> _deviceCache;
        IConditionalCache<int, AdresKlient> _addressCache;
        IConditionalCache<int, Klient> _clientCache;
        IConditionalCache<int, Pracownik> _employeeCache;
        HashSet<int> _serviceAgreementCache;

        public MachineRecordService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _recordRepository = new GenericRepository<Record>(databaseSessionProvider.OpenSession(DatabaseType.CounterService));
            
            _deviceRepository = new GenericRepository<UrzadzenieKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _addressRepository = new GenericRepository<AdresKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _clientRepository = new GenericRepository<Klient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _serviceAgreementRepository = new GenericRepository<UmowaSerwisowa>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _serviceOrderRepository = new GenericRepository<ZlecenieSerwisowe>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
            _employeeRepository = new GenericRepository<Pracownik>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));

            _deviceCache = new Cache<string, UrzadzenieKlient>();
            _addressCache = new Cache<int, AdresKlient>();
            _clientCache = new Cache<int, Klient>();
            _employeeCache = new Cache<int, Pracownik>();
            _serviceAgreementCache = new HashSet<int>();

            RefreshCache();
        }

        public IEnumerable<DeviceViewModel> GetDevicesForClient(int clientId)
        {
            if (_clientCache.Contains(clientId))
            {
                AdresKlient address;
                string addressShortValue = string.Empty;

                foreach (var d in _deviceRepository.FilterBy(d => d.IdKlient == clientId))
                {
                    address = _addressCache.Get(d.IdMiejsceInstalacji);

                    if (address != null)
                    {
                        addressShortValue = $"{address.Ulica} {address.Miejscowosc}";
                    }
                    else
                    {
                        addressShortValue = string.Empty;
                    }
                    
                    _deviceCache.Add(d.NrFabryczny, d, o => o != null);

                    yield return new DeviceViewModel()
                    {
                        SerialNumber = d.NrFabryczny,
                        Address = addressShortValue,
                        InstallationDate = d.DataInstalacji,
                        Model = d.ModelUrzadzenia?.Nazwa1
                    };
                }
            }
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
                    client.UmowaSerwisowa = _serviceAgreementCache.Contains(client.IdKlient);
                }
                else
                    Debug.WriteLine("Empty device");


                records.Add(new MachineRecordViewModel(rec, device, address, client));
            }

            return records;
        }

        public IEnumerable<RecordViewModel> GetRecordsForDevice(string deviceSerialNumber)
        {

            var list = new List<RecordViewModel>();

            if (deviceSerialNumber != null && _deviceCache.Contains(deviceSerialNumber))
            {

                var device = _deviceCache.Get(deviceSerialNumber);
                
                foreach (var order in _serviceOrderRepository
                    .FilterBy(d => d.IdUrzadzenieKlient == device.IdUrzadzenieKlient))
                {
                    var model = new RecordViewModel(order.IdZlecenieSerwisowe, DatabaseType.Assystent)
                    {
                        BlackAndWhite = order.LicznikBiezacy ?? 0,
                        DateTime = order.DataZamknieciaZlec,
                        TextContent = order.OpisCzynnosciSerwisowych
                    };

                    if (order.IdSerwisant != null)
                        model.ServiceMan = _employeeCache.Get((int)order.IdSerwisant).Imie;

                    list.Add(model);
                }

                foreach (var rec in _recordRepository.FilterBy(d => d.SerialNumber == deviceSerialNumber))
                {
                    list.Add(new RecordViewModel(rec.Id, DatabaseType.CounterService)
                    {
                        BlackAndWhite = rec.CounterBlackAndWhite ?? 0,
                        Color = rec.CounterColor ?? 0,
                        Scan = rec.CounterScanner ?? 0,
                        DateTime = rec.ReadDatetime,
                        ServiceMan = "System",
                        BinaryContent = rec.EmailSource?.Content,
                        HtmlContent = rec.ServiceSourceCounters?.Content
                    });
                }
            }

            return list.OrderByDescending(d => d.DateTime);
        }

        public void RefreshCache()
        {
            _deviceCache.UpdateMany(f => f.NrFabryczny, _deviceRepository.All(), k => !string.IsNullOrWhiteSpace(k));
            _addressCache.UpdateMany(f => f.IdAdresKlient, _addressRepository.All());
            _clientCache.UpdateMany(f => f.IdKlient, _clientRepository.All());
            _employeeCache.UpdateMany(f => f.IdPracownik, _employeeRepository.All());
            _serviceAgreementCache = new HashSet<int>(_serviceAgreementRepository.All().Select(s => s.IdKlient));
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
                    rec.Client.UmowaSerwisowa = _serviceAgreementCache.Contains(rec.Client.IdKlient);
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
