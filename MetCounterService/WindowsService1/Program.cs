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

            //LocalDatabase.saveRegistryID();
            //bool s = Network.ServerOffer.downloadMacToWebMapping("test3.xml");

            //bool s2 = Network.ServerOffer.downloadMacToWebMapping("test");

            //File.WriteAllBytes("v:\\encryptedFromC-rsav3.bytes", Security.RSAv3.encrypt(Encoding.UTF8.GetBytes("tteesstt dhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd dhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd dhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd dhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd dhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd")));


            //byte[] mb = System.IO.File.ReadAllBytes("v:\\public-server-key-m");
            //byte[] eb = System.IO.File.ReadAllBytes("v:\\public-server-key-e");

            //mb = Security.RSAv2.clearbytes(mb);
            //eb = Security.RSAv2.clearbytes(eb);

            //mb = Security.Encrypting.Encrypt(mb);
            //eb = Security.Encrypting.Encrypt(eb);

            //string m = Convert.ToBase64String(mb);
            //string e = Convert.ToBase64String(eb);


            //byte[] toencode = Encoding.UTF8.GetBytes("aldhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd kajwdh kajwdhk awgdjhagwjdkh gawjhdg jahwgd jhagwj hdgajwh gdjahwgdjahgdwjhadkawhgdka");
            //byte[] s = Security.RSAv3.encrypt(toencode);

            //System.IO.File.WriteAllBytes("v:\\encrypted.bytes", s);

            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //File.WriteAllText("localprivatekey.xml", rsa.ToXmlString(true));
            //File.WriteAllBytes("v:\\csharp-module.bytes", rsa.ExportParameters(false).Modulus);
            //File.WriteAllBytes("v:\\csharp-expo.bytes", rsa.ExportParameters(false).Exponent);

            //byte[] encrypted = File.ReadAllBytes("v:\\messagefrompython-encryptedusingcpublickey.bytes");

            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(File.ReadAllText("localprivatekey.xml"));
            //byte[] encryptedHere = rsa.Encrypt(Encoding.UTF8.GetBytes("sl"), false);

            ////byte[] decrypted = rsa.Decrypt(encrypted, true);
            ////string decryptedText = Encoding.UTF8.GetString(decrypted);

            //RSAParameters parameters = rsa.ExportParameters(false);
            //rsa = null;
            //rsa = new RSACryptoServiceProvider();
            //rsa.ImportParameters(parameters);



            //string s = Encoding.UTF8.GetString(
            //    Security.RSAv3.decrypt(encrypted
            //        ));


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
