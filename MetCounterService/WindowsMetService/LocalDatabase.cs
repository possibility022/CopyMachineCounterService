using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsMetService
{
    static class LocalDatabase
    {
        private static string[] ipsOfCopymachines;

        public enum ServerType { receiver, offer }

        private const string FolderName = "LicznikMetService";
        private const string FileIps = "ip.cfg";
        private const string MacToWebMapping = "mactoweb.xml";
        private const string LastTickFile = "ticktime.log";
        private const string MachineStorage = "machinestorage.stor";
        private const string Log = "log.log";
        private const string KeyName = "MCservice";
        private const string RegisterKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\" + FolderName;
        private const string ConfigPath = "config.cfg";

        private static byte[] clientID = Security.Encrypting.Encrypt(UnicodeEncoding.UTF8.GetBytes("00000000000000000000"));
        private static string clientDescription = "client description not set";

        private enum ConfigFile { clientID, clientDescription }

        private const string serveraddress = "***REMOVED***";

        public const string Version = "1.0";

        private static string buildPath(string file)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            if (Directory.Exists(Path.Combine(directory, FolderName)) == false)
            {
                Directory.CreateDirectory(Path.Combine(directory, FolderName));
            }
            return (Path.Combine(directory, FolderName, file));
        }

        static LocalDatabase()
        {
            ipsOfCopymachines = new string[] { };
        }

        static string loadConfig(ConfigFile configType)
        {
            try
            {
                StreamReader file = new StreamReader(buildPath(ConfigPath));
                while (file.Peek() > 0)
                {
                    string line = file.ReadLine();
                    if (line.StartsWith(configType.ToString() + ":"))
                        return line.Remove(0, configType.ToString().Length + 1); // to + 1 to jest dwukropek, taki separator który jest dopisywany przy zapisie
                }
                file.Close();

            }
            catch (FileNotFoundException ex)
            { }

            //zwraca "" jesli nie znaleziono
            return "";
        }

        static void saveConfig(ConfigFile configType, string value)
        {
            try
            {
                string configpath = buildPath(ConfigPath);
                if (File.Exists(configpath) == false)
                    File.Create(configpath);

                File.AppendAllLines(configpath, new string[] { configType.ToString() + ":" + value });
            }
            catch (Exception ex)
            { }
        }

        public static string getClientDescription()
        {
            return clientDescription;
        }

        public static void Initialize()
        {
            Global.Log("Localdata initializing start");
            Security.RSAv3.initialize();
            loadCFG_File();
            if (UnicodeEncoding.UTF8.GetString(Security.Encrypting.Decrypt(clientID)) == "00000000000000000000")
                CreateRegistryID();
            loadIpsFromFile();
            downloadMacToWebXML();
            setupLocalLog();
            Global.Log("Localdata initializing done");
        }

        private static void setupLocalLog()
        {
            if (File.Exists(buildPath(Log)) == false)
            {
                File.Create(buildPath(Log));
            }
        }

        public static void log(string message)
        {
#if DEBUG
            Console.WriteLine(new string[] { "", DateTime.Today.ToShortDateString() + " " + DateTime.Now.TimeOfDay.ToString() + " Message: " + message });
#else
            File.AppendAllLines(buildPath(Log), new string[] { DateTime.Today.ToShortDateString() + " " + DateTime.Now.TimeOfDay.ToString() + " Message: " + message });
#endif
        }

        public static bool lastTickWasToday()
        {
            try
            {
                string[] lines = File.ReadAllLines(buildPath(LastTickFile));
                if (lines[lines.Length - 1] == DateTime.Today.ToShortDateString())
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void setToodayTick()
        {
            try
            {
                File.Delete(buildPath(LastTickFile));
                File.WriteAllLines(buildPath(LastTickFile), new string[] { DateTime.Today.ToShortDateString() });
            }
            catch (Exception ex)
            {

            }
        }

        private static void loadIpsFromFile()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            try
            {
                ipsOfCopymachines = File.ReadAllLines(buildPath(FileIps));
            }
            catch
            {
                ipsOfCopymachines = new string[] { };
            }
        }

        private static void loadCFG_File()
        {
            string value = loadConfig(ConfigFile.clientDescription);
            if (value.Length > 0)
                clientDescription = value;

            value = loadConfig(ConfigFile.clientID);
            if (value.Length > 0)
                clientID = Convert.FromBase64String(value);
        }

        private static void downloadMacToWebXML()
        {
#if DEBUG
            Network.ServerOffer.downloadMacToWebMapping("debuging-xmlfile.xml");
#else
            if (Network.ServerOffer.downloadMacToWebMapping(buildPath("mactoweb-new.xml.part")))
                File.Copy(buildPath("mactoweb-new.xml.part"), buildPath(MacToWebMapping), true);
#endif
        }

        public static string[] getMachinesIps()
        {
            return ipsOfCopymachines;
        }

        public static string[] getMacWebMapping(string mac)
        {
            string[] newRow = new string[2];

            string[] links = new string[] { "", "" };

            using (XmlTextReader reader = new XmlTextReader(buildPath(MacToWebMapping)))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text:
                            if (mac.StartsWith(reader.Value))
                                return links;
                            break;
                        case XmlNodeType.Element:
                            links[0] = reader.GetAttribute("serialnuber");
                            links[1] = reader.GetAttribute("counter");
                            break;
                    }
                }
                reader.Close();
            }

            using (XmlTextReader reader = new XmlTextReader(buildPath(MacToWebMapping)))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text:
                            if (mac.StartsWith(reader.Value.Substring(0, 8))) //TUTAJ JEST ZMIANA WZGLEDEM WCZESNIEJSZEGO KODU
                                return new string[] { reader.GetAttribute("serialnuber"), reader.GetAttribute("counter") };
                            break;
                        case XmlNodeType.Element:
                            links[0] = reader.GetAttribute("serialnuber");
                            links[1] = reader.GetAttribute("counter");
                            break;
                    }
                }
                reader.Close();
            }
            return new string[] { "", "" };
        }

        public static bool macIsMapped(string mac)
        {
            if ((getMacWebMapping(mac)[0] == "") && (getMacWebMapping(mac)[1]) == "")
                return true;
            else
                return false;
        }

        public static void putMachineToStorage(Machine machine)
        {
            List<Machine> list = getMachinesFromStorage();
            list.Add(machine);
            WriteToBinaryFile(buildPath(MachineStorage), list);
        }

        public static List<Machine> getMachinesFromStorage()
        {
            List<Machine> list = (List<Machine>)ReadFromBinaryFile(buildPath(MachineStorage));
            File.Delete(buildPath(MachineStorage));
            return list;
        }

        private static void WriteToBinaryFile(string filePath, object obj)
        {
            try
            {
                byte[] objectBytes = null;

                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    objectBytes = ms.ToArray();
                }

                File.WriteAllBytes(filePath, objectBytes);
            }
            catch (Exception ex)
            {

            }
        }

        private static object ReadFromBinaryFile(string filePath)
        {
            try
            {
                byte[] filebytes = File.ReadAllBytes(filePath);
                using (var memStream = new MemoryStream())
                {
                    var binForm = new BinaryFormatter();
                    memStream.Write(filebytes, 0, filebytes.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    var obj = binForm.Deserialize(memStream);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                return new List<Machine>();
            }
        }

        public static bool CreateRegistryID()
        {
            byte[] key = Network.ServerOffer.downloadNewIDForClient(); //Pobieramy ID #1
            if (key.Length > 0)
            {
                byte[] encrypted = Security.Encrypting.Encrypt(
                                            key);

                saveConfig(ConfigFile.clientID, Convert.ToBase64String(encrypted));
                clientID = encrypted;
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string getRegistryID()
        {
            return UnicodeEncoding.UTF8.GetString(Security.Encrypting.Decrypt(clientID));
        }

        public static System.Net.IPEndPoint getServerEndpoint(ServerType type)
        {
            System.Net.IPAddress ip = null;
            try
            {
                System.Net.IPAddress[] adresy = System.Net.Dns.GetHostEntry(serveraddress).AddressList;
                ip = adresy[0];
                if (type == ServerType.offer)
                    return new System.Net.IPEndPoint(ip, 9998);
                if (type == ServerType.receiver)
                    return new System.Net.IPEndPoint(ip, 9999);
            }
            catch (Exception ex) { }
            return null;

        }
    }
}
