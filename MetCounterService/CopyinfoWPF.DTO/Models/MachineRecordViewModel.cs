using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Prism.Mvvm;

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
        public string ClientName
        {
            get { return _clientName; }
            set { SetProperty(ref _clientName, value); }
        }

        public Record Record { get => _record; private set => SetProperty(ref _record, value); }

        public AdresKlient Address { get => _address; private set => SetProperty(ref _address, value); }

        public UrzadzenieKlient Device { get => _device; private set => SetProperty(ref _device, value); }

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