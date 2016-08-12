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

    class ServerConnection
    {
        static private readonly System.Net.IPAddress serverip = new IPAddress(new byte[] { 192, 168, 1, 137 });
        static private readonly IPEndPoint serverReceiverEndPoint = new IPEndPoint(serverip, 9999);
        static private readonly IPEndPoint serverOfferEndPoint = new IPEndPoint(serverip, 9998);
        TcpClient client;
        NetworkStream stream = null;
        
        private Machine machine = null;

        public ServerConnection()
        {
            
        }

        private bool do_handshake()
        {
            client = new TcpClient(serverReceiverEndPoint.Address.ToString(), serverReceiverEndPoint.Port);
            stream = client.GetStream();
            Handshake handshake = new Handshake();
            return handshake.authorize(ref stream);
        }

        public bool sendMachine(Machine machine)
        {
            this.machine = machine;
            bool success = true;
            try
            {
                using (client)
                {
                    NetworkStream stream = null;
                    try
                    {
                        Handshake handshake = new Handshake();
                        stream = client.GetStream();
                        if (handshake.authorize(ref stream))
                            success = sendeachline(ref stream, ref machine);
                        else success = false;
                    }
                    catch (Exception ex)
                    {
                        Global.Log("Exception in getting stream: " + ex.Message);
                        success = false;
                        this.machine = null;
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Flush();
                            byte[] disconnectingdata = buildData("QUIT-DISCONNECT");
                            sendByteArray(ref stream, disconnectingdata);
                            //stream.Write(disconnectingdata, 0, disconnectingdata.Length);
                            stream.Flush();
                            stream.Close();
                        }
                    }
                    client.Close();
                }

                
            }catch
            (Exception ex)
            {
                Global.Log("Class: ServerConnection Method: sendMachine Ex.Message:" + ex.Message);
                success = false;
                this.machine = null;
            }

            Global.Log("SendMachine operation result: " + success);
            return success;
        }

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

        private bool sendeachline(ref NetworkStream stream, ref Machine machine)
        {
            byte[] lines = getLines(ref machine);

            sendByteArray(ref stream, lines);
            

            //foreach (byte[] array in lines)
            //{
            //    if (sendByteArray(ref stream, array) == false)
            //    {
            //        this.machine = null;
            //        return false;
            //    }
            //}
            return true;
        }

        private byte[] getLines(ref Machine machine)
        {
            List<byte[]> lines = new List<byte[]>();

            lines.Add(buildData(machine.counterData));
            lines.Add(buildData(machine.serialNumberData));
            lines.Add(buildData(machine.mac));
            lines.Add(buildData(machine.ip));

            return combineArrays(lines);
        }

        private byte[] buildData(string data)
        {
            return getBytes("#|$" + data + "$|#");
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

        static public bool downloadMacToWebMapping(string path)
        {
            //string path = LocalDatabase.buildPath(LocalDatabase.MacToWebMapping);
            int total = 0;
            try
            {
                using (FileStream filestream = new FileStream(path, FileMode.Create))
                {
                    using (TcpClient client = new TcpClient(serverOfferEndPoint.Address.ToString(), serverOfferEndPoint.Port))
                    {
                        byte[] buffor = new byte[1024];
                        NetworkStream networkStream = client.GetStream();

                        //Autoryzacja
                        Handshake handshake = new Handshake();
                        if (handshake.authorize(ref networkStream) == false)
                            return false;

                        System.Threading.Thread.Sleep(500);

                        //Wysylanie komendy pobrania pliku XML
                        sendByteArray(ref networkStream, getBytes("XMLO"));

                        //pobieranie pliku
                        int readed = 0;
                        List<byte[]> receivedData = new List<byte[]>();
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
                        //zamykanie polaczenia
                        networkStream.Close();
                        client.Close();
                        byte[] fileBuffor = combineArrays(receivedData);
                        fileBuffor = Security.RSAv3.decrypt(fileBuffor);
                        filestream.Write(fileBuffor, 0, fileBuffor.Length);
                    }
                    filestream.Close();
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
