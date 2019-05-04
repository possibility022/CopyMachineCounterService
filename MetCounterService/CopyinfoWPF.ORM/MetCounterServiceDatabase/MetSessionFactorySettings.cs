using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine.Maps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System.Collections.Generic;

namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings
{
    public class MetSessionFactorySettings
    {

        public MetSessionFactorySettings(string connectionString)
        {
            SessionFactory = GetNewSessionFactory(connectionString);
        }

        private Dictionary<DatabaseType, Configuration> _configurations = new Dictionary<DatabaseType, Configuration>();

        public ISessionFactory SessionFactory { get; private set; }

        private static HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();

            mapper.AddMapping<EmailsourceMap>();
            mapper.AddMapping<RecordMap>();
            mapper.AddMapping<ServiceSourceSerialNumberMap>();
            mapper.AddMapping<ServiceSourceCountersMap>();
            
            var mapping = mapper.CompileMappingFor(
                new[]
                {
                    typeof(EmailSource),
                    typeof(Record),
                    typeof(ServiceSourceSerialNumber),
                    typeof(ServiceSourceCounters)
                });

            return mapping;
        }

        public static ISessionFactory GetNewSessionFactory(string connectionString)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = connectionString;
                x.Dialect<global::NHibernate.Dialect.MsSql2012Dialect>();
            }
            );

            cfg.AddDeserializedMapping(GetMapping(), null);
            return cfg.BuildSessionFactory();
        }

        public static void RegisterNewDatabase(DatabaseType databaseType)
        {
            
        }

    }
}
