using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopyinfoWPF.Security.UnitTests
{
    [TestClass]
    public class WinDpApiTests
    {

        WinDpApi _winDpApi;

        [TestInitialize]
        public void TestInit()
        {
            _winDpApi = new WinDpApi();
        }

        [TestMethod]
        public void Testing()
        {
            // Arrange
            var stringToProtec = "ABC1234567890@#$%^&*(ERTYUIOCVBNMKJWA HOIDUAHWAIUHD";
            var bytesToProtect = Encoding.UTF8.GetBytes(stringToProtec);
            var @protected = _winDpApi.Protect(bytesToProtect);

            // Act
            var unprotected = _winDpApi.Unprotect(@protected);

            var results = Encoding.UTF8.GetString(unprotected);

            Assert.AreEqual(stringToProtec, results);
        }
    }
}
