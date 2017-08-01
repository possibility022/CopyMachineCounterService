using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsMetService
{
    class Engine
    {

        private static Timer trigger;

        private readonly DateTime STARTDATE = new DateTime(2000, 1, 1);

        private bool retry = false;

        private Timer tickTimer;
        public DateTime tickTime { get; private set; }
        public int StartDelay { get; } = 20 * 1000;

        public Engine()
        {
            Start();
        }

        public void Stop()
        {
            tickTimer?.Dispose();
            trigger?.Dispose();
            tickTime = STARTDATE;
        }

        public void Start()
        {
            tickTime = STARTDATE;
            tickTimer = new Timer(o => CheckEveryMinute(), null, StartDelay, 30000);
            retry = false;
        }

        private void CheckEveryMinute()
        {
            Random random = new Random();
            double randomvalue = random.NextDouble();

            bool dateChanged = true;

            DateTime now = DateTime.Now;

            if (LocalDatabase.ForceRead)
            {
                tickTime = DateTime.Now.AddSeconds(30);
            }
            else if (retry)
            {
                //Jeśli jest to ponowienie próby, ustaiamy ticktime na teraz + 20 minut
                tickTime = DateTime.Now.AddMinutes(20.0);
            }
            else if (LocalDatabase.LastTickWasToday())
            {
                //Jeśli dzisiaj już przesłano dane, tick jest przestawiony na jutro
                tickTime = DateTime.Today.AddDays(1.0).AddHours(10.0 + (randomvalue * 4));
            }
            else if (now > tickTime)
            {
                //Jeśli teraz jest później niż normalny czas przesyłania i dzisiaj nie przesyłano to zrób to za 20 minut.
                tickTime = DateTime.Now.AddMinutes(20.0);
            }
            else
            {
                if (tickTime.Equals(STARTDATE))
                {

                    tickTime = DateTime.Today.AddHours(10.0 + (randomvalue * 4));
                }
                else
                {
                    dateChanged = false;
                }
            }

            if (dateChanged)
            {
                trigger?.Dispose();
                trigger = new Timer(DoIt);
                trigger.Change((int)((tickTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
            }
            
        }

        private void DoIt(object state)
        {
            try
            {
                LocalDatabase.Remove_old_logs();
                string[] ips = LocalDatabase.GetMachinesIps();

                Global.Log("Tick");
                LocalDatabase.SetToodayTick();

                List<Machine> machines = new List<Machine>();

                foreach (string ip in ips)
                {
                    Machine machine = new Machine(ip);
                    machines.Add(machine);
                }

                int fails = Network.DAO.SendMachines(machines);

                if (fails == machines.Count && machines.Count > 0)
                    Global.Log("Nie udało się przesłać jakiejkolwiek maszyny z obecnego odczytu");

                Thread.Sleep(1000 * 20);

                machines = LocalDatabase.GetMachinesFromStorage();
                fails = Network.DAO.SendMachines(machines);

                if (fails == machines.Count && machines.Count > 0)
                    Global.Log("Nie udało się przesłać urządzeń z lokalnej bazy danych");
            }
            catch (Exception ex)
            {
                Global.Log("Error in main loop. Message: " + ex.Message);
                retry = true;
            }
        }
    }
}
