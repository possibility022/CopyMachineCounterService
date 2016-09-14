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


            //LocalDatabase.Initialize();
            //Global.Log("Local database initialized");

            Random random = new Random();
            double randomvalue = random.NextDouble();
            DateTime now = DateTime.Now;

            DateTime tickTime
                = DateTime.Today.AddHours(10.0 + (randomvalue * 4));


            if (false)
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
