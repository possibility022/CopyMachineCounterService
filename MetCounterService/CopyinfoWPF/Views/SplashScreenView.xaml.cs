using CopyinfoWPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace CopyinfoWPF.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreenView : Window
    {

        SplashScreenViewModel _viewModel;

        SplashScreenViewModel ViewModel { get => _viewModel ?? (_viewModel = (SplashScreenViewModel)DataContext); }

        public SplashScreenView()
        {
            InitializeComponent();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.LoginClick(PasswordBox.SecurePassword))
            {
                var window = await ViewModel.StartLoadingAsync();

                window.Show();
                Close();
            }
        }

        private void OnPasswordBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginClick(sender, null);
            }
            else if (e.Key == Key.F3)
            {
                new TestWindow().Show();
            }
            else
            {
                ViewModel.Message = "";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CheckForUpdates();
        }
    }
}
