using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CopyinfoWPF.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private ISession _session;

        public GenericRepository(ISession session)
        {
            _session = session;
            _session.FlushMode = FlushMode.Manual;
        }

        public bool Add(T entity)
        {
            _session.Save(entity);
            _session.Flush();
            return true;
        }

        public bool Add(IEnumerable<T> items)
        {
            foreach (var el in items)
                _session.Save(el);

            _session.Flush();
            return true;
        }

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public bool Delete(T entity)
        {
            _session.Delete(entity);
            _session.Flush();
            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            foreach (var el in entities)
                _session.Delete(el);

            _session.Flush();
            return true;
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return All().Where(expression);
        }

        public T FindBy(int id)
        {
            return _session.Get<T>(id);
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).FirstOrDefault();
        }

        public void Load(T record, object id)
        {
            _session.Load(record, id);
        }

        public bool Update(T entity)
        {
            _session.Update(entity);
            _session.Flush();
            return true;
        }
    }
}
