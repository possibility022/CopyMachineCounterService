using System.Linq;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace CopyinfoWPF.Repository.Tests
{
    [TestClass]
    [TestCategory("DatabaseConnection")]
    public class GenericRepositoryTest
    {
        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = MetSessionFactorySettings.GetSessionFactory();
        }

        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void AllTakeTen()
        {
            var repository = new GenericRepository<EmailSource>(SessionFactory.OpenSession());
            var taken = repository.All().Take(10).ToList();
            Assert.IsTrue(taken.Count > 0);
        }

        [TestCategory("DatabaseConnection")]
        [TestMethod]
        public void TestFewOperationsInARowWithSameSession()
        {
            var repository = new GenericRepository<ServiceSourceCounters>(SessionFactory.OpenSession());

            var record = new ServiceSourceCounters()
            {
                Content = "ABC"
            };

            repository.Add(record);
            Assert.IsTrue(record.Id > 0);

            var newRecord = repository.FindBy(record.Id);
            Assert.AreEqual(record.Id, newRecord.Id);

            repository.Delete(newRecord);
            newRecord = repository.FindBy(record.Id);
            Assert.IsNull(newRecord);
        }
    }
}
