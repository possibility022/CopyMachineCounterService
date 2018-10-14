using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;

namespace CopyinfoWPF.DTO.Models
{
    public class MachineRecord
    {

        public MachineRecord(Record record)
        {
            Record = record;
        }

        public MachineRecord()
        {
            Record = new Record();
            Address = new AdresKlient();
            Device = new UrzadzenieKlient();
        }

        public Record Record { get; private set; }

        public AdresKlient Address { get; private set; }

        public UrzadzenieKlient Device { get; private set; }
    }
}
//public string modelName { get; private set; }
//public string deviceAddress { get; private set; }
//public string clientName { get; private set; }