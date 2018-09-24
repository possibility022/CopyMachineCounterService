using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using NHibernate;
using NHibernate.Cfg;

namespace CopyinfoWPF.ORM.Tests
{
    class Factory
    {
        
        public static ISessionFactory GetSessionFactory()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=WIN-RP56U0UJDMQ;Initial Catalog=MetCounterService;User Id=Superuser;Password=1234567890";
                x.Dialect<global::NHibernate.Dialect.MsSql2012Dialect>();
            }
            );

            cfg.AddDeserializedMapping(ConfigurationSettings.GetMapping(), null);
            return cfg.BuildSessionFactory();
        }
    }
}
