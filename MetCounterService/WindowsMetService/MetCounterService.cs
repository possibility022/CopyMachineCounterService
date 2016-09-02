using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Collections.Generic;

namespace WindowsMetService
{
    public partial class MetCounterService : ServiceBase
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
            //LocalDatabase.Initialize();
            Global.SetSystemEventLog(eventLog1);
            Global.Log("Utworzono logi w konstruktorze");
        }

        protected override void OnStart(string[] args)
        {

            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            //LocalDatabase.Initialize();
            //Global.Log("Local database initialized");

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000 * 60; // 60 seconds * 60 = 1h
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            //setupTrigger();

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            LocalDatabase.Initialize();
            Global.Log("Wystartowano process.");
        }

        protected override void OnStop()
        {

        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            setupTrigger();
        }

        public void setupTrigger(bool retry = false)
        {
            callback = doit;

            Random random = new Random();
            double randomvalue = random.NextDouble();

            t = new Timer(callback);
            int msToTick = 20 * 1000;

            DateTime now = DateTime.Now;
            DateTime tickTime = DateTime.Today.AddHours(11.0 + randomvalue + randomvalue + randomvalue);

            if (retry)
            {
                //Jeśli jest to ponowienie próby, ustaiamy ticktime na teraz + 20 minut
                //Global.Log("Trigger with retry");
                msToTick = getNextTickIn(DateTime.Now, 60 * 20);
            }
            else if ((now > tickTime) && LocalDatabase.lastTickWasToday())
            {
                //Jeśli dzisiaj już przesłano dane, tick jest przestawiony na jutro
                //Global.Log("now > tickTime and last tick was tooday - trigger time increase by one day");
                tickTime = tickTime.AddDays(1.0);
            }

            else if ((now > tickTime) && LocalDatabase.lastTickWasToday() == false)
            {
                //Global.Log("now > ticTime and last tick wasn't tooday -- srtting trigger now + 20 min");
                msToTick = getNextTickIn(now, 20 * 60);
            }
            else
            {
                //Global.Log("Tic time set to normal time");
                msToTick = getNextTickIn(tickTime, 20);
                msToTick = (int)((tickTime - DateTime.Now).TotalMilliseconds);
                if (msToTick < 10)
                    msToTick = getNextTickIn(tickTime, 20);
            }
            t.Change(msToTick, Timeout.Infinite);
        }

        public static int getNextTickIn(DateTime tickTime, int secounds)
        {
            tickTime = DateTime.Now.AddSeconds(secounds);
            Global.Log("Tick time change to: " + tickTime.ToString(@"M/d/yyyy hh:mm:ss tt"));
            return (int)((tickTime - DateTime.Now).TotalMilliseconds);
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public void doit(Object stateInfo)
        {
            try
            {
                string[] ips = LocalDatabase.getMachinesIps();

                Global.Log("Pobieram: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                LocalDatabase.setToodayTick();

                List<Machine> machines = new List<Machine>();

                foreach (string ip in ips)
                {
                    Machine machine = new Machine(ip);
                    //machine.setUpMachine();
                    machines.Add(machine);
                }

                int fails = Network.DAO.SendMachines(machines);

                if (fails == machines.Count)
                    Global.Log("Nie udało się przesłać jakiejkolwiek maszyny z obecnego odczytu");

                Thread.Sleep(1000 * 20);

                machines = LocalDatabase.getMachinesFromStorage();
                Network.DAO.SendMachines(machines);

                if (fails == machines.Count && machines.Count > 0)
                    Global.Log("Nie udało się przesłać urządzeń z lokalnej bazy danych");
            }
            catch (Exception ex)
            {
                setupTrigger(true);
                Global.Log("Error in main loop. Message: " + ex.Message);
            }
        }
    }
}
