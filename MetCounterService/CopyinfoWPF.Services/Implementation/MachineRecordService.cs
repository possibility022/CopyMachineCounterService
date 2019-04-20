using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using CopyinfoWPF.Services.Extensions;

namespace CopyinfoWPF.Services.Implementation
{
    public class MachineRecordService : BaseService<MachineRecordRowView>, IMachineRecordService
    {
        public MachineRecordService(IDatabaseSessionProvider databaseSessionProvider) : base(databaseSessionProvider)
        {

        }

        public IEnumerable<DeviceViewModel> GetDevicesForClient(int clientId)
        {
            if (Cache.ClientCache.Contains(clientId))
            {
                AdresKlient address;
                string addressShortValue = string.Empty;

                foreach (var d in _deviceRepository.FilterBy(d => d.IdKlient == clientId))
                {
                    address = Cache.AddressCache.Get(d.IdMiejsceInstalacji);
                    addressShortValue = address.ToShortAddress();

                    Cache.DeviceCache.Add(d.NrFabryczny, d, o => o != null);

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

        public UrzadzenieKlient GetDeviceDetails(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber) == false)
            {
                var rec = _deviceRepository.FindBy(s => s.NrFabryczny == serialNumber);
                Cache.DeviceCache.Add(rec.NrFabryczny, rec);
                return rec;
            }
            return null;
        }

        public override ICollection<MachineRecordRowView> GetAll()
        {
            var records = new List<MachineRecordRowView>();

            foreach (var rec in _recordRepository.All().OrderByDescending(d => d.ReadDatetime))
            {

                AdresKlient address = null;
                Klient client = null;
                UrzadzenieKlient device = null;

                device = Cache.DeviceCache.Get(rec.SerialNumber);

                if (device != null)
                {
                    address = Cache.AddressCache.Get(device.IdMiejsceInstalacji);
                    client = Cache.ClientCache.Get(device.IdKlient);
                    client.UmowaSerwisowa = Cache.ServiceAgreementCache.Contains(client.IdKlient);
                }
                else
                    Debug.WriteLine("Empty device");


                records.Add(new MachineRecordRowView(rec, device, address, client));
            }

            return records;
        }

        public IEnumerable<RecordViewModel> GetRecordsForDevice(string deviceSerialNumber)
        {

            var list = new List<RecordViewModel>();

            if (deviceSerialNumber != null && Cache.DeviceCache.Contains(deviceSerialNumber))
            {

                var device = Cache.DeviceCache.Get(deviceSerialNumber);

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
                        model.ServiceMan = Cache.EmployeeCache.Get((int)order.IdSerwisant).Imie;

                    list.Add(model);
                }

                foreach (var rec in _recordRepository
                    .FilterBy(d => d.SerialNumber == deviceSerialNumber)
                    .Select(r => new RecordViewModel(r.Id, DatabaseType.CounterService)
                    {
                        BlackAndWhite = r.CounterBlackAndWhite ?? 0,
                        Color = r.CounterColor ?? 0,
                        Scan = r.CounterScanner ?? 0,
                        DateTime = r.ReadDatetime,
                        ServiceMan = "System",
                        HtmlSourceId = r.ServiceSourceCountersId,
                        EmailContentId = r.EmailSourceId
                    })
                    )
                {
                    list.Add(rec);
                }
            }

            return list.OrderByDescending(d => d.DateTime);
        }

        public void RefreshCache()
        {
            Cache.DeviceCache.UpdateMany(f => f.NrFabryczny, _deviceRepository.All(), k => !string.IsNullOrWhiteSpace(k));
            Cache.AddressCache.UpdateMany(f => f.IdAdresKlient, _addressRepository.All());
            Cache.ClientCache.UpdateMany(f => f.IdKlient, _clientRepository.All());
            Cache.EmployeeCache.UpdateMany(f => f.IdPracownik, _employeeRepository.All());
            Cache.ServiceAgreementCache = new HashSet<int>(_serviceAgreementRepository.All().Select(s => s.IdKlient));
        }

        public void RefreshViewModels(IEnumerable<MachineRecordRowView> records)
        {
            foreach (var rec in records)
            {
                rec.Record = _recordRepository.FindBy(rec.Record.Id);
                if (rec.Address != null)
                {
                    rec.Address = _addressRepository.FindBy(rec.Address.IdAdresKlient);
                    Cache.AddressCache.Add(rec.Address.IdAdresKlient, rec.Address);
                }
                if (rec.Client != null)
                {
                    rec.Client = _clientRepository.FindBy(rec.Client.IdKlient);
                    rec.Client.UmowaSerwisowa = Cache.ServiceAgreementCache.Contains(rec.Client.IdKlient);
                    Cache.ClientCache.Add(rec.Client.IdKlient, rec.Client);
                }
                if (rec.Device != null)
                {
                    rec.Device = _deviceRepository.FindBy(rec.Device.IdUrzadzenieKlient);
                    Cache.DeviceCache.Add(rec.Device.NrFabryczny, rec.Device, k => !string.IsNullOrWhiteSpace(k));
                }
            }
        }

        public void SetPrinted(IEnumerable<MachineRecordRowView> records)
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
