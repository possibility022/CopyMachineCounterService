using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Extensions;
using CopyinfoWPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Services.Implementation
{
    public class DeviceService : BaseService<DeviceRowView>, IDeviceService
    {
        public DeviceService(IDatabaseSessionProvider databaseSessionProvider) : base(databaseSessionProvider)
        { }


        public override ICollection<DeviceRowView> GetAll()
        {
            return _deviceRepository
                .All()
                .Select(r => new DeviceRowView
                {
                    InstallationDateTime = r.DataInstalacji,
                    Address = Cache.AddressCache.Get(r.IdMiejsceInstalacji).ToShortAddress(),
                    ClientName = Cache.ClientCache.Get(r.IdKlient).ToShortName(),
                    Manufacturer = r.ModelUrzadzenia.MarkaUrzadzenia.Nazwa1,
                    Model = r.ModelUrzadzenia.Nazwa1,
                    SerialNumber = r.NrFabryczny,
                    ServiceAgreement = Cache.ServiceAgreementCache.Contains(r.IdKlient)
                }).ToList();
        }

        public AdresKlient GetDeviceAddress(int idInstalationAddress)
        {
            return Cache.AddressCache.Get(idInstalationAddress);
        }
    }
}
