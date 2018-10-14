using NHibernate;
using System.Collections.Generic;

namespace CopyinfoWPF.ORM
{
    public class DatabaseSessionProvider : IDatabaseSessionProvider
    {

        private static Dictionary<DatabaseType, ISessionFactory> _configurations = new Dictionary<DatabaseType, ISessionFactory>();

        public ISession OpenSession(DatabaseType databaseType)
        {
            return _configurations[databaseType].OpenSession();
        }

        public void AddNewDatabaseSessionFactory(DatabaseType databaseType, ISessionFactory sessionFactory)
        {
            _configurations.Add(databaseType, sessionFactory);
        }

        public ISessionFactory GetSessionFactory(DatabaseType databaseType)
        {
            return _configurations[databaseType];
        }
    }
}
