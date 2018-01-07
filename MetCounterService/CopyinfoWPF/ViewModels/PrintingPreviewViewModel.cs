using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CopyinfoWPF.ViewModels
{
    class PrintingPreviewViewModel : BindableBase
    {
        public IDocumentPaginatorSource Document
        {
            get { return _document; }
            set { SetProperty(ref _document, value); }
        }

        private IDocumentPaginatorSource _document;
    }
}
