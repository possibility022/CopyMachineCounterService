using System.Collections.Generic;

namespace CopyinfoWPF.Repository
{
    public interface IGenericRepository<T> : IGenericReadOnlyRepository<T> where T : class
    {
        bool Add(T entity);
        bool Add(IEnumerable<T> items);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(IEnumerable<T> entities);
    }
}
