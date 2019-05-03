using System;
using System.Security.Cryptography;
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
            _winDpApi = new WinDpApi(Encoding.UTF8.GetBytes("TEST"));
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

        [TestMethod]
        public void Testing_WithSHA()
        {
            // Arrange
            byte[] bytes;
            using (var sha = SHA256.Create())
                bytes = sha.ComputeHash(Encoding.UTF8.GetBytes("TEST"));

            _winDpApi = new WinDpApi(bytes);
            var windDpApi = new WinDpApi(bytes);

            var stringToProtec = "ABC1234567890@#$%^&*(ERTYUIOCVBNMKJWA HOIDUAHWAIUHD";
            var bytesToProtect = Encoding.UTF8.GetBytes(stringToProtec);
            var @protected = _winDpApi.Protect(bytesToProtect);

            // Act
            var unprotected = windDpApi.Unprotect(@protected);

            var results = Encoding.UTF8.GetString(unprotected);

            Assert.AreEqual(stringToProtec, results);
        }


    }
}
