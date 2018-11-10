using Unity;
using CopyinfoWPF.Model;
using CopyinfoWPF.Security;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using CopyinfoWPF.DTO.Models;
using System.Windows;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.Formatters;
using AutoMapper;
using CopyinfoWPF.Configuration;
using CopyinfoWPF.Workflows.Email;

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

            Message = "Inicjalizacja bazy danych Liczników";
            var recordService = await Task<IMachineRecordService>.Factory.StartNew(() => Configuration.Configuration.Container.Resolve<IMachineRecordService>());

            Message = "Pobieram dane z baz danych.";
            var records = await Task.Factory.StartNew(recordService.GetLatestReports);

            Message = "Tworzę okno aplikacji.";
            var window = new MahMainWindow();

            Message = "Uzupełniam widok pobranymi danymi.";
            window.SetRecords(records);

            LoadingAnimationIsVisible = false;
            return window;
        }

        private void InitializeUnity()
        {
            Configuration.Configuration.Initialize();
            Configuration.Configuration.Container.RegisterType<IFormatter<MachineRecordViewModel>, RecordFormatter>();
            Configuration.Configuration.Container.RegisterType<IFormatter<EmailMessage>, RecordFormatter>();
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
        }

    }
}
