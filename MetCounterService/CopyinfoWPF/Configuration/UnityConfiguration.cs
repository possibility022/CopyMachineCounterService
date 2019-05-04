using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Formatters;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.ORM;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using CopyinfoWPF.Services.Implementation;
using CopyinfoWPF.Services.Interfaces;
using CopyinfoWPF.Workflows.Email;
using System;
using Unity;

namespace CopyinfoWPF.Configuration
{
    public static class UnityConfiguration
    {
        public static IUnityContainer Container { get; private set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Initialize()
        {
            if (Container != null)
                throw new InvalidOperationException("Unity was initialized twice.");

            Container = new UnityContainer();

            InitializeFormatters();
            RegisterTypes();
            RegisterInstances();
        }

        private static void RegisterTypes()
        {
            Container.RegisterType<IMachineCounterService, MachineCounterService>();
            Container.RegisterType<IMachineRecordService, MachineRecordService>();
            Container.RegisterType<IDeviceService, DeviceService>();
            Container.RegisterType<IClientService, ClientService>();
        }

        private static void RegisterInstances()
        {
            Container.RegisterInstance<IDatabaseSessionProvider>(new DatabaseSessionProvider());
        }

        public static void RegisterDatabaeses(string asystentConnectionString, string copyinfoDatabase)
        {
            var sessionProvider = Container.Resolve<IDatabaseSessionProvider>();
            sessionProvider.AddNewDatabaseSessionFactory(DatabaseType.CounterService, MetSessionFactorySettings.GetNewSessionFactory(copyinfoDatabase));
            sessionProvider.AddNewDatabaseSessionFactory(DatabaseType.Assystent, AsystentFactorySettings.GetNewSessionFactory(asystentConnectionString));
        }

        private static void InitializeFormatters()
        {
            Container.RegisterType<IFormatter<MachineRecordRowView>, RecordFormatter>();
            Container.RegisterType<IFormatter<EmailMessage>, RecordFormatter>();
            Container.RegisterType<IFormatter<RecordViewModel>, RecordFormatter>();
        }
    }
}
