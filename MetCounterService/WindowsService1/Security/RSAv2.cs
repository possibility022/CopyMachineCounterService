using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace WindowsMetService.Security
{
    class RSAv2
    {
        static void DecryptString()
        {
            // Load encrypted string from file
            string encryptedString = Regex.Replace(System.IO.File.ReadAllText("v:\\encryptedmessagev3.base64"), @"\t |\n|\r", "");

            // Load private key from file
            string privateKeyXML = System.IO.File.ReadAllText("privatekey.xml");
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyXML);

            // Decrypt string, convert to correct encoding
            Console.WriteLine("Decrypting...");
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            var decryptedBytes = rsa.Decrypt(encryptedBytes, false);
            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            Console.Write("Decrypted string: ");
            Console.WriteLine(decryptedString);
        }

        static void GenerateKeys()
        {
            // 1024 length encryption
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);

            // Write to files. 
            // This will NOT automatically overwrite the python and C# public/private keys
            System.IO.File.WriteAllText("privatekey.xml", rsa.ToXmlString(true));
            System.IO.File.WriteAllText("publickey.xml", rsa.ToXmlString(false));
        }

        public static void Test()
        {
            // Menu
            //GenerateKeys();
            //DecryptString();
            //exportKeyValues();

            //encryptUsingServerKey();
            encryptKey();
            //testingRSAv3_write();
            test4();

        }

        public static void testingRSAv3_write()
        {
            RSAv3 rsa = new RSAv3();
            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] message = RSAv3.encrypt(Encoding.UTF8.GetBytes("test wiadomosci rsa-v3"));

            File.WriteAllBytes("v:\\rsav3-message.bytes", message);
        }

        public static void exportKeyValues()
        {
            string privateKeyXML = System.IO.File.ReadAllText("privatekey.xml");
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyXML);

            RSAParameters para = rsa.ExportParameters(false);
            UnicodeEncoding encoding = new UnicodeEncoding();

            File.WriteAllBytes("v:\\csharpm.bytes", para.Modulus);
            File.WriteAllBytes("v:\\csharpe.bytes", para.Exponent);
            

        }

        //metoda wczytujaca klucz publiczny serwera, szyfrujaca dane tym kluczem i zapisujaca wiadomosc
        public static void encryptUsingServerKey()
        {
            byte[] module = File.ReadAllBytes("v:\\pythonm.bytes");
            byte[] exponent = File.ReadAllBytes("v:\\pythone.bytes");

            RSAParameters parameter = new RSAParameters();
            parameter.Exponent = clearbytes(exponent); 
            parameter.Modulus = clearbytes(module);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(parameter);

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] message = encoding.GetBytes("CSharp message encrypted using server public key");

            byte[] encrypted = rsa.Encrypt(message, false);
            File.WriteAllBytes("v:\\csharpencrypted.bytes", encrypted);
        }

        public static byte[] clearbytes(byte[] bytes)
        {
            byte empty = bytes[0];

            int start = 0;
            for(int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i].Equals(empty) == false)
                {
                    start = i;
                    break;
                }
            }

            byte[] newArray = new byte[bytes.Length - start];

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = bytes[i + start];
            }

            return newArray;
        }

        private static void encryptKey()
        {
            byte[] module = File.ReadAllBytes("v:\\pythonm.bytes");
            byte[] exponent = File.ReadAllBytes("v:\\pythone.bytes");

            RSAParameters parameter = new RSAParameters();
            parameter.Exponent = clearbytes(exponent);
            parameter.Modulus = clearbytes(module);

            //byte[] encryptedModuls = Encrypting.Encrypt(parameter.Modulus);
            //byte[] encryptedExponent = Encrypting.Encrypt(parameter.Exponent);

            //byte[] decryptedModuls = Encrypting.Decrypt(encryptedModuls);
            //bool decryptingSuccess = decryptedModuls.SequenceEqual(parameter.Modulus); // TRUE metoda szyfrowania i odszyfrowania dziala prawodlowo na kluczu M

            //string encryptedBase64Modul  = Convert.ToBase64String(encryptedModuls);
            //string encryptedBase64Exponent = Convert.ToBase64String(encryptedExponent);

            //bool encryptedModulFromBase64 = encryptedModuls.SequenceEqual(Convert.FromBase64String(encryptedBase64Modul));

            //RSAParameters p = RSAv3.getServerParameter();

            //byte[] m = Encrypting.Decrypt(Convert.FromBase64String(encryptedBase64Modul));
            //byte[] e = Encrypting.Decrypt(Convert.FromBase64String(encryptedBase64Exponent));

            //bool equal_m = parameter.Exponent.SequenceEqual(m);
            //bool equal_e = parameter.Exponent.SequenceEqual(e);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(parameter);

            

            byte[] message = Encoding.UTF8.GetBytes("CSharp message encrypted using server public key - test test test test encryptKey()");

            byte[] encrypted = rsa.Encrypt(message, false);
            File.WriteAllBytes("v:\\rsa-v3.bytes", encrypted);
        }


        private static void test4()
        {
            byte[] encrypted = RSAv3.encrypt(Encoding.UTF8.GetBytes("Test rsav-3 public static methods"));
            File.WriteAllBytes("v:\\csharpencrypted-rsav3.bytes", encrypted);
        }
    }
}
