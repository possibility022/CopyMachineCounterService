﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceProcess;
using System.Text;
using System.Reflection;
using System.Configuration.Install;

namespace MetCounterService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ReportService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
