﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
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

#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MetService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
