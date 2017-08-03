using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsMetService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTests.WindowsMetServiceTest
{
    /// <summary>
    /// Summary description for JsonConv
    /// </summary>
    [TestClass]
    public class JsonConv
    {
        public JsonConv()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SaveAndLoadSettings()
        {
            WindowsMetService.Settings settings = new Settings()
            {
                ClientDescription = "Opis klienta",
                ClientIddd = Encoding.UTF8.GetBytes("akjsdlKAWjdhkajwhdka"),
                ForceRead = false,
                LastTick = "",
                Version = "2.7",
                SaveLogsToSystem = true
            };

            string settingConverted = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("test.txt", settingConverted);

            string loadedString = File.ReadAllText("test.txt");

            Settings loadedSettings = JsonConvert.DeserializeObject<Settings>(loadedString);

            Assert.IsTrue(loadedSettings.ClientIddd.SequenceEqual(settings.ClientIddd));
            Assert.AreEqual(settings.ClientDescription, loadedSettings.ClientDescription);
            Assert.AreEqual(settings.ForceRead, loadedSettings.ForceRead);
            Assert.AreEqual(settings.Version, loadedSettings.Version);
            Assert.AreEqual(settings.SaveLogsToSystem, loadedSettings.SaveLogsToSystem);


            if (File.Exists("test.txt"))
                File.Delete("test.txt");
        }
    }
}
