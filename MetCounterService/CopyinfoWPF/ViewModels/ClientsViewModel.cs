using CompareThis;
using CopyinfoWPF.Commands;
using CopyinfoWPF.Configuration;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Services.Interfaces;
using CopyinfoWPF.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CopyinfoWPF.ViewModels
{
    public class ClientsViewModel : PageViewBase<ClientViewModel>
    {
        public override string ViewName => "Clients";

        public ClientsViewModel() : this(UnityConfiguration.Resolve<IClientService>())
        {
            
        }

        public ClientsViewModel(IClientService clientService) : base(clientService)
        {
            FilterLogic = BuidFilterFunc();
            DataGridDoubleClickCommand = new BaseCommand(OpenSelectedRecord);
            FilterTextKeyUpCommand = new BaseCommand(ApplyFilters);
            RefreshCommand = new AsyncCommand(RefreshAsync, CanRefresh);
        }


        private async Task RefreshAsync()
        {
            _canRefresh = false;
            var records = await Task.Factory.StartNew(_baseService.GetAll);
            _sourceCollection.Clear();
            _sourceCollection.AddRange(records);
            Collection = CollectionViewSource.GetDefaultView(_sourceCollection);
            _canRefresh = true;
        }


        private void OpenSelectedRecord()
        {
            var clientOverviewViewModel = new ClientOverviewViewModel();
            var deviceOverviewViewModel = UnityConfiguration.Resolve<DeviceOverviewViewModel>();
            var reportOverviewViewModel = UnityConfiguration.Resolve<ReportOverviewViewModel>();

            clientOverviewViewModel.LoadClient((SelectedItems?.FirstOrDefault() as ClientViewModel).Id);

            clientOverviewViewModel.DeviceSelected += deviceOverviewViewModel.OnDeviceSelected;
            deviceOverviewViewModel.RecordSelected += reportOverviewViewModel.OnRecordSelected;

            new OverviewView(clientOverviewViewModel, deviceOverviewViewModel, reportOverviewViewModel)
                .Show();
        }

        private Func<ClientViewModel, string, bool> BuidFilterFunc()
        {
            var settings = new CompareThis.Settings()
            {
                StringCompareOptions = System.Globalization.CompareOptions.IgnoreCase
            };

            settings.SetStandardWhiteList();
            var function = CompareFactory.BuildContainsFunc<ClientViewModel>(settings);

            return function;
        }

    }
}
