using System;
using System.Text;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    [TestCategory("DatabaseConnection")]
    public class TestDeleteOrphans
    {
        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = MetSessionFactorySettings.GetNewSessionFactory("Server=WIN-RP56U0UJDMQ;Initial Catalog=MetCounterService;User Id=Superuser;Password=1234567890");
        }

        [TestCategory("DatabaseConnection")]
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
                    ReadDatetime = DateTime.Now,
                    SerialNumber = SerialNumber
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
