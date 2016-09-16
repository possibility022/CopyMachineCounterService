﻿using System;
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

    static class ServerOffer
    {
        static private IPEndPoint server = null;

        private enum Commands { XMLO, CLID }

        private static bool sendByteArray(ref NetworkStream stream, byte[] data)
        {
            try
            {
                data = Security.RSAv3.encrypt(data);
                stream.Write(data, 0, data.Length);
                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Global.Log("ArgumentOutOfRange - sendByteArray in ServerConnection class - Message: " + ex.Message);
            }
            catch (System.IO.IOException exception)
            {
                Global.Log("IOException - sendByteArray in ServerConnection class - Message: " + exception.Message);
                //LocalDatabase.putMachineToStorage(this.machine);
            }
            catch (ObjectDisposedException exception)
            {
                Global.Log("ObjectDisposedException - sendByteArray in ServerConnection class - Message: " + exception.Message);
                //LocalDatabase.putMachineToStorage(machine);
            }

            return false;
        }


        private static byte[] getBytes(string data)
        {
            byte[] d = System.Text.Encoding.ASCII.GetBytes(data);
            return d;
        }

        private static byte[] combineArrays(List<byte[]> arrays)
        {
            int sum = 0;
            foreach (byte[] ar in arrays)
                sum += ar.Length;

            byte[] rv = new byte[sum];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="buffer"></param>
        /// <param name="total"></param>
        /// <param name="maxsize">0: unlimited</param>
        /// <returns></returns>
        private static bool sendRequest(Commands command,ref byte[] buffer, ref int total)
        {
            if (server == null)
                server = LocalDatabase.getServerEndpoint(LocalDatabase.ServerType.offer);

            List<byte[]> receivedData = new List<byte[]>();

            using (TcpClient client = new TcpClient(server.Address.ToString(), server.Port))
            {
                total = 0;

                byte[] buffor = new byte[1024];
                NetworkStream networkStream = client.GetStream();

                //Autoryzacja
                Handshake handshake = new Handshake();
                if (handshake.authorize(ref networkStream) == false)
                    return false;

                System.Threading.Thread.Sleep(500);

                //Wysylanie komendy
                if (sendByteArray(ref networkStream, getBytes(command.ToString())) == true)
                {
                    //pobieranie danych
                    int readed = 0;

                    do
                    {
                        readed = networkStream.Read(buffor, 0, buffor.Length);
                        if (readed > 0)
                        {
                            byte[] newBuffor = new byte[readed];
                            System.Buffer.BlockCopy(buffor, 0, newBuffor, 0, readed);
                            receivedData.Add(newBuffor);
                        }
                        total += readed;
                    } while (readed > 0);
                }
                else
                {
                    Global.Log("Nie udalo się wysłać komendy");
                }

                networkStream.Close();                              //Zamykanie połączenia
                client.Close();
            }

            buffer = combineArrays(receivedData); //Łączenie odebranych danych.
            
            return true;
        }

        static public byte[] downloadNewIDForClient()
        {
            byte[] buffor = new byte[] { };
            int total = 0;
            bool sucess = sendRequest(Commands.CLID, ref buffor, ref total);
            if (sucess)
            { 
                buffor = Security.RSAv3.decrypt(buffor);
                return buffor;
            }
            else
            {
                return new byte[] { };
            }
        }

        static public bool downloadMacToWebMapping(string path)
        {
            //string path = LocalDatabase.buildPath(LocalDatabase.MacToWebMapping);
            int total = 0;
            try
            {
                using (FileStream filestream = new FileStream(path, FileMode.Create))
                {
                    byte[] fileBuffor = new byte[] { };
                    bool sucess = sendRequest(Commands.XMLO, ref fileBuffor, ref total);
                    if (sucess)
                    {
                        fileBuffor = Security.RSAv3.decrypt(fileBuffor);    //odszyfrowywanie
                        filestream.Write(fileBuffor, 0, fileBuffor.Length); //zapisywanie do pliku
                        filestream.Close();
                    }
                }
            }catch(Exception ex)
            {
                return false;
            }

            if (total == 0)
                return false;
            return true;
        }

        
    }
    
}