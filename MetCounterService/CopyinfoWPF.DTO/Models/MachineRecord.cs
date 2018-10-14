using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;

namespace CopyinfoWPF.DTO.Models
{
    public class MachineRecord
    {

        public MachineRecord(Record record, UrzadzenieKlient device)
        {
            Record = record;
            Device = device;
        }

        public Record Record { get; private set; } = new Record();

        public AdresKlient Address { get; private set; } = new AdresKlient();

        public UrzadzenieKlient Device { get; private set; } = new UrzadzenieKlient();
    }
}
//public string modelName { get; private set; }
//public string deviceAddress { get; private set; }
//public string clientName { get; private set; }