using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Forms.FPasswordPrompt password = new Forms.FPasswordPrompt();
            //password.ShowDialog();

            //Security.Encrypting.Initialize(password.tbTextBox1.Text);

            Application.Run(new Copyinfo.Forms.FCopyInfo());
        }
    }
}
