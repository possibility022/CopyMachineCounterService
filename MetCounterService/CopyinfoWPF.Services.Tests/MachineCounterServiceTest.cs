using System;
using CopyinfoWPF.ORM;
using CopyinfoWPF.Services.Implementation;
using CopyinfoWPF.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopyinfoWPF.Services.Tests
{
    [TestClass]
    [TestCategory("DatabaseConnection")]
    public class MachineCounterServiceTest
    {

        // Integration tests.

        //[ClassInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    Configuration.UnityConfiguration.Initialize();
        //}

        //[TestCategory("DatabaseConnection")]
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    IMachineCounterService service = new MachineCounterService(new DatabaseSessionProvider());
        //    var records = service.TakeRecords(10);
        //    Assert.AreEqual(10, records.Count);
        //}
    }
}
