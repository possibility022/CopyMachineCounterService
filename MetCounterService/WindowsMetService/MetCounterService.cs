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
        public TimerCallback callback; //callback dla timera t
        public Timer t; // Timer który uruchamia cały process.
        public Timer t2; // Timer który co X czasu nastawia nowy czas dla timera t.
        DateTime TICKTIMECURENTLYSET;
        bool ToodayTickSet = false;

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
            Global.SetSystemEventLog(eventLog1);
            //LocalDatabase.Initialize();

            //Global.Log("Utworzono logi w konstruktorze");
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
            timer.Interval = 60000 * 20; // 60 seconds * 20 = 20 min
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
            t.Dispose();
            
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

            DateTime now = DateTime.Now;

            DateTime tickTime
                = DateTime.Today.AddHours(10.0 + (randomvalue * 4));


            if (retry)
            {
                //Jeśli jest to ponowienie próby, ustaiamy ticktime na teraz + 20 minut
                tickTime = DateTime.Now.AddMinutes(20.0);
            }
            else if (LocalDatabase.lastTickWasToday())
            {
                //Jeśli dzisiaj już przesłano dane, tick jest przestawiony na jutro
                tickTime = DateTime.Today.AddDays(1.0).AddHours(10.0 + (randomvalue * 4));
            }
            else if ((now > tickTime))
            {
                //Jeśli teraz jest później niż normalny czas przesyłania i dzisiaj nie przesyłano to zrób to za 20 minut.
                tickTime = DateTime.Now.AddMinutes(20.0);
            }

            if (TICKTIMECURENTLYSET != null)
            {
                //Jeśli ustawiony czas już miną, ustaw nową wartość.
                if (TICKTIMECURENTLYSET < DateTime.Now)
                    setCurentlyTickTime(tickTime);
            }
            else
            {
                setCurentlyTickTime(tickTime);
            }
        }

        public void setCurentlyTickTime(DateTime tickTime)
        {
            TICKTIMECURENTLYSET = tickTime;
            t.Change((int)((tickTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
            Global.Log("Ustawiono czas na: " + tickTime.ToString(@"M/d/yyyy hh:mm:ss tt"));
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public void doit(Object stateInfo)
        {
            try
            {
                string[] ips = LocalDatabase.getMachinesIps();

                Global.Log("Tick");
                LocalDatabase.setToodayTick();

                List<Machine> machines = new List<Machine>();

                foreach (string ip in ips)
                {
                    Machine machine = new Machine(ip);
                    //machine.setUpMachine();
                    machines.Add(machine);
                }

                int fails = Network.DAO.SendMachines(machines);

                if (fails == machines.Count && machines.Count > 0)
                    Global.Log("Nie udało się przesłać jakiejkolwiek maszyny z obecnego odczytu");

                Thread.Sleep(1000 * 20);

                machines = LocalDatabase.getMachinesFromStorage();
                fails = Network.DAO.SendMachines(machines);

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
