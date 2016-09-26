using System;
using System.Collections.Generic;
using System.Linq;
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
            //Database.MongoDB mongo = new Database.MongoDB();
            //mongo.t();
            DateTime dtime = DateTime.Now;
            string s = dtime.ToString("dd/MM/yyyy HH:mm");
            Application.Run(new Form1());
        }
    }
}
