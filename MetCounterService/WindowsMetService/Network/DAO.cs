using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMetService.Network
{
    static class DAO
    {
        private const string dateFormat = @"dd/MM/yyyy HH:mm";
        static ServerStream connection = new ServerStream();

        enum DataPrefix { CounterData, SerialNumber, MAC, IP, DateTime, Description };

        static Dictionary<DataPrefix, string> dataPrefix = new Dictionary<DataPrefix, string>()
        {
            {DataPrefix.DateTime,           "|DateTime|"},
            {DataPrefix.Description,        "|Description|" },
            {DataPrefix.IP,                 "|AddressIP|" },
            {DataPrefix.MAC,                "|AddressMAC|" },
            {DataPrefix.CounterData,        "|FullCounter|" },
            {DataPrefix.SerialNumber,       "|FullSerialnumber|" }
        };

        /*

            machineData[0] = machine.datetime.ToString(dateFormat); 
            machineData[1] = LocalDatabase.getClientDescription(); 
            machineData[2] = machine.mac;
            machineData[3] = machine.ip;
            machineData[4] = machine.counterData;
            machineData[5] = machine.serialNumberData;

    */

        static bool SendOneMachine(Machine machine)
        {
            if (connection.connect() == false)
            {
                LocalDatabase.putMachineToStorage(machine);
                return false;
            }

            byte[] bytes = connection.buildStringData(CreateStringArray(machine));

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
                {
                    Global.Log("Przesyłanie urządzenia nie powiodło się. Ip: " + m.ip + " Mac: " + m.mac);
                    fails++;
                }else
                {
                    Global.Log("Przesłano urządzenie: " + m.ip + " Mac: " + m.mac);
                }
            }

            connection.disconnect();

            return fails;
        }

        static string[] CreateStringArray(Machine machine)
        {
            string[] machineData = new string[6];

            machineData[0] = dataPrefix[DataPrefix.DateTime] + machine.datetime.ToString(dateFormat);
            machineData[1] = dataPrefix[DataPrefix.Description] + LocalDatabase.getClientDescription();
            machineData[2] = dataPrefix[DataPrefix.MAC] + machine.mac;
            machineData[3] = dataPrefix[DataPrefix.IP] + machine.ip;
            machineData[4] = dataPrefix[DataPrefix.CounterData] + machine.counterData;
            machineData[5] = dataPrefix[DataPrefix.SerialNumber] + machine.serialNumberData;

            return machineData;
        }
    }
}
