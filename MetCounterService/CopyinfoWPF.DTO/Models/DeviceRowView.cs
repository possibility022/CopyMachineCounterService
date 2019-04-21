using System;
using System.Reflection;

namespace CopyinfoWPF.DTO.Models
{
    public class DeviceRowView : BaseRow
    {

        private static readonly PropertyInfo[] PropertyInfos = typeof(DeviceRowView).GetProperties();

        public string SerialNumber { get; set; }

        public string ClientName { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Address { get; set; }

        public DateTime InstallationDateTime { get; set; }  

        public bool ServiceAgreement { get; set; }

        public override bool Filter(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
