using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace WindowsMetService.Network
{
    class Finder
    {
        public static List<Machine> searchForMachines()
        {
            Finder finder = new Finder();

            List<IPAddress> listOfIps = finder.getAdressesWithResponseInNetworksOfThisPC();
            List<Machine> machineList = new List<Machine>();

            foreach (IPAddress ip in listOfIps)
            {
                string mac = Finder.getMacAddress(ip);

                if (LocalDatabase.macIsMapped(mac))
                {
                    Machine machine = new Machine(ip.ToString());
                    machine.mac = mac;

                    machineList.Add(machine);
                }
            }

            return machineList;
        }

        private List<IPAddress> aviableHosts = new List<IPAddress>();

        private object @lock = new object();

        private void addHostToList(IPAddress obj)
        {
            lock (@lock)
            {
                aviableHosts.Add(obj);
            }
        }

        private Thread[] threads = new Thread[5];

        private int getFreeThreadIndex()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i] != null)
                {
                    if (threads[i].IsAlive == false)
                        return i;
                }
                else
                {
                    threads[i] = new Thread(blank);
                    return i;
                }
            }

            Thread.Sleep(1000);
            return getFreeThreadIndex();
        }

        private bool checkResponse(IPAddress ip)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(ip);

            if (reply.Status == IPStatus.Success)
            {
                addHostToList(ip);
                return true;
            }

            return false;
        }

        public List<IPAddress> getAdressesWithResponse(List<IPAddress> listOfIp)
        {

            foreach (IPAddress ip in listOfIp)
            {
                int index = getFreeThreadIndex();
                threads[index] = new Thread(() => checkResponse(ip));
                threads[index].Start();
            }

            waitForThreads();

            return aviableHosts;
        }

        //public List<IPAddress> getLexmarkMachinesByMac(List<IPAddress> listOfIp)
        //{
        //    List<IPAddress> lexmarkIps = new List<IPAddress>();

        //    foreach (IPAddress ip in listOfIp)
        //    {
        //        if (getMacAddress(ip).ToLower().StartsWith("00-21-B7".ToLower()))
        //            lexmarkIps.Add(ip);
        //    }
        //    return lexmarkIps;
        //}

        public static string getMacAddress(IPAddress ip)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ip.ToString();
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string strOutput = pProcess.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                         + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                         + "-" + substrings[7] + "-"
                         + substrings[8].Substring(0, 2);
                return macAddress;
            }

            else
            {
                return "not found";
            }
        }

        private void blank() { }

        private void waitForThreads()
        {
            foreach (Thread th in threads)
            {
                if (th != null)
                    if (th.IsAlive)
                    {
                        Thread.Sleep(1000);
                        waitForThreads();
                    }
            }
        }

        public List<IPAddress> getAdressesWithResponseInNetworksOfThisPC()
        {
            List<IPAddress> IPs = new List<IPAddress>();
            List<IPAddress> Masks = new List<IPAddress>();

            List<IPAddress> adresses = new List<IPAddress>();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPs.Add(ip);
                }
            }

            foreach (IPAddress ip in IPs)
            {
                Masks.Add(IPV4.getMask(ip));
            }

            if (IPs.Count != Masks.Count) return new List<IPAddress>();

            for (int i = 0; i < IPs.Count; i++)
            {
                List<IPAddress> ls =
                    this.getAdressesWithResponse
                    (IPV4.getAdressesInLan(
                    (IPAddress)IPV4.getLANAdress(IPs[i], Masks[i]),
                    (IPAddress)Masks[i]));

                adresses.AddRange(ls);
            }

            return adresses;
        }

    }
}
