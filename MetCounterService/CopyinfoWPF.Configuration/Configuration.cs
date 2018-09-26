using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using Unity;

namespace CopyinfoWPF.Configuration
{
    public static class Configuration
    {
        public static IUnityContainer Container { get; private set; }

        public static void Initialize(IUnityContainer container = null)
        {
            if (container == null)
                Container = new UnityContainer();
            else
                Container = container;

            RegisterTypes();
            RegisterInstances();
            RegisterDatabaeses();
        }

        private static void RegisterTypes()
        {

        }

        private static void RegisterInstances()
        {
            Container.RegisterInstance<IDatabaseSessionProvider>(new DatabaseSessionProvider());
        }

        private static void RegisterDatabaeses()
        {
            var sessionProvider = Container.Resolve<IDatabaseSessionProvider>();
            sessionProvider.AddNewDatabaseSessionFactory(DatabaseType.CounterService, MetSessionFactorySettings.GetSessionFactory());
        }
    }
}
