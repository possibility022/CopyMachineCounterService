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

        public string LoadingAnimationVisible { get { return _loadingAnimationIsVisible ? "Visible" : "Hidden"; } }

        private bool LoadingAnimationIsVisible
        {
            get { return _loadingAnimationIsVisible; }
            set
            {
                SetProperty(ref _loadingAnimationIsVisible, value);
                RaisePropertyChanged(nameof(LoadingAnimationVisible));
            }
        }
        private bool _loadingAnimationIsVisible;

        public SplashScreenViewModel()
        {
            _loadingAnimationIsVisible = false;
        }

        public bool LoginClick(SecureString password)
        {

            SecureString copyOfPassword = password.Copy();
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
            LoadingAnimationIsVisible = true;

            Message = "Inicjalizacja podstawowej konfiguracji.";
            await Task.Factory.StartNew(InitializeUnity);

            Message = "Inicjalizacja automappera.";
            await Task.Factory.StartNew(InitializeAutoMapper);

            Message = "Inicjalizacja cach'u.";
            Cache.InitializeCache();

            Message = "Inicjalizacja bazy danych Liczników";
            var recordService = await Task<IMachineRecordService>.Factory.StartNew(() => Configuration.Configuration.Container.Resolve<IMachineRecordService>());

            Message = "Uzupełnianie cachu.";
            recordService.RefreshCache();

            Message = "Pobieram dane z baz danych.";
            var records = await Task.Factory.StartNew(recordService.GetAll);

            Message = "Tworzę okno aplikacji.";
            var window = new MahMainWindow();

            var recordsModel = new ReportsViewModel();
            recordsModel.SetRecords(records);

            var views = new IPageView[]
            {
                recordsModel,
                new DevicesViewModel(Configuration.Configuration.Container.Resolve<IDeviceService>())
            };
            

            Message = "Uzupełniam widok pobranymi danymi.";
            window.DataContext = new MahMainWindowModel(views);

            LoadingAnimationIsVisible = false;
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

        private void InitializeUnity()
        {
            Configuration.Configuration.Initialize();
            Configuration.Configuration.Container.RegisterType<IFormatter<MachineRecordRowView>, RecordFormatter>();
            Configuration.Configuration.Container.RegisterType<IFormatter<EmailMessage>, RecordFormatter>();
            Configuration.Configuration.Container.RegisterType<IFormatter<RecordViewModel>, RecordFormatter>();
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
        }

    }
}
