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
    class RSAv3
    {
        static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        static byte[] encrypted_parameter_m = Convert.FromBase64String("***REMOVED***");
        static byte[] encrypted_parameter_e = Convert.FromBase64String("***REMOVED***");
        static RSACryptoServiceProvider serverRSA;

        static int slit = 64;

        public static void Initialize()
        {
            serverRSA = new RSACryptoServiceProvider();
            serverRSA.ImportParameters(GetServerParameter());
        }

        #region Decrypting
        /// <summary>
        /// Rozszyfrowuje dane za pomocą klucza prywatnego klienta (lokalny klucz prywatny RSA)
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static public byte[] Decrypt(byte[] bytes)
        {
            //rsa.FromXmlString(File.ReadAllText("localprivatekey.xml")); //TODO usuń tą linijkę
            //Jeśli bajtów jest więcej niż 128 wykorzystuje metode decryptingBigData
            if (bytes.Length > 128)
                return DecryptBigData(bytes);

            //Jeśli danych jest mniej niż 128 bajtów dokonuje dekodowania.
            return rsa.Decrypt(bytes, false);
        }


        /// <summary>
        /// Ta metoda dzieli dane większe niż 128 bajtów na części i deszyfruje. Zwraca pełny ciąg bajtów dowolnej wielkości. Dane wejściowe muszą być wielokrotnością liczby 128.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static private byte[] DecryptBigData(byte[] input)
        {

            //Jeśli dane wejściowe nie są tablicą bajtów której długość jest wielokrotnością liczby 128 to metoda wyrzuci wyjątek.
            //Dlatego że dane zaszyfrowane kluczem RSA 1024 bitowym ze stałym paddingiem będą mieć długość 128 bajtów.
            if (input.Length % 128 != 0)
                throw new Exception("Dekodowanie danych wiekszysch niz 128 bajtów nie udało się. Wielkość tablicy wejściowej nie jest wielokrotnością 128. Długość tablicy: " + input.Length);

            //Dzielenie danych na osobne fragmęty 128 bajtowe do odszyfrowania.
            byte[][] newArrays = new byte[input.Length / 128][];
            // stary kod : int finalArrayLenght = 0;
            for (int i = 0; i < newArrays.Length; i++)
            {
                byte[] toDecrypt = CutBytes(ref input, i*128, (i*128) + 128);
                newArrays[i] = rsa.Decrypt(toDecrypt, false);
                // stary kod : finalArrayLenght += newArrays[i].Length;
            }

            return Combine(newArrays);

            //Stary kod:
            //Kopiowanie bajtów z poprzednio rozszyfrowanych fragmentów do nowej ciągłej tablicy bajtów która następnie jest zwracana.
            //byte[] finalArray = new byte[finalArrayLenght];
            //int loop = 0;
            //foreach(byte[] array in newArrays)
            //{
            //    copyBytes(array, ref finalArray, loop * 64);
            //    loop++;
            //}

            //return finalArray;
        }


        #endregion


        #region Encrypting
        
        /// <summary>
        /// Szyfrowanie danych za pomocą publicznego klucza serwera.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="slitAllowed">Jeśli tablica jest większa niż 127 to ten parametr zezwala na szyfrowanie tych danych. False && 127 < bytes.Lenght </param>
        /// <returns></returns>
        static public byte[] Encrypt(byte[] bytes, bool slitAllowed = true)
        {            
            if ((bytes.Length > slit) && (slitAllowed == false))
                throw new Exception("Podano dane wieksze niż jest to możliwe. Metoda dzielenia danych nie zadziałała prawidłowo.");

            if(bytes.Length > slit)
            {
                return EncryptBigData(bytes);
            }else
            {
                return serverRSA.Encrypt(bytes, false);//TODO sprawdz ta linijke czy jest uzywany klucz serwera
            }

            //byte[] encryptedBytes = serverRSA.Encrypt(bytes, false);
            //return encryptedBytes;
        }

        /// <summary>
        /// Metoda dzieli dane większe niż 128 bajtów. Zwraca tablice której długość jest wielokrotnością liczby 128.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static private byte[] EncryptBigData(byte[] data)
        {
            //okreslenie ilosci tablic
            int arraySize = 0;
            if (data.Length % 64 == 0)
                arraySize = data.Length / 64;
            else
                arraySize = (data.Length / 64) + 1;

            byte[][] newArrays = new byte[arraySize][];

            //wycinanie poszczególnych fragmentów ze źródłowej tablicy
            for(int i = 0; i < arraySize; i++)
            {
                byte[] bytesToEncrypt = CutBytes(ref data, (i * 64), (i * 64) + 64);
                bytesToEncrypt = Encrypt(bytesToEncrypt, false);
                newArrays[i] = bytesToEncrypt;
            }
            //łączenie tablic
            return Combine(newArrays);

            //stary kod tworzenie tablicy finalnej
            //byte[] finallArray = new byte[newArrays.Length * 128];

            //łączenie tablic

            //  Stary kod:
            //int loop = 0;
            //foreach(byte[] array in newArrays)
            //{
            //    copyBytes(array, ref finallArray, loop * 128);
            //    loop++;
            //}

            //return finallArray;
        }

        /// <summary>
        /// Metoda kopiuje wybrany zakres bitow i zwraca je w nowej tablicy.
        /// </summary>
        /// <param name="data">Dane źródłowe. Dane mimo referencji nie są zmieniane</param>
        /// <param name="start">Indeks startowy.</param>
        /// <param name="end">Indeks końcowy</param>
        /// <returns></returns>
        static private byte[] CutBytes(ref byte[] data, int start, int end)
        {
            if (end > data.Length)
                end = data.Length;
            int range = end - start;
            byte[] newArray = new byte[range];

            int newArrayIndex = 0;
            for(int i = start; i < end; i ++)
            {
                newArray[newArrayIndex] = data[i];
                newArrayIndex++;
            }

            return newArray;
        }

        /// <summary>
        /// Metoda kopiuje bajty.
        /// </summary>
        /// <param name="from">Tablica źródłowa.</param>
        /// <param name="to">Tablica do której są zapisywane bajty z tablicy źródłowej.</param>
        /// <param name="indexInNewArray">Przesunięcie w tablicy źródłowej.</param>
        static private void CopyBytes(byte[] from, ref byte[] to, int indexInNewArray)
        {
            int end = 0;
            if (to.Length > from.Length)
                end = from.Length;
            else
                end = to.Length;


            for (int i = 0; i < end; i++)
            {
                to[indexInNewArray + i] = from[i];
            }
        }

        static private byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        #endregion

        #region Other
        /// <summary>
        /// Zwraca parametry M i E klucza publicznego serwera
        /// </summary>
        /// <returns></returns>
        static private RSAParameters GetServerParameter()
        {
            RSAParameters parameter = new RSAParameters();
            parameter.Exponent = Encrypting.Decrypt(encrypted_parameter_e);
            parameter.Modulus = Encrypting.Decrypt(encrypted_parameter_m);

            return parameter;
        }

        /// <summary>
        /// zwraca zaszyfrowany parametr E klucza publicznego klienta |PublicServerKey[PublicClientKeyEParameter]|
        /// </summary>
        /// <returns></returns>
        static public byte[] GetEncryptedClientRsaParameterE()
        {
            return Encrypt(rsa.ExportParameters(false).Exponent);
        }


        /// <summary>
        /// zwraca zaszyfrowany parametr M klucza publicznego klienta |PublicServerKey[PublicClientKeyMParameter]|
        /// </summary>
        /// <returns></returns>
        static public byte[] GetEncryptedClientRsaParameterM()
        {
            return Encrypt(rsa.ExportParameters(false).Modulus);
        }
        #endregion
    }
}
