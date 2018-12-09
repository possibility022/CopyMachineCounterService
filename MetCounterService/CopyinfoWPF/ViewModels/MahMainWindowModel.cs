using CopyinfoWPF.Commands;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Interfaces;
using Prism.Mvvm;
using System.Collections.Generic;
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

        public MahMainWindowModel(IEnumerable<MachineRecordViewModel> records)
        {
            var recordsView = new ReportsViewModel();
            recordsView.SetRecords(records);

            Views = new IPageView[]
            {
                recordsView,
                new DevicesViewModel()
            };

            SwitchViewCommand = new RelayCommand(SwitchViewAction, CanSwitchView);
        }

        public MahMainWindowModel() : this(new List<MachineRecordViewModel>())
        {
            
        }

        private void SwitchViewAction(object view)
        {
            System.Diagnostics.Debug.WriteLine(view?.GetType().ToString());
            CurrentView = (IPageView)view;
        }

        private bool CanSwitchView(object view)
        {
            var iView = (IPageView)view;
            return iView != null;
        }
    }
}
