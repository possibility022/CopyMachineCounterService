using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Security.Cryptography;


namespace Copyinfo.Security
{
    class Encrypting
    {

        public static string PasswordToDecrypt = "***REMOVED***";
        private const string EncryptedPassword = @"***REMOVED***";

        static private byte[] DecryptedPassword = null;

        //private static string Encrypt(string text, string password)
        //{
        //    byte[] encrypted = AES_Encrypt(Encoding.UTF8.GetBytes(text), Encoding.UTF8.GetBytes(password));
        //    return Convert.ToBase64String(encrypted);
        //}

        //private static string Decrypt(string text, string password)
        //{
        //    byte[] todecrypt = Convert.FromBase64String(text);
        //    byte[] decrypted = AES_Decrypt(todecrypt, Encoding.UTF8.GetBytes(password));
        //    return Encoding.UTF8.GetString(decrypted);
        //}

        public static byte[] AES_Decrypt(byte[] data)
        {
            return AES_Decrypt(data, getPassword());
        }

        public static byte[] AES_Encrypt(byte[] data)
        {
            return AES_Encrypt(data, getPassword());
        }

        public static string AES_Decrypt(string text)
        {
            return Encoding.UTF8.GetString(AES_Decrypt(Convert.FromBase64String(text), getPassword()));
        }

        public static string AES_Encrypt(string text)
        {
            return Convert.ToBase64String(AES_Encrypt(Encoding.UTF8.GetBytes(text), getPassword()));
        }

        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 5, 2, 3, 3, 5, 1, 7, 2 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 5, 2, 3, 3, 5, 1, 7, 2 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static string showPasswordPrompt()
        {
            Forms.FPasswordPrompt prompt = new Forms.FPasswordPrompt();
            prompt.ShowDialog();
            return prompt.tbTextBox1.Text;
        }

        private static byte[] getPassword()
        {
            if (DecryptedPassword == null)
            {
                string passwordTypedByUser = showPasswordPrompt();

                DecryptedPassword = AES_Decrypt(Convert.FromBase64String(EncryptedPassword), Encoding.UTF8.GetBytes(passwordTypedByUser));
            }

            return DecryptedPassword;
        }
    }
}
