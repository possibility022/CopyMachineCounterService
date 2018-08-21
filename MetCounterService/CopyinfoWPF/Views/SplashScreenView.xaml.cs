﻿using CopyinfoWPF.Database;
using CopyinfoWPF.ViewModels;
using System.Collections.Generic;
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
                IEnumerable<MachineRecord> records = await ViewModel.StartLoadingAsync();
                MainWindow mainWindow = new MainWindow(records);
                mainWindow.Show();
                Close();
            }
        }

        private void OnPasswordBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginClick(sender, null);
            }else
            {
                ViewModel.Message = "";
            }
        }
    }
}