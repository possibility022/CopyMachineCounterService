﻿using CopyinfoWPF.Common;
using CopyinfoWPF.ViewModels;
using CopyinfoWPF.Views;
using System;
using System.Configuration;
using System.Windows;
using Unity;
using WpfBindingErrors;

namespace CopyinfoWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static string NewVersionUrl { get => ConfigurationManager.AppSettings["UpdateUrl"]; }
        public static string SettingsPath = "settings.set";
        public static string SettingsPathUnProtected = "settings.json";

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            Log.ConfigureNLog();
            Configuration.UnityConfiguration.Initialize();

            // Start listening for WPF binding error.
            // After that line, a BindingException will be thrown each time
            // a binding error occurs
            BindingExceptionThrower.Attach();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            var splashScreenViewModel = Configuration.UnityConfiguration.Container.Resolve<SplashScreenViewModel>();
            var window = new SplashScreenView() { DataContext = splashScreenViewModel };
            window.Show();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(((Exception)e.ExceptionObject));
            
            MessageBox.Show($"Błąd. Logi znajdziesz tutaj: {Log.FilePath}");
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Error(e.Exception);
            MessageBox.Show($"Błąd. Logi znajdziesz tutaj: {Log.FilePath}");
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "App.OnDispatcherUnhandledException()", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void RegisterTypes()
        {

        }

        private static void RegisterInstances()
        {
        }

        private static void RegisterDatabaeses()
        {

        }

        private static void InitializeFormatters()
        {

        }
    }
}
