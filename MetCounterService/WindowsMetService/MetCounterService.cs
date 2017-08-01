﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Collections.Generic;

namespace WindowsMetService
{
    public partial class MetCounterService : ServiceBase
    {
        private Engine engine;

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        ServiceStatus serviceStatus = new ServiceStatus();

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        static System.Diagnostics.EventLog eventLog1;

        public MetCounterService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MetServiceLogSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MetServiceLogSource", "MetCounterLog");
            }
            eventLog1.Source = "MetServiceLogSource";
            eventLog1.Log = "MetCounterLog";
            Global.SetSystemEventLog(eventLog1);
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            LocalDatabase.Initialize();

            engine = new Engine();

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            Global.Log("Wystartowano process.");
        }

        protected override void OnStop()
        {
            engine.Stop();
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

    }
}
