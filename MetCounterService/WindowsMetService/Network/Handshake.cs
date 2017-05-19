using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsMetService.Security;

namespace WindowsMetService.Network
{
    class Handshake
    {
        public const int handshakekeylenght = 50;
        private const string handshake_ok = "#|$HANDSHAKE_OK$|#";

        public Handshake()
        {

        }

        private byte[] HandshakeResponse(byte[] receivedMessage)
        {
            string key = System.Text.Encoding.UTF8.GetString(receivedMessage);
            char[] sortedkey = Sort(key.ToCharArray());

            return Encoding.ASCII.GetBytes(sortedkey);
        }

        private static char[] Sort(char[] c)
        {
            int elements = c.Length;
            do
            {
                for (int i = 0; i < elements - 1; i++)
                {
                    if (c[i] > c[i + 1])
                    {
                        //swap
                        char buffor = c[i];
                        c[i] = c[i + 1];
                        c[i + 1] = buffor;
                    }
                }
                elements -= 1;
            } while (elements > 1);
            return c;
        }

        public bool Authorize(ref System.Net.Sockets.NetworkStream stream)
        {
            //wysylanie zaszyfrowanego klucza publicznego klienta |ServerPublicKey[ClientPublicKeyM]|
            byte[] buffor = Security.RSAv3.GetEncryptedClientRsaParameterM();
            stream.Write(buffor, 0, buffor.Length);

            System.Threading.Thread.Sleep(100);

            //wysylanie drugiej czesci parametr e
            buffor = Security.RSAv3.GetEncryptedClientRsaParameterE();
            stream.Write(buffor, 0, buffor.Length);

            //odbieranie klucza handshake
            int readed = 0;
            byte[] keyBuffor = new byte[128];
            readed = stream.Read(keyBuffor, 0, keyBuffor.Length);

            keyBuffor = RSAv3.Decrypt(keyBuffor);
            
            //jeśli klucz ma nieprawidłową długość
            if (keyBuffor.Length != Handshake.handshakekeylenght)
                return false;

            //wysylanie odpowiedzi
            keyBuffor = RSAv3.Encrypt(HandshakeResponse(keyBuffor));
            stream.Write(keyBuffor, 0, keyBuffor.Length);

            //wysyłanie klienta ID
            string s = LocalDatabase.GetClientID();
            byte[] id = UnicodeEncoding.UTF8.GetBytes(s);
            id = RSAv3.Encrypt(id);
            stream.Write(id, 0, 128);

            //odbieranie powiadomienia 'ok'
            readed = 0;
            readed = stream.Read(keyBuffor, 0, keyBuffor.Length);

            

            if (readed != 128)
                throw new Exception("Otrzymano zbyt mało danych in Handshake:authorize(NetworkStream)");

            

            bool sucess = 
            UnicodeEncoding.UTF8.GetString(RSAv3.Decrypt(keyBuffor)).EndsWith(handshake_ok);

            Global.Log("Połączono z serwerem");
            return sucess;
        }
    }
}
