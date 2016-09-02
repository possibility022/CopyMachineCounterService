using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace WindowsMetService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {
#if DEBUG

            LocalDatabase.Initialize();

            try
            {
                string[] ips = LocalDatabase.getMachinesIps();

                Global.Log("tick: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                LocalDatabase.setToodayTick();

                List<Machine> machines = new List<Machine>();


                foreach (string ip in ips)
                {
                    Machine machine = new Machine(ip);
                    machines.Add(machine);
                }

                Network.DAO.SendMachines(machines);

                Thread.Sleep(1000 * 20);

                machines = LocalDatabase.getMachinesFromStorage();
                Network.DAO.SendMachines(machines);
            }
            catch (Exception ex)
            {
                Global.Log("Error in main loop. Message: " + ex.Message);
            }

#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MetService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
