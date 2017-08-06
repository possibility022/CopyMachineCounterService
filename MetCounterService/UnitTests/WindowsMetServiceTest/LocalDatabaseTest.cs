using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WindowsMetService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.WindowsMetServiceTest
{
    [TestClass]
    public class LocalDatabaseTest
    {
        [TestMethod]
        public void CheckCommentInIpConfigFiles()
        {
            IPAddress[] ips = LocalDatabase.GetMachinesIps();

        }

        [TestMethod]
        public void CheckMachineStorage()
        {
            LocalDatabase.Initialize();
            File.Delete("C:\\ProgramData\\LicznikMetService\\machinestorage.stor");
            List<Machine> machines = LocalDatabase.GetMachinesFromStorage();

            Assert.IsTrue(machines.Count == 0);

            machines = new List<Machine>();

            for (int i = 0; i < 10; i++)
            {
                IPAddress add = IPAddress.Parse("192.168.1." + i.ToString());
                machines.Add(new Machine(add));
            }


            foreach (Machine machine in machines)
            {
                LocalDatabase.PutMachineToStorage(machine);
                List<Machine> fromStorage = LocalDatabase.GetMachinesFromStorage();

                Assert.IsTrue(fromStorage.Last().ip.Equals(machine.ip));
            }

            List<Machine> machinesFromStorage = LocalDatabase.GetMachinesFromStorage();


            for (var index = 0; index < machinesFromStorage.Count; index++)
            {
                Machine storageMachine = machinesFromStorage[index];
                Machine machine = machines[index];

                Assert.IsTrue(storageMachine.ip == machine.ip);
            }
        }
    }
}
