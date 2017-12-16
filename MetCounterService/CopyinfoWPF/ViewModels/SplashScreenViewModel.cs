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

        public async Task StartLoadingAsync()
        {
            Message = "Inicjalizacja bazy danych Liczników";
            await Database.MongoTB.InitializeAsync();

            await SleepAsync();

            Message = "Inicjalizacja bazy danych Asystenta";
            await Database.LocalCache.FirebirdServiceCache.InitializeAsync();

            await SleepAsync();

            Message = "Inicjalizacja pamięci podręcznej.";
            await Database.DAO.InitializeAsync();

            await SleepAsync();

            Message = "Inicjalizacja skrzynki email.";
            Email.Initialize(
                Encrypting.AES_Decrypt(ConstantData.EncryptedEmailLogin),
                Encrypting.AES_Decrypt(ConstantData.EncryptedEmailPassword),
                Encrypting.AES_Decrypt(ConstantData.EncryptedEmailSmtpPassword));
        }

        public async Task SleepAsync()
        {
            await Task.Run(() => Thread.Sleep(1000));
        }
    }
}
