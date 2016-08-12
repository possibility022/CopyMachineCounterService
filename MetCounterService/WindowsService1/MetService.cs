using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Collections.Generic;

namespace WindowsMetService
{
    public partial class MetService : ServiceBase
    {
        public TimerCallback callback;
        public Timer t;

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

        
        static System.Diagnostics.EventLog eventLog1;

        public MetService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MetServiceSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MetServiceSource", "MetServiceLog");
            }
            eventLog1.Source = "MetServiceSource";
            eventLog1.Log = "MetServiceLog";
            //LocalDatabase.Initialize();
        }

        protected override void OnStart(string[] args)
        {

            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            
            Global.Log("In OnStart");
            //LocalDatabase.Initialize();
            Global.Log("Local database initialized");

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000 * 60; // 60 seconds * 60 = 1h
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            //setupTrigger();

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            LocalDatabase.Initialize();
            Global.Log("Initializing local data - done");
            setupTrigger();
        }

        public void setupTrigger(bool retry = false)
        {
            Global.Log("Setting up trigger");
            callback = doit;

            Random random = new Random();
            double randomvalue = random.NextDouble();

            t = new Timer(callback);
            int msToTick = 20 * 1000;

            DateTime now = DateTime.Now;
            DateTime tickTime = DateTime.Today.AddHours(11.0 + randomvalue);

            if (retry)
            {
                Global.Log("Trigger with retry");
                msToTick = getNextTickIn(tickTime, 20 * 1000);
            }
            else if ((now > tickTime) && LocalDatabase.lastTickWasToday())
            {
                Global.Log("now > tickTime and last tick was tooday - trigger time increase by one day");
                tickTime = tickTime.AddDays(1.0);
            }
            else
            {
                Global.Log("Trigger sets");
                msToTick = getNextTickIn(tickTime, 20);
                msToTick = (int)((tickTime - DateTime.Now).TotalMilliseconds);
                if (msToTick < 10)
                    msToTick = getNextTickIn(tickTime, 20);
            }

            //int msUntilFour = (int)((tickTime - now).TotalMilliseconds);
            t.Change(msToTick, Timeout.Infinite);
        }

        public static int getNextTickIn(DateTime tickTime, int secounds)
        {
            tickTime = DateTime.Now.AddSeconds(secounds);
            return (int)((tickTime - DateTime.Now).TotalMilliseconds);
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public void doit(Object stateInfo)
        {
            try
            {
                string[] ips = LocalDatabase.getMachinesIps();

                Global.Log("tick: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());

                List<Machine> machines = new List<Machine>();

                Network.ServerConnection server = new Network.ServerConnection();

                foreach (string ip in ips)
                {
                    Machine machine = new Machine(ip);
                    //machine.setUpMachine();
                    machines.Add(machine);
                }

                foreach (Machine m in machines)
                {
                    if (server.sendMachine(m) == false)
                        LocalDatabase.putMachineToStorage(m);
                }

                Thread.Sleep(1000 * 20);

                machines = LocalDatabase.getMachinesFromStorage();
                foreach (Machine m in machines)
                    if (server.sendMachine(m) == false)
                        LocalDatabase.putMachineToStorage(m);

                LocalDatabase.setToodayTick();
            }
            catch (Exception ex)
            {
                setupTrigger(true);
                Global.Log("Error in main loop. Message: " + ex.Message);
            }
        }
    }
}
