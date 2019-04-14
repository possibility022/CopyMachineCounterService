using Unity;
using CopyinfoWPF.Security;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System.Security;
using System.Threading.Tasks;
using CopyinfoWPF.DTO.Models;
using System.Windows;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.Formatters;
using AutoMapper;
using CopyinfoWPF.Configuration;
using CopyinfoWPF.Workflows.Email;
using AutoUpdaterDotNET;
using CopyinfoWPF.Interfaces;
using CopyinfoWPF.Services.Implementation;

namespace CopyinfoWPF.ViewModels
{
    class SplashScreenViewModel : BindableBase
    {

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _message;
        private object @Lock = new object();

        private bool _checkedForUpdates;

        private string _loadingAnimationVisible;
        public string LoadingAnimationVisible
        {
            get => _loadingAnimationVisible;
            private set => SetProperty(ref _loadingAnimationVisible, value);
        }
        
        public SplashScreenViewModel()
        {
            ShowAnimation(false);
        }

        public SplashScreenViewModel(IMachineRecordService machineRecordService, IDeviceService deviceService)
        {
            ShowAnimation(false);
            _deviceService = deviceService;
            _machineRecordService = machineRecordService;
        }

        private IDeviceService _deviceService;
        private IMachineRecordService _machineRecordService;

        private void ShowAnimation(bool visible)
        {
            LoadingAnimationVisible = visible ? "Visible" : "Hidden";
        }

        public bool LoginClick(SecureString password)
        {
            var copyOfPassword = password.Copy();
            copyOfPassword.MakeReadOnly();

            bool passwordCorrect = Encrypting.DecryptSecureString(copyOfPassword, (result) =>
             {
                 return Encrypting.DecryptPassword(result);
             });

            if (passwordCorrect)
            {
                return true;
            }
            else
            {
                Message = "Błędne hasło. :(";
                return false;
            }
        }

        public async Task<Window> StartLoadingAsync()
        {
            ShowAnimation(true);

            Message = "Inicjalizacja automappera.";
            await Task.Factory.StartNew(InitializeAutoMapper);

            Message = "Inicjalizacja cach'u.";
            Cache.InitializeCache();

            Message = "Uzupełnianie cachu.";
            _machineRecordService.RefreshCache();

            Message = "Pobieram dane z baz danych.";
            var records = await Task.Factory.StartNew(_machineRecordService.GetAll);

            Message = "Tworzę okno aplikacji.";
            var window = new MahMainWindow();

            var recordsModel = new ReportsViewModel();
            recordsModel.SetRecords(records);

            var views = new IPageView[]
            {
                recordsModel,
                new DevicesViewModel(UnityConfiguration.Container.Resolve<IDeviceService>()),
                new ClientsViewModel(UnityConfiguration.Container.Resolve<IClientService>()),
            };


            Message = "Uzupełniam widok pobranymi danymi.";
            window.DataContext = new MahMainWindowModel(views);

            ShowAnimation(false);
            return window;
        }

        public async Task CheckForUpdates()
        {
            if (!_checkedForUpdates)
            {
                await Task.Factory.StartNew(() => System.Threading.Thread.Sleep(1000)); // I have no idea what is going on but without 
                                                                                        // this delay update window is shown only for few sec.
                                                                                        // It works in debug and in most cases when you run it from VStudio.
                                                                                        // To see efects (BUG) try to run application by double clicking on .exe file.
                AutoUpdater.RunUpdateAsAdmin = false;
                AutoUpdater.Start(App.NewVersionUrl, System.Reflection.Assembly.GetExecutingAssembly());
                _checkedForUpdates = true;
            }

        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
        }

    }
}
