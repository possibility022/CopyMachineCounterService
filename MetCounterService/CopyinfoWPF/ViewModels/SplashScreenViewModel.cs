using Unity;
using CopyinfoWPF.Security;
using CopyinfoWPF.Services.Interfaces;
using Prism.Mvvm;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using CopyinfoWPF.Configuration;
using AutoUpdaterDotNET;
using CopyinfoWPF.Interfaces;
using CopyinfoWPF.Services.Implementation;
using Newtonsoft.Json;
using System;
using System.Text;
using CopyinfoWPF.Common;
using CopyinfoWPF.Settings;
using CopyinfoWPF.Common.File;

namespace CopyinfoWPF.ViewModels
{
    public class SplashScreenViewModel : BindableBase
    {

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _loginButtonText;
        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set { SetProperty(ref _loginButtonText, value); }
        }

        private Visibility _confirmPasswordVisible;
        public Visibility ConfirmPasswordVisible
        {
            get { return _confirmPasswordVisible; }
            set { SetProperty(ref _confirmPasswordVisible, value); }
        }

        private string _message;

        private bool _checkedForUpdates;

        private string _loadingAnimationVisible;
        public string LoadingAnimationVisible
        {
            get => _loadingAnimationVisible;
            private set => SetProperty(ref _loadingAnimationVisible, value);
        }

        Func<SecureString, SecureString, bool> _loginButtonAction;

        public string SettingsPath { get; set; }
        public string SettingsPathUnprotected { get; set; }

        public IFileOperation FileOperation { get; set; }

        public SplashScreenViewModel()
        {
            ShowAnimation(false);

            SettingsPath = App.SettingsPath;
            SettingsPathUnprotected = App.SettingsPathUnProtected;

            FileOperation = new FileOperationWrapper();

            if (!FileOperation.Exists(SettingsPath))
            {
                if (FileOperation.Exists(SettingsPathUnprotected))
                {
                    InitializeEncrypting();
                }
                else
                {
                    CreateEmptySettingsFile();
                    Message = $"Uzupełnij plik {SettingsPathUnprotected} o niezbędne dane i wprowadź nowe hasło.";
                }
            }
            else
            {
                ConfirmPasswordVisible = Visibility.Hidden;
                LoginButtonText = "Login";
                _loginButtonAction = LoginAction;
            }
        }

        private BasicSettings _settings;

        private void InitializeEncrypting()
        {
            Message = "Wprowadz nowe hasło";
            LoginButtonText = "Zatwierdź";
            _loginButtonAction = EncryptSettingsAction;
            ConfirmPasswordVisible = Visibility.Visible;
        }

        private IMachineRecordService _machineRecordService;

        private void ShowAnimation(bool visible)
        {
            LoadingAnimationVisible = visible ? "Visible" : "Hidden";
        }

        public bool LoginClick(SecureString password, SecureString confirm)
        {
            return _loginButtonAction.Invoke(password, confirm);
        }

        public bool LoginAction(SecureString password, SecureString _)
        {
            var settings = DecryptSettings(password);

            if (settings != null)
            {
                _settings = settings;
                return true;
            }
            else
            {
                Message = "Błędne hasło. :(";
                return false;
            }
        }

        private bool EncryptSettingsAction(SecureString password, SecureString confirm)
        {
            if (Encrypting.DecryptSecureString(password.Copy(), pass =>
            {
                return Encrypting.DecryptSecureString(confirm.Copy(), conf =>
                {
                    return pass.Equals(conf, StringComparison.CurrentCulture);
                });
            }))
            {
                var json = FileOperation.ReadAllText(SettingsPathUnprotected);
                var settings = JsonConvert.DeserializeObject<BasicSettings>(json);

                json = EncryptSettings(settings, password.Copy());

                FileOperation.WriteAllText(SettingsPath, json);
                CreateEmptySettingsFile();

                LoginButtonText = "Login";
                _loginButtonAction = LoginAction;
                ConfirmPasswordVisible = Visibility.Hidden;

            }
            else
            {
                Message = "Hasła się różnią.";
            }

            return false;
        }

        private void CreateEmptySettingsFile() =>
            FileOperation.WriteAllText(SettingsPathUnprotected, JsonConvert.SerializeObject(new BasicSettings(), Formatting.Indented));



        private ObjectEncryptor GetObjectEncryptor(SecureString password)
        {
            byte[] passwordHash =
                Encrypting.DecryptSecureString(password.Copy(), p => Encrypting.ComputeSha(Encoding.UTF8.GetBytes(p)));
            byte[] salt = Encrypting.ComputeSha(Encoding.UTF8.GetBytes(Environment.MachineName));

            var full = CombineArrays(passwordHash, salt);


            var winDpApi = new WinDpApi(full);
            var serializer = new SimpleSerializer();

            return new ObjectEncryptor(winDpApi, serializer);
        }

        public string EncryptSettings(BasicSettings settings, SecureString password)
        {
            var oe = GetObjectEncryptor(password);

            return oe.Encrypt(settings);
        }

        public BasicSettings DecryptSettings(SecureString password)
        {
            var json = FileOperation.ReadAllText(SettingsPath);
            var oe = GetObjectEncryptor(password);
            json = oe.Decrypt<BasicSettings>(json);

            if (json == null)
                return null;

            return JsonConvert.DeserializeObject<BasicSettings>(json);
        }

        private static byte[] CombineArrays(byte[] a, byte[] b)
        {
            byte[] combined = new byte[a.Length + b.Length];

            int l = 0;

            for (int i = 0; i < a.Length; i++)
                SetByte(ref combined, ref a[i], ref l);

            for (int i = 0; i < b.Length; i++)
                SetByte(ref combined, ref b[i], ref l);

            return combined;
        }

        private static void SetByte(ref byte[] target, ref byte value, ref int index)
        {
            target[index] = value;
            index++;
        }

        public async Task<Window> StartLoadingAsync()
        {
            ShowAnimation(true);

            try
            {

                Message = "Inicjalizacja automappera.";
                await Task.Factory.StartNew(InitializeAutoMapper);

                Message = "Konfigurowanie połączeń baz danych.";
                await Task.Factory.StartNew(() => UnityConfiguration.RegisterDatabaeses(_settings.AsystentDatabase, _settings.CopyInfoDatabase));

                Message = "Inicjalizacja cach'u.";
                Cache.InitializeCache();

                Message = "Uzupełnianie cachu.";
                _machineRecordService = UnityConfiguration.Resolve<IMachineRecordService>();
                await Task.Factory.StartNew(_machineRecordService.RefreshCache);

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
                return window;
            }
            catch (Exception ex)
            {
                throw; //todo logging
            }
            finally
            {
                ShowAnimation(false);
            }


            return null;
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
