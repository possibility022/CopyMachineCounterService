using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using CopyinfoWPF.Common.Enums;
using CopyinfoWPF.Common;
using System.Text;

namespace CopyinfoWPF.ViewModels
{
    class PrintingPreviewViewModel : BindableBase
    {

        public DocumentViewer DocumentViewer
        {
            get { return _documentViewer; }
            set { SetProperty(ref _documentViewer, value); }
        }

        DocumentViewer _documentViewer;

        public IEnumerable<PageSizes> PageSizes
        {
            get { return _pageSizes; }
            set { SetProperty(ref _pageSizes, value); }
        }

        private IEnumerable<PageSizes> _pageSizes = Enum.GetValues(typeof(PageSizes)).Cast<PageSizes>().ToArray();

        public PageSizes SelectedPageSize
        {
            get { return _selectedPageSize; }
            set { SetProperty(ref _selectedPageSize, value); }
        }

        public PageSizes _selectedPageSize;

        public IDocumentPaginatorSource Document
        {
            get { return _document; }
            private set { SetProperty(ref _document, value); }
        }

        private IDocumentPaginatorSource _document;

        private Uri _packageUri;

        public PrintingPreviewViewModel()
        {
            SelectedPageSize = PageSizes.FirstOrDefault();
        }

        public void CreatePreview(string text)
        {
            if (Document == null)
            {
                throw new InvalidOperationException("Document paginator is null");
            }

            RemovePackage();

            DocumentPaginator paginator = Printing.GetA4Preview(text);

            MemoryStream stream = new MemoryStream();

            Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);
            
            _packageUri = GenerateUri();
            
            PackageStore.AddPackage(_packageUri, package);
            var xpsDoc = new XpsDocument(package);

            xpsDoc.Uri = _packageUri;
            XpsDocument.CreateXpsDocumentWriter(xpsDoc).Write(paginator);
            
            Document = xpsDoc.GetFixedDocumentSequence();
            FixedDocumentSequence seq = new FixedDocumentSequence();
        }

        private Uri GenerateUri()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            StringBuilder stringBuilder = new StringBuilder(@"memorystream://");

            for (int i = 0; i < 15; i++)
            {
                int r = random.Next(CommonData.AllStandardCharacters.Length);
                stringBuilder.Append(CommonData.AllStandardCharacters[r]);
            }

            stringBuilder.Append(".xps");

            return new Uri(stringBuilder.ToString());
        }

        public void RemovePackage()
        {
            if (_packageUri != null)
                PackageStore.RemovePackage(_packageUri);
        }
    }
}
