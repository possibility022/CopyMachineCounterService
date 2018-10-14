using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.DTO.Models
{
    public class MachineRecord
    {
        public Record Record { get; private set; }

        public AdresKlient Address { get; private set; }

        public UrzadzenieKlient Device { get; private set; }
    }
}
//public string modelName { get; private set; }
//public string deviceAddress { get; private set; }
//public string clientName { get; private set; }