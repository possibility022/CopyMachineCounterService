using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System.Collections.Generic;

namespace CopyinfoWPF.Interfaces
{
    interface IPrintContentProvider
    {
        IEnumerable<string> GetTextToPrint(IEnumerable<Record> records);
    }
}
