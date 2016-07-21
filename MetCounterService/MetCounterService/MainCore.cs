using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MetCounterService
{
    class MainCore
    {
        private static DateTime tickTime;

        public static void setupTrigger(bool retry = false)
        {
            Tick tick= new Tick();
            TimerCallback callback = tick.doit;

            Random random = new Random();
            double randomvalue = random.NextDouble();

            Timer t = new Timer(callback);
            int msToTick = 20 * 1000;

            DateTime now = DateTime.Now;
            DateTime tickTime = DateTime.Today.AddHours(8.0 + randomvalue);

            if (retry)
            {
                msToTick = getNextTickIn(20*1000);
            }
            else if ((now > tickTime) && LocalDatabase.lastTickWasToday())
            {
                tickTime = tickTime.AddDays(1.0);
            }
            else
            {
                msToTick = getNextTickIn(20);
                msToTick = (int)((tickTime - DateTime.Now).TotalMilliseconds);
                if (msToTick < 10)
                    msToTick = getNextTickIn(20);
            }

            //int msUntilFour = (int)((tickTime - now).TotalMilliseconds);
            t.Change(msToTick, Timeout.Infinite);
        }

        private static int getNextTickIn(int secounds)
        {
            tickTime = DateTime.Now.AddSeconds(secounds);
            return (int)((tickTime - DateTime.Now).TotalMilliseconds);
        }
    }

    class Tick
    {
        public void doit(Object stateInfo)
        {
            try
            {
                string[] ips = LocalDatabase.getMachinesIps();

                Console.WriteLine("tick: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.TimeOfDay.ToString());

                List<Machine> machines = new List<Machine>();

                Network.ServerConnection server = new Network.ServerConnection();

                foreach (string ip in ips)
                {
                    Machine machine = new Machine(ip);
                    //machine.setUpMachine();
                    machines.Add(machine);
                }

                foreach (Machine m in machines)
                    server.sendMachine(m);

                machines = LocalDatabase.getMachinesFromStorage();
                foreach (Machine m in machines)
                    server.sendMachine(m);

                LocalDatabase.setToodayTick();
                MainCore.setupTrigger(true);
            }
            catch ( Exception ex)
            {
                MainCore.setupTrigger(true);
                Global.Log("Critical Error in main loop. Message: " + ex.Message);
            }
        }
    }

}
