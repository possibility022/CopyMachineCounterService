using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Prism.Mvvm;
using System.Diagnostics;

namespace CopyinfoWPF.DTO.Models
{
    public class MachineRecordViewModel : BindableBase
    {
        private Klient _client;
        private UrzadzenieKlient _device;
        private AdresKlient _address;
        private Record _record;

        public MachineRecordViewModel(Record record, UrzadzenieKlient device, AdresKlient address, Klient client)
        {
            Record = record;
            Device = device;
            Address = address;
            Client = client;
        }

        private string _clientName;
        private bool _printed;
        private bool _emailSourceAvailable;

        public string ClientName
        {
            get { return _clientName; }
            set { SetProperty(ref _clientName, value); }
        }

        public Record Record
        {
            get => _record;
            set
            {
                SetProperty(ref _record, value);
                Printed = value?.Printed ?? false;
                EmailSourceAvailable = value?.EmailSourceId == null ? false : true;
            }
        }

        public AdresKlient Address { get => _address; set => SetProperty(ref _address, value); }

        public UrzadzenieKlient Device { get => _device; set => SetProperty(ref _device, value); }

        public bool Printed { get => _printed; set => SetProperty(ref _printed, value); }

        public bool EmailSourceAvailable { get => _emailSourceAvailable; set => SetProperty(ref _emailSourceAvailable, value); }

        public Klient Client
        {
            get => _client; set
            {
                SetProperty(ref _client, value);
                if (value != null)
                {
                    ClientName = value.NazwaSkr + value.Nazwa2;
                }
            }
        }
    }
}
//public string modelName { get; private set; }
//public string deviceAddress { get; private set; }
//public string clientName { get; private set; }