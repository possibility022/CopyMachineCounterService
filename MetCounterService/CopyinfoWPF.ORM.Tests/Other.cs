using System;
using System.IO;
using System.Linq;
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

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = MetSessionFactorySettings.GetSessionFactory();
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
    }
}
