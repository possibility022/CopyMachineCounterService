using CopyinfoWPF.Commands;
using CopyinfoWPF.Interfaces;
using Prism.Mvvm;
using System.Windows.Input;

namespace CopyinfoWPF.ViewModels
{
    public class MahMainWindowModel : BindableBase
    {
        private IPageView _currentView;

        public IPageView[] Views { get; }

        public ICommand SwitchViewCommand { get; set; }

        public IPageView CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public MahMainWindowModel(IPageView[] views)
        {
            SwitchViewCommand = new RelayCommand(SwitchViewAction, CanSwitchView);
            Views = views;
        }

        public MahMainWindowModel()
        {
            
        }

        private void SwitchViewAction(object view)
        {
            System.Diagnostics.Debug.WriteLine(view?.GetType().ToString());
            CurrentView = (IPageView)view;
            CurrentView.Selected();
        }

        private bool CanSwitchView(object view)
        {
            var iView = (IPageView)view;
            return iView != null;
        }
    }
}
