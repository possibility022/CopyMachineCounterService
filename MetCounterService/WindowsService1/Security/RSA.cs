using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsMetService.Security
{
    class TBRSA
    {
        public TBRSA()
        {
            //lets take a new CSP with a new 2048 bit rsa key pair
            var csp = new RSACryptoServiceProvider(2048);

            //how to get the private key
            var privKey = csp.ExportParameters(true);

            //and the public key ...
            var pubKey = csp.ExportParameters(false);

            //converting the public key into a string representation
            string pubKeyString;
            {
                //we need some buffer
                var sw = new System.IO.StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, pubKey);
                //get the string from the stream
                pubKeyString = sw.ToString();
            }

            //converting it back
            {
                //get a stream from the string
                var sr = new System.IO.StringReader(pubKeyString);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                pubKey = (RSAParameters)xs.Deserialize(sr);
            }

            //conversion for the private key is no black magic either ... omitted

            //we have a public key ... let's get a new csp and load that key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);

            //we need some data to encrypt
            var plainTextData = "foobar";

            //for encryption, always handle bytes...
            var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

            //we might want a string representation of our cypher text... base64 will do
            var cypherText = Convert.ToBase64String(bytesCypherText);


            /*
             * some transmission / storage / retrieval
             * 
             * and we want to decrypt our cypherText
             */

            //first, get our bytes back from the base64 string ...
            bytesCypherText = Convert.FromBase64String(cypherText);

            //we want to decrypt, therefore we need a csp and load our private key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

            //get our original plainText back...
            plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
        }

        public static void test1()
        {

            string privatekey = @"MIICXQIBAAKBgQCKdEhWmIunJgme/SdI+epiNkVPmN0WW/YujomntM695nJMZ8Mh
moNqZqVyLDebHiJDBVm550aQJSF/j+rXhR0vMYt0bwVcElVP4S/P+hvBK1CQHeVo
jOYQlHVKrMcsp1k2DX+wiNfJ1XUxrb4KTSViiBz2KPorn4dQO0Fv6nWrMQIDAQAB
AoGAaR6MZaMANPH+UAXos9F7kQGfciWfhoBf001JWlk+tpOmqDgHwRWtPTWd26eS
uGUSokwHqcvcmUh3vIAqT2Ozmalkh1B1GZ0j5vAAnpP4rG0UEuNYNF2I9O7hWIkv
L7CqD/dmkg+Q9SSIwiSC1SQjQLRhq24ElNeClzvhl6ohRzkCQQC8Jdj+5dheJIRP
OjevTCDAmIQyzcYkVDAPBgRGZiLuz4hwy321U0mEzO6CEhE6B6zogmi6tDfM8VQ+
qesQPjj3AkEAvGKhOd+Fle5IESTCpv2uhoT32I4FI/X+kXuLH26q3sam/P48h2DK
vmtKtNxWOSfKBQyImJ2gnmjBvnUxy2ebFwJBAJlZn8viOsBAszaAFPLj8a4IoXdn
EKB7ndAg70sR5FcQ//wvX35zK/D5t0x8vV22889uuz2xtelsqWWJyPWsQXECQQCP
F4vHXdUmUVxESVvhZAfQ/ecUgqu86Pl4oBLIyVLY7KOiv6pSWl8lzHFud36241Y8
B40p/3ElYgToGjS8f/H5AkACv+vZIUmOLWMQIwH96KM333tn4bI/2JWdvzfiTbNf
xlPOMwqVpAf18F6Yl6rS1x2bVssV6rXHYn2YmdAqS/gi";

            string publickey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDKw3E8VOtgKM5UbmKsTiibcyZe
x5P38uYk9RPnFdfrhPgM0S61GiS8eHLtkjt7NHzzA4n/dq8CRmhF7vd3bGWEx8R5
BQ3ViL6IRQxVmCMWijvQOhuzFT9vjgDY4476vZwBNw3xxLkLgGL9BT1rsGayP+I8
ljDGIO1OUg5QGLYjswIDAQAB";



            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(publickey));


            //byte[] s = rsa.Encrypt(BitConverter.GetBytes(., false);
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            
            byte[] dataToEncrypt = ByteConverter.GetBytes("TESTOWA WIADOMOSC");
            byte[] encryptedData;
            byte[] decryptedData;


            RSAParameters para = rsa.ExportParameters(false);

            encryptedData = RSAEncrypt(dataToEncrypt, rsa.ExportParameters(false), true);
            

            File.WriteAllBytes("V:\\encryptedmessage csharp", encryptedData);
                        
            //string s = rsa.ToXmlString(true);
        }

        public static void test2()
        {
            string publickey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDKw3E8VOtgKM5UbmKsTiibcyZe
x5P38uYk9RPnFdfrhPgM0S61GiS8eHLtkjt7NHzzA4n/dq8CRmhF7vd3bGWEx8R5
BQ3ViL6IRQxVmCMWijvQOhuzFT9vjgDY4476vZwBNw3xxLkLgGL9BT1rsGayP+I8
ljDGIO1OUg5QGLYjswIDAQAB";

            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(publickey));

            RSAParameters par =  rsa.ExportParameters(false);

            byte[] buffor = File.ReadAllBytes("V:\\encryptedtext");
            byte[] decrypted = rsa.Decrypt(buffor, true);

            string message = Encoding.UTF8.GetString(decrypted);
            
        }

        public static void test3()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string s = "<RSAKeyValue><Modulus>t0GOW75BsnFAQHIJ8XW8x2fwrFcRA8IQpLWcs2n1m+kjmSsLlDDVucmcG+nZdzPWyAo3NJSeea9CBfumzL6TOLtll0SWcMAi00jSLdj4USdhcs1ge0fMGfAc/PZIFXtITj1UhYTNkHQ8yTlnIaPGnzAz2+iIeKNhn9ulg5ulqIk=</Modulus><Exponent>AQAB</Exponent><P>1uP8pGZ44r3eX6ZhJsroTvNK9Shwbfsp3TBAPgW9eHf394TkX7pl681cQGJt1zaW2tXg2Idtz8lDsjLCiA5j3w==</P><Q>2lBOl7CC5wU7VbOz2CJONsk08m/bsRKqx/VFEOTAlC+7Gpic9ACz1nCP3YPEOYxLTXYLYWafp7/kWYB2G+9Alw==</Q><DP>b5rG2sdRn0lHFdw+drxJPL/EvWC8S5J3YqYtp0ip58g+47GEPmd7iaUFdXbpXy292XfdJ/fmQ1VfMEyGlc0Ldw==</DP><DQ>bBUiM9tBk5p1e8KSIUkqq9kFi5lxjMMPJhv565k09qiC23H/EpZecqYbs7GOrmcUyO7OR5SFKonIRb9hugbDVQ==</DQ><InverseQ>ZrCj0GliPY3e7Lll0keCTYoaNaHbbHg3eEB9LafedTzzK7jbJx0PhvwWfLMrncvdKLlrSbmTaibCvX9SdmQbzQ==</InverseQ><D>WjAfvSQB1i2pVu4o2ZY9rY+IYXDKFWTlZwJgV+YOc8hF9kQ6gxiTnsdLlms81M8E5dctOynnYnCv2Bz1Bq/F6OERyN8nxyrXio+6m9R3gYhLEyWmIxt0pMqTxuXuCDBwR115yozSDutjyHKYPo/GV7oz2gA+y/68np6PGnkOzc8=</D></RSAKeyValue>";
            rsa.FromXmlString(s);

            File.WriteAllText("V:\\csharpmodules", UnicodeEncoding.UTF8.GetString(rsa.ExportParameters(false).Modulus));
            File.WriteAllText("V:\\csharpexpo", UnicodeEncoding.UTF8.GetString(rsa.ExportParameters(false).Exponent));


        }

        public static void test4()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);

            UnicodeEncoding converter = new UnicodeEncoding();
            byte[] message = converter.GetBytes("test wiadomosci");

            byte[] encryptedmessage = rsa.Encrypt(message, false);

            File.WriteAllBytes("v:\\modulefromcsharp.bytes", rsa.ExportParameters(false).Modulus);
            File.WriteAllBytes("v:\\exponentfromcsharp.bytes", rsa.ExportParameters(false).Exponent);

            File.WriteAllBytes("v:\\messagefromcsharp.bytes", encryptedmessage);

            test5(ref rsa);
        }

        public static void test5(ref RSACryptoServiceProvider rsa)
        {
            string encryptedString = Regex.Replace(System.IO.File.ReadAllText("v:\\encryptedpythonmessage.bytes"), @"\t|\n|\r", "");
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);

            //byte[] encryptedBytes = File.ReadAllBytes("v:\\encryptedpythonmessage.bytes");
            var decryptedBytes = rsa.Decrypt(encryptedBytes, false);
            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        public static string RSADecode(string input)
        {
            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(GetKey()));

            return (Convert.ToBase64String(rsa.Encrypt(Encoding.ASCII.GetBytes(input), false)));
        }

        public static string RSADecode(byte[] input)
        {
            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(GetKey()));

            return (Convert.ToBase64String(rsa.Decrypt(input, false)));
        }

        static string GetKey()
        {
            return File.ReadAllText("V:\\public.peb").Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "");
            //.Replace("\n", "");
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        public static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(x509key);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
    }
}
