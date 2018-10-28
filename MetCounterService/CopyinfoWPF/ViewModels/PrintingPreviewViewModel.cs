using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using CopyinfoWPF.Common.Enums;
using CopyinfoWPF.Workflows.Printing;

namespace CopyinfoWPF.ViewModels
{
    class PrintingPreviewViewModel : BindableBase
    {
        public IEnumerable<PageSizes> PageSizesList
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

        private PrintingPreview _printingPreview;

        public PrintingPreviewViewModel()
        {
            SelectedPageSize = PageSizesList.FirstOrDefault();
        }

        public PrintingPreviewViewModel(IDocumentPaginatorSource document) : this()
        {
            Document = document;
        }

        public PrintingPreviewViewModel(PrintingPreview document) : this()
        {
            _printingPreview = document;
            Document = document.XpsDocument.GetFixedDocumentSequence();
        }
    }
}
