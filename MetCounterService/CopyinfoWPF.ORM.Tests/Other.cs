using System;
using System.IO;
using System.Linq;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    public class Other
    {

        private static ISessionFactory SessionFactory { get; set; }
        private static ISessionFactory AsystentSessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = MetSessionFactorySettings.GetNewSessionFactory("Server=WIN-RP56U0UJDMQ;Initial Catalog=MetCounterService;User Id=Superuser;Password=1234567890");
            AsystentSessionFactory = AsystentFactorySettings.GetNewSessionFactory("User=SYSDBA;Password=masterkey;Database=D:\\data\\test.fdb;DataSource=WIN-RP56U0UJDMQ; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;");
        }

        [TestMethod]
        [Ignore]
        public void LoadRecordsToFiles()
        {
            using (var session = SessionFactory.OpenSession())
            {
                var records = session.Query<Record>().Where(r => r.EmailSource != null).Take(10);

                foreach(var r in records)
                {
                    File.WriteAllBytes($"Record{r.Id}.bytes", r.EmailSource.Content);
                }
            }
        }

        [TestMethod]
        [Ignore]
        public void LoadClientRecords()
        {
            using (var session = AsystentSessionFactory.OpenSession())
            {
                var clients = session.Query<Klient>().Take(20);

                foreach(var c in clients)
                {
                    Console.WriteLine(c);
                }
            }
        }
    }
}
