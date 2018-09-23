using System;
using System.Linq;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;

namespace CopyinfoWPF.Repository.Tests
{
    [TestClass]
    public class GenericRepositoryTest
    {
        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=WIN-RP56U0UJDMQ;Initial Catalog=MetCounterService;User Id=Superuser;Password=1234567890";
                x.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
            }
            );

            cfg.AddDeserializedMapping(ConfigurationSettings.GetMapping(), null);
            SessionFactory = cfg.BuildSessionFactory();
        }

        [TestMethod]
        public void AllTakeTen()
        {
            var repository = new GenericRepository<EmailSource>(SessionFactory.OpenSession());
            var taken = repository.All().Take(10).ToList();
            Assert.IsTrue(taken.Count > 0);
        }
    }
}
