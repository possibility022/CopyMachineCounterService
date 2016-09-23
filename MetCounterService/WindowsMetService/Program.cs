using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace WindowsMetService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static public Timer t; // Timer który uruchamia cały process.
        static public Timer t2; // Timer który co X czasu nastawia nowy czas dla timera t.
        static DateTime TICKTIMECURENTLYSET;

        static void Main()
        {
#if DEBUG
            LocalDatabase.Initialize();
            LocalDatabase.remove_old_logs();
            string[] ips = LocalDatabase.getMachinesIps();

            Global.Log("Tick");
            LocalDatabase.setToodayTick();

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

            machines = LocalDatabase.getMachinesFromStorage();
            fails = Network.DAO.SendMachines(machines);

            if (fails == machines.Count && machines.Count > 0)
                Global.Log("Nie udało się przesłać urządzeń z lokalnej bazy danych");
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MetCounterService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }

        static public void doit(Object stateInfo)
        {

        }

        static public void setCurentlyTickTime(DateTime tickTime)
        {
            TICKTIMECURENTLYSET = tickTime;
            if (t != null) t.Dispose();
            t = new Timer(doit);
            t.Change((int)((tickTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
            Global.Log("Ustawiono czas na: " + tickTime.ToString(@"M/d/yyyy hh:mm:ss tt"));
        }


        static public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            int i = 0;
        }
    }
}
