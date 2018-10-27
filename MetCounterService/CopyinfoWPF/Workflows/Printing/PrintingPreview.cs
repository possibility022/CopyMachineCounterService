using CopyinfoWPF.Common;
using CopyinfoWPF.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace CopyinfoWPF.Workflows.Printing
{
    public class PrintingPreview : IDisposable
    {

        public static PrintingPreview CreatePreview(IEnumerable<string> text)
        {
            var preview = new PrintingPreview();
            preview.CreateDocument(text);
            return preview;
        }

        private XpsDocument _xpsDocument;
        private Uri _packageUri;

        public XpsDocument XpsDocument { get => _xpsDocument; private set => _xpsDocument = value; }

        public void RemovePackage()
        {
            if (_packageUri != null)
                PackageStore.RemovePackage(_packageUri);
        }


        private Uri GenerateUri()
        {
            var random = new Random(DateTime.Now.Millisecond);

            var stringBuilder = new StringBuilder(@"memorystream://");

            for (int i = 0; i < 15; i++)
            {
                int r = random.Next(CommonData.AllStandardCharacters.Length);
                stringBuilder.Append(CommonData.AllStandardCharacters[r]);
            }

            stringBuilder.Append(".xps");

            return new Uri(stringBuilder.ToString());
        }

        public void CreateDocument(IEnumerable<string> text)
        {
            RemovePackage();

            DocumentPaginator paginator = new TextDocumentPaginator(text, CommonData.PageSizes[PageSizes.A4]);

            var stream = new MemoryStream();

            var package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);

            _packageUri = GenerateUri();

            PackageStore.AddPackage(_packageUri, package);
            XpsDocument = new XpsDocument(package)
            {
                Uri = _packageUri
            };

            XpsDocument.CreateXpsDocumentWriter(XpsDocument).Write(paginator);
        }

        public void Dispose()
        {
            RemovePackage();
        }
    }
}
