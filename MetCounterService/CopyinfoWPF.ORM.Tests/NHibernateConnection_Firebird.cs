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
        public void BrowseRecordsUrzadzenieKlients()
        {
            List<UrzadzenieKlient> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<UrzadzenieKlient>().Take(100).ToList();
            }

            Trace.Write(list.Count);
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsUrzadzenieKlients_CheckMappingModelUrzadzenia()
        {
            List<UrzadzenieKlient> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<UrzadzenieKlient>().Take(100).ToList();
            }

            Assert.IsTrue(list.All(a => a.ModelUrzadzenia != null));
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsModelUrzadzenia_MarkaUrzadzenia()
        {
            List<ModelUrzadzenia> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<ModelUrzadzenia>().Take(100).ToList();
            }

            Assert.IsTrue(list.All(a => a.MarkaUrzadzenia != null));
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsModelUrzadzenia()
        {
            List<ModelUrzadzenia> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<ModelUrzadzenia>().Take(100).ToList();
            }

            Trace.Write(list.Count);
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsMarkaUrzadzenia()
        {
            List<MarkaUrzadzenia> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<MarkaUrzadzenia>().Take(100).ToList();
            }

            Trace.Write(list.Count);
        }


    }
}
