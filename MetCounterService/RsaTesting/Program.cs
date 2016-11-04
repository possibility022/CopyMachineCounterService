using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace RsaTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            test1();

            //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            //{
            //    //Export the key information to an RSAParameters object.
            //    //Pass false to export the public key information or pass
            //    //true to export public and private key information.
            //    RSAParameters RSAParams = RSA.ExportParameters(false);

            //    //Create another RSACryptoServiceProvider object.
            //    using (RSACryptoServiceProvider RSA2 = new RSACryptoServiceProvider())
            //    {
            //        //Import the the key information from the other 
            //        //RSACryptoServiceProvider object.  
            //        RSA2.ImportParameters(RSAParams);
            //    }
            //}



            //RSACryptoServiceProvider rsaprovider = new RSACryptoServiceProvider(1024);
            //RSAParameters parameter = new RSAParameters();



            //byte[] buffor = File.ReadAllBytes("y:\\Programowanie\\Projekt\\themodule - n");
            //parameter.Modulus = buffor;
            

            //byte[] b = removeEmptyBytes(ref buffor);
            //parameter.Modulus = b;

            //buffor = File.ReadAllBytes("y:\\Programowanie\\Projekt\\themodule - e");
            //parameter.Exponent = buffor;

            //b = removeEmptyBytes(ref buffor);
            //parameter.Exponent = b;

            //rsaprovider.ImportParameters(parameter);
            //byte[] message = rsaprovider.Decrypt(File.ReadAllBytes("y:\\Programowanie\\Projekt\\encryptedtext"), false);
            //string s = System.Text.Encoding.Unicode.GetString(message);

        }

        public static byte[] removeEmptyBytes(ref byte[] array)
        {
            int lastIndex = array.Length - 1;
            for (int i = array.Length - 1; i > 0; i--)
            {
                if (array[i].Equals(0) == false)
                {
                    lastIndex = i;
                    break;
                } 
            }

            byte[] clearedArray = new byte[lastIndex + 1];
            for (int i = 0; i <= lastIndex; i++)
            {
                clearedArray[i] = array[i];
            }

            return clearedArray;
        }

        public static void test1()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);

            UnicodeEncoding converter = new UnicodeEncoding();
            byte[] message = converter.GetBytes("test wiadomosci");

            byte[] encryptedmessage = rsa.Encrypt(message, true);

            File.WriteAllBytes("y:\\Programowanie\\Projekt\\modulefromcsharp.bytes", rsa.ExportParameters(false).Modulus);
            File.WriteAllBytes("y:\\Programowanie\\Projekt\\exponentfromcsharp.bytes", rsa.ExportParameters(false).Exponent);

            File.WriteAllBytes("y:\\Programowanie\\Projekt\\messagefromcsharp.bytes", encryptedmessage);

            test2(ref rsa);

        }

        //reading message from python encrypted by rsa public key imported from c#
        public static void test2(ref RSACryptoServiceProvider rsa)
        {
            byte[] encryptedmessage = File.ReadAllBytes("y:\\Programowanie\\Projekt\\messagefrompython.bytes");
            rsa.Decrypt(encryptedmessage,)
            string decrypted = UnicodeEncoding.UTF8.GetString(rsa.Decrypt(encryptedmessage, true));

        }


    }
}
