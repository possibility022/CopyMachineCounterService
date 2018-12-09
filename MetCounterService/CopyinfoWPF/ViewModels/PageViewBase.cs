using CopyinfoWPF.Interfaces;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System.ComponentModel;
using System.Windows.Data;

namespace CopyinfoWPF.ViewModels
{
    public abstract class PageViewBase<T> : BindableBase, IPageView
    {

        public PageViewBase() { }

        public PageViewBase(BaseService<T> baseService)
        {
            _baseService = baseService;
        }

        protected BaseService<T> _baseService;
        protected bool Loaded { get; set; }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }

        public abstract string ViewName { get; }

        private ICollectionView _collection;
        public ICollectionView Collection
        {
            get { return _collection; }
            set { SetProperty(ref _collection, value); }
        }

        public virtual void Selected()
        {
            if (Loaded == false)
            {
                Loaded = true;

                var items = _baseService.GetAll();
                Collection = CollectionViewSource.GetDefaultView(items);
            }
        }
    }
}
