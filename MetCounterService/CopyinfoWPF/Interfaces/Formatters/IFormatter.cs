using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Interfaces.Formatters
{
    interface IFormatter<T>
    {
        IEnumerable<string> GetText(IEnumerable<T> items);

        StringBuilder GetText(T item);
    }
}
