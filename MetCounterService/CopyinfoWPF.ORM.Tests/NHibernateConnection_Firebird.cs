using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
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
                list.Add(session.Query<AdresKlient>().FirstOrDefault());
                list.Add(session.Query<UrzadzenieKlient>().FirstOrDefault());
            }

            Assert.IsFalse(list.Any(o => o == null));
                
        }

        [TestMethod]
        public void BrowseRecordsClientDevices()
        {
            List<ClientDevice> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<ClientDevice>().Take(100).ToList();
            }

            Trace.Write(list.Count);
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsClientDevices_CheckMappingDeviceModel()
        {
            List<ClientDevice> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<ClientDevice>().Take(100).ToList();
            }

            Assert.IsTrue(list.All(a => a.DeviceModel != null));
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsDeviceModel_DeviceBrand()
        {
            List<DeviceModel> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<DeviceModel>().Take(100).ToList();
            }

            Assert.IsTrue(list.All(a => a.DeviceBrand != null));
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsDeviceModel()
        {
            List<DeviceModel> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<DeviceModel>().Take(100).ToList();
            }

            Trace.Write(list.Count);
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsDeviceBrand()
        {
            List<DeviceBrand> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<DeviceBrand>().Take(100).ToList();
            }

            Trace.Write(list.Count);
        }


    }
}
