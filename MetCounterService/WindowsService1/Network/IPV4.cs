using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WindowsMetService.Network
{
    class IPV4
    {
        static IPAddress[] Masks = new IPAddress[] {
            new IPAddress(new byte[] { 128,0,0,0 }),
            new IPAddress(new byte[] { 192,0,0,0 }),
            new IPAddress(new byte[] { 224,0,0,0 }),
            new IPAddress(new byte[] { 240,0,0,0 }),
            new IPAddress(new byte[] { 248,0,0,0 }),
            new IPAddress(new byte[] { 252,0,0,0 }),
            new IPAddress(new byte[] { 254,0,0,0 }),
            new IPAddress(new byte[] { 255,0,0,0 }),
            new IPAddress(new byte[] { 255,128,0,0 }),
            new IPAddress(new byte[] { 255,192,0,0 }),
            new IPAddress(new byte[] { 255,224,0,0 }),
            new IPAddress(new byte[] { 255,240,0,0 }),
            new IPAddress(new byte[] { 255,248,0,0 }),
            new IPAddress(new byte[] { 255,252,0,0 }),
            new IPAddress(new byte[] { 255,254,0,0 }),
            new IPAddress(new byte[] { 255,255,0,0 }),
            new IPAddress(new byte[] { 255,255,128,0 }),
            new IPAddress(new byte[] { 255,255,192,0 }),
            new IPAddress(new byte[] { 255,255,224,0 }),
            new IPAddress(new byte[] { 255,255,240,0 }),
            new IPAddress(new byte[] { 255,255,248,0 }),
            new IPAddress(new byte[] { 255,255,252,0 }),
            new IPAddress(new byte[] { 255,255,254,0 }),
            new IPAddress(new byte[] { 255,255,255,0 }),
            new IPAddress(new byte[] { 255,255,255,128 }),
            new IPAddress(new byte[] { 255,255,255,192 }),
            new IPAddress(new byte[] { 255,255,255,224 }),
            new IPAddress(new byte[] { 255,255,255,240 }),
            new IPAddress(new byte[] { 255,255,255,248 }),
            new IPAddress(new byte[] { 255,255,255,252 })
                };



        public static IPAddress getLANAdress(IPAddress ip, IPAddress mask)
        {
            byte[] ip_b = ip.GetAddressBytes();
            byte[] mask_b = mask.GetAddressBytes();

            byte[] LAN = new byte[] { 0, 0, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                LAN[i] = (byte)(ip_b[i] & mask_b[i]);
            }
            return new IPAddress(LAN);
        }

        public static IPAddress getBroadcastOfLAN(IPAddress LANAdress, IPAddress mask)
        {
            byte[] ipAdressBytes = LANAdress.GetAddressBytes();
            byte[] subnetMaskBytes = mask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            if (Masks.Contains(mask) == false)
                throw new ArgumentException("Wrong mask in getBroadcastOfLan " + mask.Address.ToString());

            subnetMaskBytes[0] = (byte)~subnetMaskBytes[0];
            subnetMaskBytes[1] = (byte)~subnetMaskBytes[1];
            subnetMaskBytes[2] = (byte)~subnetMaskBytes[2];
            subnetMaskBytes[3] = (byte)~subnetMaskBytes[3];

            for (int i = 0; i < ipAdressBytes.Length; i++)
            {
                ipAdressBytes[i] = (byte)(subnetMaskBytes[i] ^ ipAdressBytes[i]);
            }

            return new IPAddress(ipAdressBytes);

        }

        public static int getMaskLenght(IPAddress mask)
        {
            byte[] mask_b = mask.GetAddressBytes();
            int lenght = 0;

            foreach (byte b in mask_b)
            {
                int count = Convert.ToString(b, 2).ToCharArray().Count(c => c == '1');
                lenght += count;
            }
            return lenght;
        }

        public static List<System.Net.IPAddress> getAdressesInLan(IPAddress LANAdress, IPAddress mask)
        {
            List<System.Net.IPAddress> adresses = new List<IPAddress>();

            // 192.168.1.110
            // A . B . C . D

            IPAddress networkAddress = LANAdress;
            IPAddress broadCast = getBroadcastOfLAN(networkAddress, mask);

            byte[] start = networkAddress.GetAddressBytes();
            byte[] end = broadCast.GetAddressBytes();

            byte[] ip = start;

            while (end.SequenceEqual(ip) == false)
            {
                if (ip[3] == 255)
                    ip[2] += 1;
                if (ip[2] == 255)
                    ip[1] += 1;
                if (ip[1] == 255)
                    if (ip[0] < 255)
                        ip[0] += 1;

                ip[3] += 1;

                adresses.Add(new IPAddress(ip));
            }

            adresses.RemoveAt(adresses.Count - 1);

            return adresses;
        }

        public static IPAddress getMask(IPAddress address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address))
                        {
                            return unicastIPAddressInformation.IPv4Mask;
                        }
                    }
                }
            }
            throw new ArgumentException(string.Format("Can't find subnetmask for IP address '{0}'", address));
        }
    }
}
