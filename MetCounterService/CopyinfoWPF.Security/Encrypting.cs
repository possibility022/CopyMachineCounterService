using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.InteropServices;

namespace CopyinfoWPF.Security
{
    public class Encrypting
    {

        public static string PasswordToDecrypt = "***REMOVED***";
        private const string EncryptedPassword = @"***REMOVED***";

        static private byte[] DecryptedPassword = null;

        public static byte[] AES_Decrypt(byte[] data)
        {
            return AES_Decrypt(data, DecryptedPassword);
        }

        public static byte[] AES_Encrypt(byte[] data)
        {
            return AES_Encrypt(data, DecryptedPassword);
        }

        public static string AES_Decrypt(string text)
        {
            return Encoding.UTF8.GetString(AES_Decrypt(Convert.FromBase64String(text), DecryptedPassword));
        }

        public static string AES_Encrypt(string text)
        {
            return Convert.ToBase64String(AES_Encrypt(Encoding.UTF8.GetBytes(text), DecryptedPassword));
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

        public static bool DecryptPassword(string password)
        {
            try
            {
                if (DecryptedPassword == null)
                {
                    DecryptedPassword = AES_Decrypt(Convert.FromBase64String(EncryptedPassword), Encoding.UTF8.GetBytes(password));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Passes decrypted password String pinned in memory to Func delegate scrubbed on return.
        /// </summary>
        /// <typeparam name="T">Generic type returned by Func delegate</typeparam>
        /// <param name="action">Func delegate which will receive the decrypted password pinned in memory as a String object</param>
        /// <returns>Result of Func delegate</returns>
        public static T DecryptSecureString<T>(SecureString secureString, Func<string, T> action)
        {
            var insecureStringPointer = IntPtr.Zero;
            var insecureString = String.Empty;
            var gcHandler = GCHandle.Alloc(insecureString, GCHandleType.Pinned);

            try
            {
                insecureStringPointer = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                insecureString = Marshal.PtrToStringUni(insecureStringPointer);

                return action(insecureString);
            }
            finally
            {
                insecureString = null;

                gcHandler.Free();
                Marshal.ZeroFreeGlobalAllocUnicode(insecureStringPointer);
            }
        }

        /// <summary>
        /// Runs DecryptSecureString with support for Action to leverage void return type
        /// </summary>
        /// <param name="secureString"></param>
        /// <param name="action"></param>
        public static void DecryptSecureString(SecureString secureString, Action<string> action)
        {
            DecryptSecureString<int>(secureString, (s) =>
            {
                action(s);
                return 0;
            });
        }

        public static byte[] ComputeSha(byte[] bytes)
        {
            using(var sha = SHA256.Create())
            {
                return sha.ComputeHash(bytes);
            }
        }
    }
}
