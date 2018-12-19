using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.Repository;
using CopyinfoWPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Services.Implementation
{
    public class DeviceService : IDeviceService
    {
        private GenericRepository<UrzadzenieKlient> _deviceRepository;

        public DeviceService(IDatabaseSessionProvider databaseSessionProvider)
        {
            _deviceRepository = new GenericRepository<UrzadzenieKlient>(databaseSessionProvider.OpenSession(DatabaseType.Assystent));
        }


        public ICollection<DeviceRowView> GetAll()
        {
            return _deviceRepository
                .All()
                .Select(r => new DeviceRowView
                {
                    InstallationDateTime = r.DataInstalacji,
                    Address = "TEST"
                }).ToList();
        }
    }
}
