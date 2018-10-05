using CopyinfoWPF.ORM.AsystentDatabase.Address;
using CopyinfoWPF.ORM.AsystentDatabase.Address.Maps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System.Collections.Generic;

namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings
{
    public static class AsystentFactorySettings
    {

        private static Dictionary<DatabaseType, Configuration> _configurations = new Dictionary<DatabaseType, Configuration>();

        private static HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();

            mapper.AddMapping<ClientAddressMap>();
            
            var mapping = mapper.CompileMappingFor(
                new[]
                {
                    typeof(ClientAddress)
                });

            return mapping;
        }

        public static ISessionFactory GetSessionFactory()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "***REMOVED***Database=D:\\data\\test.fdb;DataSource=WIN-RP56U0UJDMQ; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";
                x.Dialect<global::NHibernate.Dialect.FirebirdDialect>();
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
