﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MetCounterService
{
    public partial class ReportService : ServiceBase
    {
        public ReportService()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("LogSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "LogSource", "MetServiceLog");
            }
            eventLog1.Source = "LogSource";
            eventLog1.Log = "MetServiceLog";
        }

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

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("Met Report Service - Start");
            //string v = LocalDatabase.Version;
            //LocalDatabase.Initialize();
            //eventLog1.WriteEntry("Local database initialized");
            //MainCore.setupTrigger();
            //eventLog1.WriteEntry("Trigger have been set");


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {

        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information);
        }

        protected override void OnContinue()
        {
            base.OnContinue();
        }
    }
}
