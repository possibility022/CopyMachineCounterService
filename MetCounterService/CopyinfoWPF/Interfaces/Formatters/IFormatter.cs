using System.Collections.Generic;
using System.Text;

namespace CopyinfoWPF.Interfaces.Formatters
{
    public interface IFormatter<T>
    {
        IEnumerable<string> GetText(IEnumerable<T> items);

        StringBuilder GetText(T item);
    }
}
