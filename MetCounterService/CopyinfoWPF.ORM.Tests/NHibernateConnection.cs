﻿using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    [TestCategory("DatabaseConnection")]
    public class NHibernateConnection
    {

        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = MetSessionFactorySettings.GetNewSessionFactory("Server=WIN-RP56U0UJDMQ;Initial Catalog=MetCounterService;User Id=Superuser;Password=1234567890");
        }


        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void TestConnection()
        {
            IEnumerable<EmailSource> list;

            bool isConnected = false;

            using (var session = SessionFactory.OpenSession())
            {
                isConnected = session.IsConnected;
            }

            Assert.IsTrue(isConnected);
        }

        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void TestQueryEmailSource()
        {
            var s = GetFirstQuery<EmailSource>();
            Trace.WriteLine(s);
        }

        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void TestQueryRecord()
        {
            var s = GetFirstQuery<Record>();
            Trace.WriteLine(s);
        }

        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void TestQueryServiceSourceCounters()
        {
            var s = GetFirstQuery<ServiceSourceCounters>();
            Trace.WriteLine(s);
        }

        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void TestQueryServiceSourceSerialNumber()
        {
            var s = GetFirstQuery<ServiceSourceSerialNumber>();
            Trace.WriteLine(s);
        }



        private T GetFirstQuery<T>()
        {
            using (var session = SessionFactory.OpenSession())

            {
                return session.Query<T>().FirstOrDefault();
            }
        }
    }
}
