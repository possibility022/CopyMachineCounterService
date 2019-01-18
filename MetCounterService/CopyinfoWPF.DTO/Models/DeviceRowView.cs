using System;

namespace CopyinfoWPF.DTO.Models
{
    public class DeviceRowView : BaseRow
    {
        public string SerialNumber { get; set; }

        public string ClientName { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Address { get; set; }

        public DateTime InstallationDateTime { get; set; }  

        public bool ServiceAgreement { get; set; }
    }
}
