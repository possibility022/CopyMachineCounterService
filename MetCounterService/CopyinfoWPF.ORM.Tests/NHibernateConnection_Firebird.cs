using System.Collections.Generic;
using System.Linq;
using CopyinfoWPF.ORM.AsystentDatabase.Address;
using CopyinfoWPF.ORM.AsystentDatabase.Device;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    public class NHibernateConnection_Firebird
    {
        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = AsystentFactorySettings.GetSessionFactory();
        }

        [TestMethod]
        public void TestConnection()
        {
            bool isConnected = false;

            using (var session = SessionFactory.OpenSession())
            {
                isConnected = session.IsConnected;
            }

            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public void TestConnection_GetOneRecord()
        {
            var list = new List<object>();

            using (var session = SessionFactory.OpenSession())
            {
                list.Add(session.Query<ClientAddress>().FirstOrDefault());
                list.Add(session.Query<ClientDevice>().FirstOrDefault());
            }

            Assert.IsFalse(list.Any(o => o == null));
        }
    }
}
