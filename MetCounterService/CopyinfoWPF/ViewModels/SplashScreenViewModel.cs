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
using System.IO;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
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

        private bool _loginEnabled;
        public bool LoginEnabled
        {
            get { return _loginEnabled; }
            set { SetProperty(ref _loginEnabled, value); }
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
            FileOperation = new FileOperationWrapper();

            if (!FileOperation.Exists(SettingsPath))
            {
                if (FileOperation.Exists(SettingsPathUnprotected))
                {
                    InitializeEncrypting();
                }
                else
                {
                    Message = "Nie znaleziono pliku ustawień.";
                }
            }
            else
            {
                // _loginButtonAction todo set this <---
                //todo decrypt settings
            }
        }

        private void InitializeEncrypting()
        {
            Message = "Wprowadz nowe hasło";
            _loginButtonAction = EncryptSettingsAction;
            ConfirmPasswordVisible = Visibility.Visible;
        }

        public SplashScreenViewModel(IMachineRecordService machineRecordService, IDeviceService deviceService)
        {
            _deviceService = deviceService;
            _machineRecordService = machineRecordService;
        }

        private IDeviceService _deviceService;
        private IMachineRecordService _machineRecordService;

        private void ShowAnimation(bool visible)
        {
            LoadingAnimationVisible = visible ? "Visible" : "Hidden";
        }

        public bool LoginClick(SecureString password, SecureString confirm)
        {
            return _loginButtonAction.Invoke(password, confirm);
        }

        public bool LoginAction(SecureString password)
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

                return true;
            }
            else
            {
                Message = "Hasła się różnią.";
                return false;
            }
        }

        public string EncryptSettings(BasicSettings settings, SecureString password)
        {
            byte[] passwordHash =
                Encrypting.DecryptSecureString(password.Copy(), p => Encrypting.ComputeSha(Encoding.UTF8.GetBytes(p)));
            byte[] salt = Encrypting.ComputeSha(Encoding.UTF8.GetBytes(Environment.MachineName));

            var full = CombineArrays(passwordHash, salt);


            var winDpApi = new WinDpApi(full);
            var serializer = new SimpleSerializer();

            var objectEncryptor = new ObjectEncryptor(winDpApi, serializer);

            return objectEncryptor.Encrypt(settings);
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
