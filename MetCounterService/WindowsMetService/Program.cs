using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMetService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000 * 60; // 60 seconds * 60 = 1h
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Program.OnTimer);
            timer.Start();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MetCounterService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }


        static public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            int i = 0;
        }
    }
}
