using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace MetCounterService.Network
{

    class ServerConnection
    {
        static private readonly System.Net.IPAddress serverip = new IPAddress(new byte[] { 127, 0, 0, 1 });
        static private readonly IPEndPoint serverReceiverEndPoint = new IPEndPoint(serverip, 9999);
        static private readonly IPEndPoint serverOfferEndPoint = new IPEndPoint(serverip, 9998);
        TcpClient client;

        private Machine machine = null;

        private bool canCreateNewConnection = true;

        public void sendMachine(Machine machine)
        {
            this.machine = machine;
            try
            {
                if ((client == null) || (canCreateNewConnection))
                {
                    canCreateNewConnection = false;
                    using (client = new TcpClient(serverReceiverEndPoint.Address.ToString(), serverReceiverEndPoint.Port))
                    {
                        NetworkStream stream = null;
                        try
                        {
                            stream = client.GetStream();
                            sendeachline(ref stream, ref machine);
                        }
                        catch (Exception ex)
                        {
                            LocalDatabase.putMachineToStorage(machine);
                            this.machine = null;
                        }
                        finally
                        {
                            if (stream != null)
                            {
                                stream.Flush();
                                byte[] disconnectingdata = buildData("QUIT-DISCONNECT");
                                stream.Write(disconnectingdata, 0, disconnectingdata.Length);
                                stream.Flush();
                                stream.Close();
                            }
                            canCreateNewConnection = true;
                        }
                        client.Close();
                    }

                }
            }catch
            (Exception ex)
            {
                Global.Log("Class: ServerConnection Method: sendMachine Ex.Message:" + ex.Message);
                LocalDatabase.putMachineToStorage(machine);
                this.machine = null;
            }
        }

        private bool sendByteArray(ref NetworkStream stream, byte[] data)
        {
            try
            {
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
                LocalDatabase.putMachineToStorage(this.machine);
            }
            catch (ObjectDisposedException exception)
            {
                Global.Log("ObjectDisposedException - sendByteArray in ServerConnection class - Message: " + exception.Message);
                LocalDatabase.putMachineToStorage(machine);
            }

            return false;
        }

        private void sendeachline(ref NetworkStream stream, ref Machine machine)
        {
            List<byte[]> lines = getLines(ref machine);

            foreach (byte[] array in lines)
            {
                if (sendByteArray(ref stream, array) == false)
                {
                    this.machine = null;
                    break;
                }
            }
        }

        private List<byte[]> getLines(ref Machine machine)
        {
            List<byte[]> lines = new List<byte[]>();

            lines.Add(buildData(machine.counterData));
            lines.Add(buildData(machine.serialNumberData));
            lines.Add(buildData(machine.mac));
            lines.Add(buildData(machine.ip));

            return lines;
        }

        private byte[] buildData(string data)
        {
            byte[] d = System.Text.Encoding.ASCII.GetBytes("#|$" + data + "$|#");
            return d;
        }

        static public bool downloadMacToWebMapping(string path)
        {
            //string path = LocalDatabase.buildPath(LocalDatabase.MacToWebMapping);

            try
            {
                using (FileStream filestream = new FileStream(path, FileMode.Create))
                {
                    using (TcpClient client = new TcpClient(serverOfferEndPoint.Address.ToString(), serverOfferEndPoint.Port))
                    {
                        int readed = 0;
                        byte[] buffor = new byte[1024];
                        NetworkStream networkStream = client.GetStream();
                        do
                        {
                            readed = networkStream.Read(buffor, 0, buffor.Length);
                            if (readed > 0)
                            {
                                filestream.Write(buffor, 0, readed);
                            }
                        } while (readed > 0);
                        networkStream.Close();
                        client.Close();
                    }
                    filestream.Close();
                }
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
    
}
