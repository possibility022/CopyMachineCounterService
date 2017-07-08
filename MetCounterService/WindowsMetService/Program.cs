using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WindowsMetService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        static void Main()
        {
#if DEBUG
            Test();

#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MetCounterService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }

        public static void Test()
        {
            LocalDatabase.Initialize();
            Console.WriteLine("Zainicjalizowano bazę danych");
            LocalDatabase.Remove_old_logs();
            string[] ips = LocalDatabase.GetMachinesIps();

            Console.WriteLine("Wczytane adresy ip:");
            foreach (string ip in ips)
                Debug.WriteLine(ip);

            List<Machine> machines = new List<Machine>();

            foreach (string ip in ips)
            {
                Machine machine = new Machine(ip);
                machines.Add(machine);
            }

            Console.WriteLine("Dane znalezionych urządzeń: ");
            foreach(Machine m in machines)
            {
                Debug.WriteLine(m.ip);
                Debug.WriteLine(m.mac);
                Debug.WriteLine(m.url_counterData);
                Debug.WriteLine(m.url_serialNumber);
            }

            int fails = Network.DAO.SendMachines(machines);

            if (fails == machines.Count && machines.Count > 0)
                Global.Log("Nie udało się przesłać jakiejkolwiek maszyny z obecnego odczytu");

            if (fails == 0)
                return;

            Console.WriteLine("Coś poszło nie tak z przesyłaniem. Spróbuję jeszcze raz za 20 sekund");

            Thread.Sleep(1000 * 20);

            machines = LocalDatabase.GetMachinesFromStorage();
            fails = Network.DAO.SendMachines(machines);

            if (fails == machines.Count && machines.Count > 0)
                Global.Log("Nie udało się przesłać urządzeń z lokalnej bazy danych. Liczba: " + fails.ToString());

            Console.WriteLine("Zakończono. Konsola się zamknie za 1 min.");
            Thread.Sleep(1000 * 60);
        }
    }
}
