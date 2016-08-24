using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace WindowsMetService.Network
{
    class ServerStream
    {

        private const string disconnectingData = "QUIT-DISCONNECT";

        static private readonly System.Net.IPAddress serverip = new IPAddress(new byte[] { 192, 168, 1, 131 });
        static private readonly IPEndPoint serverReceiverEndPoint = new IPEndPoint(serverip, 9999);

        TcpClient client;
        NetworkStream stream = null;

        private bool connected = false;

        public ServerStream()
        {

        }

        public bool connect()
        {
            if (connected != true)
                connected = do_handshake();
            return connected;
        }

        private bool do_handshake()
        {
            try
            {
                client = new TcpClient(serverReceiverEndPoint.Address.ToString(), serverReceiverEndPoint.Port);
                stream = client.GetStream();
                Handshake handshake = new Handshake();
                return handshake.authorize(ref stream);
            }
            catch(SocketException ex)
            {
                Global.Log("SocketException in ServerStream message: " + ex.Message);
                stream = null;
                return false;
            }
            catch(Exception ex)
            {
                Global.Log("Unnknown error in ServerStream handshake. Message: " + ex.Message);
                return false;
            }
        }

        public bool sendData(byte[] data)
        {
            if (connected == false)
                return false;

            data = Security.RSAv3.encrypt(data, true);

            byte[] dataLenght = BitConverter.GetBytes(data.Length);
            dataLenght = Security.RSAv3.encrypt(dataLenght, false);

            //Wysłanie zaszyfrowanej długości wysyłanych danych.
            try
            {
                stream.Write(dataLenght, 0, 128);
            }
            catch (Exception ex)
            {
                Global.Log("Exception in sending data lenght. Message: " + ex.Message);
                disconnect();
                return false;
            }

            try
            {
                //Wysyłanie zaszyfrowanych danych.
                stream.Write(data, 0, data.Length);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Global.Log("ArgumentOutOfRange - sendByteArray in ServerConnection class - Message: " + ex.Message);
            }
            catch (System.IO.IOException exception)
            {
                Global.Log("IOException - sendByteArray in ServerConnection class - Message: " + exception.Message);
            }
            catch (ObjectDisposedException exception)
            {
                Global.Log("ObjectDisposedException - sendByteArray in ServerConnection class - Message: " + exception.Message);
            }

            return true;
        }

        public void disconnect()
        {
            int step = 0;
            if (connected)
            {
                try
                {
                    sendData(buildStringData(disconnectingData));
                    step = 1;
                    stream.Flush();
                    step = 2;
                    stream.Close();
                    step = 3;
                    client.Close();
                }
                catch (Exception ex)
                { Global.Log("Exception in disconnecting. #Ex step: " + step.ToString() + " Message: " + ex.Message); }
            }
            else
            {
                try
                {
                    if (client.Connected)
                    {
                        step = 4;
                        client.Close();
                    }
                } catch (Exception ex)
                { Global.Log("Exception in disconnecting. #Ex step: " + step.ToString() + " Message: " + ex.Message); }

                try
                {
                    step = 5;
                    stream.Close();
                }
                catch(Exception ex)
                { Global.Log("Exception in disconnecting. #Ex step: " + step.ToString() + " Message: " + ex.Message); }
            }

            connected = true;
        }

        public byte[] buildStringData(string data)
        {
            return getBytes("#|$" + data + "$|#");
        }

        public byte[] buildStringData(string[] data)
        {
            string all = "";

            foreach (string part in data)
                all += "#|$" + part + "$|#";

            return buildStringData(all);
        }

        private static byte[] getBytes(string data)
        {
            byte[] d = System.Text.Encoding.ASCII.GetBytes(data);
            return d;
        }

        
    }
}
