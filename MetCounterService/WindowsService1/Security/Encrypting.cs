using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace WindowsMetService.Security
{
    class Encrypting
    {
        public static string Encrypt(string text)
        {
            byte[] encrypted = Encrypt(Encoding.Unicode.GetBytes(text));
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string text)
        {
            byte[] encrypted = Decrypt(Encoding.Unicode.GetBytes(text));
            return Convert.ToBase64String(encrypted);
        }
        
        public static byte[] Encrypt(byte[] data)
        {
            if (data == null) return new byte[] { };

            string EncryptionKey = "***REMOVED***";
            byte[] clearBytes = data; //Encoding.Unicode.GetBytes

            string s = Convert.ToBase64String(new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    data = ms.ToArray();//Convert.ToBase64String
                }
            }
            return data;
        }

        public static byte[] Decrypt(byte[] data)
        {
            if (data == null) return new byte[] { };

            string EncryptionKey = "***REMOVED***";
            byte[] cipherBytes = data;//Convert.FromBase64String
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    data = ms.ToArray(); //Encoding.Unicode.GetString
                }
            }
            return data;
        }
    }
}
