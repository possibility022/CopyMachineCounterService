using System.Collections.Generic;

namespace CopyinfoWPF.Services.Interfaces
{
    public interface IBaseService<T>
    {
        ICollection<T> GetAll();
    }
}
