using CopyinfoWPF.Security;
using Prism.Mvvm;
using System.Security;

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
    }
}
