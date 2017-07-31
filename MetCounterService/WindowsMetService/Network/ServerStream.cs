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
        private enum Commands
        {
            QUIT_DISCONNECT,
            FULL_DATA_RECEIVED,
            RECEIVE_MACHINE_DATA
        }

        static private IPEndPoint server = null;

        TcpClient client;
        NetworkStream stream = null;

        private bool connected = false;

        public ServerStream()
        {

        }

        public bool Connect()
        {
            server = LocalDatabase.GetServerEndpoint(LocalDatabase.ServerType.Receiver);
            if (connected != true && server != null)
                connected = DoHandshake();
            return connected;
        }

        private bool DoHandshake()
        {
            try
            {
                client = new TcpClient(server.Address.ToString(), server.Port);
                stream = client.GetStream();
                Handshake handshake = new Handshake();
                return handshake.Authorize(ref stream);
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

        private bool SendCommand(Commands com)
        {
            return Send(Security.RSAv3.Encrypt(BuildStringData(com.ToString())));
        }

        public bool SendMachineData(byte[] data)
        {
            if (connected == false)
                return false;

            data = Security.RSAv3.Encrypt(data, true);

            byte[] dataLenght = BitConverter.GetBytes(data.Length);
            dataLenght = Security.RSAv3.Encrypt(dataLenght, false);

            try
            {
                //wysyłanie komendy
                SendCommand(Commands.RECEIVE_MACHINE_DATA);
                //Wysłanie zaszyfrowanej liczby która reprezentuje ilość danych do wysłania.
                stream.Write(dataLenght, 0, 128);
                //Wysyłanie zaszyfrowanych danych.
                Send(data);

                byte[] server_response = new byte[128];
                stream.Read(server_response, 0, server_response.Length);

                if (DebuildStringData(Security.RSAv3.Decrypt(server_response)) != Commands.FULL_DATA_RECEIVED.ToString())
                    return false;

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

        private bool Send(byte[] data)
        {
            if (connected == false)
                return false;

            try
            {
                if (data.Length > 1024)
                {
                    int parts = (int)data.Length / 1024;
                    int rest = data.Length % 1024;

                    for (int i = 0; i < parts; i++)
                        stream.Write(data, i * 1024, 1024);

                    stream.Write(data, 1024 * parts, rest);
                }
                else
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Global.Log("ArgumentOutOfRange - sendByteArray in ServerConnection class - Message: " + ex.Message);
                return false;
            }
            catch (System.IO.IOException exception)
            {
                Global.Log("IOException - sendByteArray in ServerConnection class - Message: " + exception.Message);
                return false;
            }
            catch (ObjectDisposedException exception)
            {
                Global.Log("ObjectDisposedException - sendByteArray in ServerConnection class - Message: " + exception.Message);
                return false;
            }

            return true;
        }

        public void Disconnect()
        {
            int step = 0;
            if (connected && client != null)
            {
                try
                {
                    SendCommand(Commands.QUIT_DISCONNECT);
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

            connected = false;
        }

        public string DebuildStringData(byte[] data)
        {
            string str = UnicodeEncoding.UTF8.GetString(data);
            str = str.Remove(0, 3);
            str = str.Remove(str.Length - 3);
            return str;
        }

        public byte[] BuildStringData(string data)
        {
            //return getBytes("#|$" + data + "$|#");
            return Encoding.UTF8.GetBytes("#|$" + data + "$|#");
        }

        public byte[] BuildStringData(string[] data)
        {
            string all = "";

            foreach (string part in data)
                all += "#|$" + part + "$|#";

            return BuildStringData(all);
        }

        private static byte[] GetBytes(string data)
        {
            byte[] d = System.Text.Encoding.ASCII.GetBytes(data);
            return d;
        }
    }
}
