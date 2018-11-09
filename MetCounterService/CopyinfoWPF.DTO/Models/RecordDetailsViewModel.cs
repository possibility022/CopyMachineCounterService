using Prism.Mvvm;
using System.Collections.Generic;

namespace CopyinfoWPF.DTO.Models
{
    public class RecordDetailsViewModel<T> : BindableBase
    {

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private IEnumerable<T> _list;
        public IEnumerable<T> List
        {
            get { return _list; }
            set { SetProperty(ref _list, value); }
        }

    }
}
