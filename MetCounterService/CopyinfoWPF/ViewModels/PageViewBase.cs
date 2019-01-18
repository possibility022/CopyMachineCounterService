﻿using CopyinfoWPF.DTO;
using CopyinfoWPF.Interfaces;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace CopyinfoWPF.ViewModels
{
    public abstract class PageViewBase<T> : BindableBase, IPageView where T : BaseRow
    {

        public PageViewBase() { }

        public PageViewBase(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set { SetProperty(ref _filterText, value.ToLower()); }
        }

        protected IBaseService<T> _baseService;
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

        public IEnumerable<T> SelectedRecords
        {
            get => _sourceCollection.Where(i => i.IsSelected);
        }

        protected ObservableCollection<T> _sourceCollection = new ObservableCollection<T>();

        public abstract ICommand RefreshCommand { get; protected set; }

        public virtual void Selected()
        {
            if (Loaded == false)
            {
                Loaded = true;

                _sourceCollection.Clear();
                _sourceCollection.AddRange(_baseService.GetAll());
                Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            }
        }
    }
}
