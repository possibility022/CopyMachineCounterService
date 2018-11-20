using System;
using System.IO;
using CopyinfoWPF.Workflows.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopyinfoWPF.Tests.Workflow
{
    [TestClass]
    public class MimeReaderTests
    {
        private const string FolderPath = "D:\\TMP\\records\\";

        [TestMethod]
        [Ignore]
        [TestCategory("DatabaseConnection")]
        public void DeserializeEmailFromBytes()
        {
            var files = Directory.GetFiles(FolderPath, "*.bytes");

            foreach(var f in files)
            {
                var mime = new EmailMessage(File.ReadAllBytes(f));
                File.WriteAllText(FolderPath + Path.GetFileName(f) + ".txt", mime.TextBody);
            }
        }
    }
}
