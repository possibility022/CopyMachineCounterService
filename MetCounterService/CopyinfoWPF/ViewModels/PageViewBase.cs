using CopyinfoWPF.Interfaces;
using Prism.Mvvm;

namespace CopyinfoWPF.ViewModels
{
    public abstract class PageViewBase : BindableBase, IPageView
    {
        public abstract string ViewName { get; }

        public virtual void Select()
        {
            
        }
    }
}
