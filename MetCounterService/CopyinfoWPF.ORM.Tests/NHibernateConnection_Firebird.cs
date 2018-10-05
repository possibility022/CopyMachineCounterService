using System.Linq;
using CopyinfoWPF.ORM.AsystentDatabase.Address;
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
            bool isConnected = false;
            ClientAddress record = null;

            using (var session = SessionFactory.OpenSession())
            {
                isConnected = session.IsConnected;
                record = session.Query<ClientAddress>().FirstOrDefault();
            }

            Assert.IsNotNull(record);
        }
    }
}
