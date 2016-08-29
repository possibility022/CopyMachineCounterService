using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMetService.Network
{
    static class DAO
    {
        static ServerStream connection = new ServerStream();

        static bool SendOneMachine(Machine machine)
        {
            if (connection.connect() == false)
            {
                LocalDatabase.putMachineToStorage(machine);
                return false;
            }

            byte[] bytes = connection.buildStringData(PrepareStringArray(machine));

            return connection.sendMachineData(bytes);
        }


        /// <summary>
        /// W przypadku niepowodzenia: 
        /// Urządzenia są zapisywane lokalnie dla późniejszej próby.
        /// </summary>
        /// <param name="machines"></param>
        /// <returns>0 - Wszystkie dane zostały przekazane prawidlowo. Każda jednostka oznacza jedno urządzenie przesłane nieprawidłowo.</returns>
        public static int SendMachines(List<Machine> machines)
        {
            int fails = 0;
            foreach(Machine m in machines)
            {
                if (SendOneMachine(m) == false)
                    fails++;
            }

            connection.disconnect();

            return fails;
        }

        static string[] PrepareStringArray(Machine machine)
        {
            string[] machineData = new string[4];

            machineData[0] = machine.counterData;
            machineData[1] = machine.serialNumberData;
            machineData[2] = machine.mac;
            machineData[3] = machine.ip;

            return machineData;
        }
    }
}
