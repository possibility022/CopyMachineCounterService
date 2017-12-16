using CopyinfoWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CopyinfoWPF.Views
{
    /// <summary>
    /// Interaction logic for PasswordPrompt.xaml
    /// </summary>
    public partial class PasswordPrompt : Window
    {

        PasswordPromptViewModel _viewModel;

        PasswordPromptViewModel ViewModel { get => _viewModel ?? (_viewModel = (PasswordPromptViewModel)DataContext); }

        public PasswordPrompt()
        {
            InitializeComponent();
        }

        private void ButtonOkClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Accept())
            {
                Close();
            }
        }
    }
}
