using NHibernate;

namespace CopyinfoWPF.ORM
{
    public interface IDatabaseSessionProvider
    {
        ISession OpenSession(DatabaseType databaseType);
        ISessionFactory GetSessionFactory(DatabaseType databaseType);
        void AddNewDatabaseSessionFactory(DatabaseType databaseType, ISessionFactory sessionFactory);
    }
}
