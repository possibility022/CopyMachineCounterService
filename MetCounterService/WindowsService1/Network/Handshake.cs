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

        private byte[] handshakeResponse(byte[] receivedMessage)
        {
            string key = System.Text.Encoding.UTF8.GetString(receivedMessage);
            char[] sortedkey = sort(key.ToCharArray());

            return Encoding.ASCII.GetBytes(sortedkey);
        }

        private static char[] sort(char[] c)
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

        public bool authorize(ref System.Net.Sockets.NetworkStream stream)
        {
            //wysylanie zaszyfrowanego klucza publicznego klienta |ServerPublicKey[ClientPublicKeyM]|
            byte[] buffor = Security.RSAv3.getEncryptedClientRsaParameterM();
            stream.Write(buffor, 0, buffor.Length);

            System.Threading.Thread.Sleep(100);

            //wysylanie drugiej czesci parametr e
            buffor = Security.RSAv3.getEncryptedClientRsaParameterE();
            stream.Write(buffor, 0, buffor.Length);

            int readed = 0;
            byte[] keyBuffor = new byte[128];
            readed = stream.Read(keyBuffor, 0, keyBuffor.Length);

            keyBuffor = RSAv3.decrypt(keyBuffor);

            if (keyBuffor.Length != Handshake.handshakekeylenght)
                return false;
            //wysylanie odpowiedzi
            keyBuffor = RSAv3.encrypt(handshakeResponse(keyBuffor));
            stream.Write(keyBuffor, 0, keyBuffor.Length);

            //odbieranie powiadomienia 'ok'
            readed = 0;
            readed = stream.Read(keyBuffor, 0, keyBuffor.Length);

            if (readed != 128)
                throw new Exception("Otrzymano zbyt mało danych in Handshake:authorize(NetworkStream)");

            return
            UnicodeEncoding.UTF8.GetString(RSAv3.decrypt(keyBuffor)).EndsWith(handshake_ok);
        }
    }
}
