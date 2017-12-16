using CopyinfoWPF.Security;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using CopyinfoWPF.Common;
using CopyinfoWPF.Resources;
using System.Threading;
using CopyinfoWPF.Database;

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

        public async Task<IEnumerable<MachineRecord>> StartLoadingAsync()
        {
            LoadingAnimationIsVisible = true;
            Message = "Inicjalizacja bazy danych Liczników";
            await MongoTB.InitializeAsync();

            Message = "Inicjalizacja bazy danych Asystenta";
            await Database.LocalCache.FirebirdServiceCache.InitializeAsync();

            Message = "Inicjalizacja pamięci podręcznej.";
            await DAO.InitializeAsync();

            Message = "Inicjalizacja skrzynki email.";
            Email.Initialize(
                Encrypting.AES_Decrypt(ConstantData.EncryptedEmailLogin),
                Encrypting.AES_Decrypt(ConstantData.EncryptedEmailPassword),
                Encrypting.AES_Decrypt(ConstantData.EncryptedEmailSmtpPassword));

            Message = "Pobieram dane z baz danych.";

            IEnumerable<MachineRecord> records = await DAO.GetAllReportsAsync();

            LoadingAnimationIsVisible = false;
            return records;
        }
    }
}
