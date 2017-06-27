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
            //Database.FirebirdTB.test();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string password = Security.Encrypting.AES_Encrypt("***REMOVED***--_][");
            string login = Security.Encrypting.AES_Encrypt("***REMOVED***");

            Application.Run(new Copyinfo.Forms.FCopyInfo());
        }
    }
}
