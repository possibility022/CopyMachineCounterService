using System;
using WindowsMetService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.WindowsMetServiceTest
{
    [TestClass]
    public class LocalDatabaseTest
    {
        [TestMethod]
        public void CheckCommentInIpConfigFiles()
        {
            string[] ips = LocalDatabase.GetMachinesIps();

        }
    }
}
