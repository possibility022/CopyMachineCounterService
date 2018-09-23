using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private ISession _session;

        public GenericRepository(ISession session)
        {
            _session = session;
        }

        public bool Add(T entity)
        {
            _session.Save(entity);
            _session.Flush();
            return true;
        }

        public bool Add(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public T FindBy(int id)
        {
            throw new NotImplementedException();
        }

        public T FindBy<TV>(TV id)
        {
            throw new NotImplementedException();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
