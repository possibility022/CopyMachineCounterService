using CopyinfoWPF.Common.File;
using CopyinfoWPF.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CopyinfoWPF.Tests.ViewModels
{
    [TestClass]
    public class SplashScreenViewModelTests
    {

        SplashScreenViewModel model;
        Mock<IFileOperation> fileOperations;


        [TestInitialize]
        public void TestInit()
        {
            model = new SplashScreenViewModel();
            fileOperations = new Mock<IFileOperation>();
            model.FileOperation = fileOperations.Object;
        }

        [TestMethod]
        public void EncryptedJson_IsNotNull()
        {
            var settings = new Settings.BasicSettings()
            {
                AsystentDatabase = "ABC",
                CopyInfoDatabase = "CBA"
            };

            var password = new System.Security.SecureString();
            foreach (var c in "TEST")
                password.AppendChar(c);

            // Act
            var json = model.EncryptSettings(settings, password);


            Assert.IsNotNull(json);
        }
    }
}
