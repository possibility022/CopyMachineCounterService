using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    public class NHibernateConnection
    {
        [TestMethod]
        public void TestConnection()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=WIN-RP56U0UJDMQ;Initial Catalog=MetCounterService;User Id=Superuser;Password=1234567890";
                x.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
            }
            );

            cfg.AddDeserializedMapping(ConfigurationSettings.GetMapping(), null);

            var sessionFactory = cfg.BuildSessionFactory();

            IEnumerable<Emailsource> list;

            using (var session = sessionFactory.OpenSession())
            {
                list = session.Query<Emailsource>().Take(10).ToList();
            }

            foreach (var el in list)
                Console.WriteLine(el.Id);

        }
    }
}
