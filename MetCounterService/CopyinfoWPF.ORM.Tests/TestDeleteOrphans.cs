using System;
using System.Text;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    public class TestDeleteOrphans
    {
        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = MetSessionFactorySettings.GetSessionFactory();
        }

        [TestMethod]
        public void DeleteRelatedRecords()
        {
            // Arrange
            const string SerialNumber = "21938726391232312342";

            Record record;

            using (var session = SessionFactory.OpenSession())
            {
                record = new Record()
                {
                    Readdatetime = DateTime.Now,
                    Serialnumber = SerialNumber
                };

                session.Save(record);

                var emailSource = new EmailSource()
                {
                    Content = Encoding.UTF8.GetBytes("Content"),
                    Id = record.Id
                };

                var htmlCounterSource = new ServiceSourceCounters()
                {
                    Content = "Content",
                    Id = record.Id
                };

                var htmlNumberSource = new ServiceSourceSerialNumber()
                {
                    Content = "Content",
                    Id = record.Id
                };

                session.Save(htmlCounterSource);
                session.Save(htmlNumberSource);

                session.Flush();
            }

            // Act
            using (var session = SessionFactory.OpenSession())
            {
                session.Delete(record);
                session.Flush();
            }

            // Assert
            using (var session = SessionFactory.OpenSession())
            {
                var rec = session.Get<Record>(record.Id);
                Assert.IsNull(rec);
            }
        }
    }
}
