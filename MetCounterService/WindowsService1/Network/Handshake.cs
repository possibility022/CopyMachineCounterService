using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMetService.Network
{
    class Handshake
    {
        public const int handshakekeylenght = 50;

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
            int readed = 0;
            byte[] keyBuffor = new byte[Handshake.handshakekeylenght];
            readed = stream.Read(keyBuffor, 0, keyBuffor.Length);

            if (readed != Handshake.handshakekeylenght)
                return false;
            //wysylanie odpowiedzi
            keyBuffor = handshakeResponse(keyBuffor);

            stream.Write(keyBuffor, 0, keyBuffor.Length);

            return true;
        }
    }
}
