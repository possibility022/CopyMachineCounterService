using MahApps.Metro.Controls;
using System.Diagnostics;

namespace CopyinfoWPF
{
    /// <summary>
    /// Interaction logic for MahMainWindow.xaml
    /// </summary>
    public partial class MahMainWindow : MetroWindow
    {
        public MahMainWindow()
        {
            InitializeComponent();
        }

        private void ContentControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.NewValue);
        }
    }
}
