using System.Windows;

namespace CopyinfoWPF.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static TViewModel GetViewModel<TViewModel>(this DependencyObject sender)
        {
            FrameworkElement view = (FrameworkElement)sender;
            return (TViewModel)view.DataContext;
        }
    }
}
