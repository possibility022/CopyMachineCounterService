using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;

namespace CopyinfoWPF.DTO.Models
{
    public class MachineRecord
    {

        public MachineRecord(Record record, UrzadzenieKlient device, AdresKlient address, Klient client)
        {
            Record = record;
            Device = device;
            Address = address;
            Client = client;
        }

        public Record Record { get; private set; } = new Record();

        public AdresKlient Address { get; private set; } = new AdresKlient();

        public UrzadzenieKlient Device { get; private set; } = new UrzadzenieKlient();

        public Klient Client { get; set; } = new Klient();
    }
}
//public string modelName { get; private set; }
//public string deviceAddress { get; private set; }
//public string clientName { get; private set; }