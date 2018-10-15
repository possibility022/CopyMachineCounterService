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

        public async Task<IEnumerable<MachineRecordViewModel>> StartLoadingAsync()
        {
            LoadingAnimationIsVisible = true;

            Message = "Inicjalizacja podstawowej konfiguracji.";
            await Task.Factory.StartNew(() => Configuration.Configuration.Initialize());

            Message = "Inicjalizacja bazy danych Liczników";
            var recordService = Configuration.Configuration.Container.Resolve<IMachineRecordService>();
            //await MongoTB.InitializeAsync();

            //Message = "Inicjalizacja bazy danych Asystenta";
            //await Database.LocalCache.FirebirdServiceCache.InitializeAsync();

            //Message = "Inicjalizacja pamięci podręcznej.";
            //await DAO.InitializeAsync();

            //Message = "Inicjalizacja skrzynki email.";
            //Email.Initialize(
            //    Encrypting.AES_Decrypt(ConstantData.EncryptedEmailLogin),
            //    Encrypting.AES_Decrypt(ConstantData.EncryptedEmailPassword),
            //    Encrypting.AES_Decrypt(ConstantData.EncryptedEmailSmtpPassword));

            Message = "Pobieram dane z baz danych.";

            var records = await Task.Factory.StartNew(recordService.GetLatestReports);

            LoadingAnimationIsVisible = false;
            return records;
        }
    }
}
