using System;
using System.Linq;
using System.Linq.Expressions;

namespace CopyinfoWPF.Repository
{
    public interface IGenericReadOnlyRepository<T> where T : class
    {
        T FindBy(int id);
        IQueryable<T> All();
        T FindBy(Expression<Func<T, bool>> expression);
        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}
