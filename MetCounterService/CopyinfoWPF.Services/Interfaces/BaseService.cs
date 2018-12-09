using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface BaseService<T>
    {
        ICollection<T> GetAll();
    }
}
