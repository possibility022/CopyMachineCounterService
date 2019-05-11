using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using WindowsMetService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;

namespace UnitTests.WindowsMetServiceTest
{
    [TestClass]
    public class FullUnitTest
    {
        [TestMethod]
        [Timeout(60*1000)]
        [TestCategory("SmokeTest")]
        public void CheckFullSequence()
        {
            LocalDatabase.Initialize();
            Console.WriteLine("Zainicjalizowano bazę danych");
            LocalDatabase.Remove_old_logs();
            IPAddress[] ips = LocalDatabase.GetMachinesIps();

            Console.WriteLine("Wczytane adresy ip:");
            foreach (IPAddress ip in ips)
                Debug.WriteLine(ip);

            List<Machine> machines = new List<Machine>();

            foreach (IPAddress ip in ips)
            {
                Machine machine = new Machine(ip);
                machines.Add(machine);
            }

            Console.WriteLine("Dane znalezionych urządzeń: ");
            foreach (Machine m in machines)
            {
                Debug.WriteLine(m.ip);
                Debug.WriteLine(m.mac);
                Debug.WriteLine(m.url_counterData);
                Debug.WriteLine(m.url_serialNumber);
            }

            int fails = WindowsMetService.Network.DAO.SendMachines(machines);

            if (fails == machines.Count && machines.Count > 0)
                Global.Log("Nie udało się przesłać jakiejkolwiek maszyny z obecnego odczytu");

            if (fails == 0)
                return;

            Console.WriteLine("Coś poszło nie tak z przesyłaniem. Spróbuję jeszcze raz za 20 sekund");

            Thread.Sleep(1000 * 20);

            machines = LocalDatabase.GetMachinesFromStorage();
            fails = WindowsMetService.Network.DAO.SendMachines(machines);

            if (fails == machines.Count && machines.Count > 0)
                Global.Log("Nie udało się przesłać urządzeń z lokalnej bazy danych. Liczba: " + fails.ToString());

            Console.WriteLine("Zakończono. Konsola się zamknie za 1 min.");
            Thread.Sleep(1000 * 60);
        }
    }
}
