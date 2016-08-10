using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            //LocalDatabase.saveRegistryID();
            //bool s = Network.ServerConnection.downloadMacToWebMapping("test3.xml");
            //bool s2 = Network.ServerConnection.downloadMacToWebMapping("test");
            //Security.RSA rsa = new Security.RSA();

            //byte[] encryptedtext = System.IO.File.ReadAllBytes("V:\\encryptedtext");

            //Security.RSA.RSADecode(encryptedtext);
            //Security.TBRSA.test4();
            //Security.RSAv2.Test();

            //Network.ServerConnection.downloadMacToWebMapping("test");
            byte[] toencode = Encoding.UTF8.GetBytes("aldhjskajwhdk jahwkdj hawkdjhakwdjha jdhkhjwd kajwdh kajwdhk awgdjhagwjdkh gawjhdg jahwgd jhagwj hdgajwh gdjahwgdjahgdwjhadkawhgdka");
            byte[] s = Security.RSAv3.encrypt(toencode);
            byte[] decrypted = Security.RSAv3.decrypt(s);

            string final = Encoding.UTF8.GetString(decrypted);
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
