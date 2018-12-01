using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfBindingErrors;

namespace CopyinfoWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public const string NewVersionUrl  = "http://***REMOVED***/copyinfo/version.xml";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start listening for WPF binding error.
            // After that line, a BindingException will be thrown each time
            // a binding error occurs
            BindingExceptionThrower.Attach();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string file = Path.GetFullPath("CopyinfoErrorLog.log");
            File.AppendAllText(file, Environment.NewLine + ((Exception)e.ExceptionObject).Message);
            MessageBox.Show($"Błąd. Logi znajdziesz tutaj: {file}");
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string file = Path.GetFullPath("CopyinfoErrorLog.log");
            File.AppendAllText(file, Environment.NewLine + e.Exception.Message);
            MessageBox.Show($"Błąd. Logi znajdziesz tutaj: {file}");
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "App.OnDispatcherUnhandledException()", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
