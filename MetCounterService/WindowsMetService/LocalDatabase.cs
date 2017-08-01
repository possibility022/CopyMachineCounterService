using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace WindowsMetService
{
    static class LocalDatabase
    {
        private static string[] _ipsOfCopymachines;

        public enum ServerType { Receiver, Offer }

        private const string FolderName = "LicznikMetService";
        private const string File_Ips = "ip.cfg";
        private const string File_MacToWebMapping = "mactoweb.xml";
        private const string File_LastTick = "ticktime.log";
        private const string File_MachineStorage = "machinestorage.stor";
        private const string File_Log = "log.log";
        private const string File_ConfigPath_Json = "config.json";
        private static readonly string WorkFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        private static string _clientDescription = "client description not set";

        private const string ServerAddress = "***REMOVED***";

        public static byte[] ClientId => settings.ClientIddd;

        public static string ClientDescription
        {
            get { return settings.ClientDescription; }
            private set { settings.ClientDescription = value; }
        }
        public static bool ForceRead => settings.ForceRead;

        public static bool SaveLogToSystem => settings.SaveLogsToSystem;

        public const string Version = "2.8";

        private static Settings settings = new Settings()
        {
            ClientDescription = "",
            ForceRead = false,
            ClientIddd = Encoding.UTF8.GetBytes(Settings.EmptyId),
            TickTime = "",
            Version = Version,
            SaveLogsToSystem = false
        };

        private static string BuildPath(string fileName)
        {
            return (Path.Combine(WorkFolder, FolderName, fileName)); // Dla frameworku 4.5
            //return WorkFolder + "//" + FolderName + "//" + file;
        }

        static LocalDatabase()
        {
            _ipsOfCopymachines = new string[] { };
        }

        public static bool Initialize()
        {
            var directory = WorkFolder;
            if (Directory.Exists(Path.Combine(directory, FolderName)) == false)
            {
                Directory.CreateDirectory(Path.Combine(directory, FolderName));
            }

            if (File.Exists(BuildPath(File_ConfigPath_Json)) == false)
                File.Create(BuildPath(File_ConfigPath_Json)).Close();

            if (File.Exists(BuildPath(File_Ips)) == false)
                File.Create(BuildPath(File_Ips)).Close();

            if (File.Exists(BuildPath(File_LastTick)) == false)
                File.Create(BuildPath(File_LastTick)).Close();

            if (File.Exists(BuildPath(File_Log)) == false)
                File.Create(BuildPath(File_Log)).Close();

            if (File.Exists(BuildPath(File_MachineStorage)) == false)
                File.Create(BuildPath(File_MachineStorage)).Close();

            if (File.Exists(BuildPath(File_MacToWebMapping)) == false)
                File.Create(BuildPath(File_MacToWebMapping)).Close();

            Security.RSAv3.Initialize();
            if (LoadSettings() == false)
            {
                if (File.Exists(BuildPath(File_ConfigPath_Json)))
                    return false;
            }
           
            if (settings.ClientIddd.SequenceEqual(Encoding.UTF8.GetBytes(Settings.EmptyId)))
                if (DownloadAndSaveID() == false)
                    return false;
            
            LoadIpsFromFile();
            DownloadMacToWebXML();
            SetupLocalLog();

            return true;
        }

        private static void SetupLocalLog()
        {
            if (File.Exists(BuildPath(File_Log)) == false)
            {
                File.Create(BuildPath(File_Log));
            }
        }

        public static void Log(string message)
        {
#if DEBUG
            string mes = string.Format(DateTime.Today.ToShortDateString() + " " +
                                           DateTime.Now.TimeOfDay.ToString() + " Message: " + message);
            Console.WriteLine(mes);
            Debug.WriteLine(mes);
#else
            File.AppendAllLines(BuildPath(File_Log), new string[] { DateTime.Today.ToShortDateString() + " " + DateTime.Now.TimeOfDay.ToString() + " Message: " + message });
#endif
        }

        public static bool LastTickWasToday()
        {
            try
            {
                string[] lines = File.ReadAllLines(BuildPath(File_LastTick));
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

        public static void SetToodayTick()
        {
            try
            {
                File.Delete(BuildPath(File_LastTick));
                File.WriteAllLines(BuildPath(File_LastTick), new string[] { DateTime.Today.ToShortDateString() });
            }
            catch (Exception ex)
            {

            }
        }

        private static void LoadIpsFromFile()
        {
            string path = BuildPath(File_Ips);
            try
            {
                _ipsOfCopymachines = File.ReadAllLines(BuildPath(File_Ips));
            }
            catch (IOException ex)
            {
                Global.Log("Nie udało się wczytać adresów IP. Message: " + ex.Message);
                if (File.Exists(path) == false)
                    File.Create(path);
                _ipsOfCopymachines = new string[] { };
            }
            catch (Exception ex)
            {
                Global.Log("Nie udało się wczytać adresów IP. Message: " + ex.Message);
                _ipsOfCopymachines = new string[] { };
            }
        }

        private static void DownloadMacToWebXML()
        {
#if DEBUG
            Network.ServerOffer.DownloadMacToWebMapping("debuging-xmlfile.xml");
#else
            if (Network.ServerOffer.DownloadMacToWebMapping(BuildPath("mactoweb-new.xml.part")))
                File.Copy(BuildPath("mactoweb-new.xml.part"), BuildPath(File_MacToWebMapping), true);
#endif
        }

        public static string[] GetMachinesIps()
        {
            LoadIpsFromFile();
            return _ipsOfCopymachines;
        }

        public static string[] GetMacWebMapping(string mac)
        {
            string[] newRow = new string[2];

            string[] links = new string[] { "", "" };

            try
            {
                using (XmlTextReader reader = new XmlTextReader(BuildPath(File_MacToWebMapping)))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Text:
                                if (mac.StartsWith(Regex.Replace(reader.Value, @"\s+", "")))
                                    return links;
                                break;
                            case XmlNodeType.Element:
                                links[0] = reader.GetAttribute("serialnumber");
                                links[1] = reader.GetAttribute("counter");
                                break;
                        }
                    }
                    reader.Close();
                }

                using (XmlTextReader reader = new XmlTextReader(BuildPath(File_MacToWebMapping)))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Text:
                                if (mac.StartsWith(Regex.Replace(reader.Value, @"\s+", "").Substring(0, 8))
                                ) //TUTAJ JEST ZMIANA WZGLEDEM WCZESNIEJSZEGO KODU
                                    return new string[]
                                        {reader.GetAttribute("serialnuber"), reader.GetAttribute("counter")};
                                break;
                            case XmlNodeType.Element:
                                links[0] = reader.GetAttribute("serialnuber");
                                links[1] = reader.GetAttribute("counter");
                                break;
                        }
                    }
                    reader.Close();
                }
                return new string[] {"", ""};
            }
            catch (System.Xml.XmlException xe)
            {
                Global.Log(@"Jakis problem z czytaniem XMLa. Należy wysłać te logi do firmy MET. Message: " +
                           xe.Message);
            }
            return new string[] {"",""};
        }

        public static bool MacIsMapped(string mac)//TODO sprawdz ta metode. Chyba powinno być na odwrót.
        {
            if ((GetMacWebMapping(mac)[0] == "") && (GetMacWebMapping(mac)[1]) == "")
                return false;
            else
                return true;
        }

        public static void PutMachineToStorage(Machine machine)
        {
            List<Machine> list = GetMachinesFromStorage();
            list.Add(machine);
            WriteToBinaryFile(BuildPath(File_MachineStorage), list);
        }

        public static List<Machine> GetMachinesFromStorage()
        {
            List<Machine> list = null;
            try
            {
                list = (List<Machine>)ReadFromBinaryFile(BuildPath(File_MachineStorage));
                File.Delete(BuildPath(File_MachineStorage));
            }catch(FieldAccessException ex)
            {

            }
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

        public static bool DownloadAndSaveID()
        {
            byte[] key = Network.ServerOffer.DownloadNewIDForClient();
            if (key.Length > 0)
            {
                settings.ClientIddd = key;
                SaveSettings();
                
                Global.Log("Pobrano ID");
                return true;
            }
            else
            {
                Global.Log("Nie udało pobrać nowego ID");
                return false;
            }
        }

        //public static string GetClientID()
        //{
        //    return Encoding.UTF8.GetString(Security.Encrypting.Decrypt(settings.ClientId));
        //}

        public static System.Net.IPEndPoint GetServerEndpoint(ServerType type)
        {
            IPAddress ip;
            try
            {
                System.Net.IPAddress[] adresy = System.Net.Dns.GetHostEntry(ServerAddress).AddressList;
                if (adresy.Length < 1)
                {
                    Global.Log("Nie pobrano adresu IP z nazwy hosta: " + ServerAddress);
                    return null;
                }

                ip = adresy[0];
                switch (type)
                {
                    case ServerType.Offer:
                        return new System.Net.IPEndPoint(ip, 9998);
                    case ServerType.Receiver:
                        return new System.Net.IPEndPoint(ip, 9999);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, @"Błąd w implementacji. W instrukcji switch został podany parametr, który nie ma swojego wywołania.");
                }
            }
            catch (Exception ex) { Global.Log("GetServerEndpoint ExMessage: " + ex.Message); }

            Global.Log("Pobieranie IP serwera nie powiodło się.");
            return null;
        }

        public static void Remove_old_logs()
        {
            string[] lines = File.ReadAllLines(BuildPath(File_Log));
            List<string> selected = new List<string>();

            int index = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    DateTime datetime = DateTime.ParseExact(lines[i].Remove(10), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (datetime != null)
                    {
                        DateTime now = DateTime.Now;
                        double diffrent = (now - datetime).TotalDays;
                        if (diffrent > 30.0) index = i;
                    }

                }catch(Exception ex)
                {

                }
            }

            if (index == 0)
                return;

            for(int i = index; i < lines.Length; i++)
            {
                selected.Add(lines[i]);
            }

            File.WriteAllLines(BuildPath(File_Log), selected);
        }

        private static void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            try
            {
                File.WriteAllText(BuildPath(File_ConfigPath_Json), json);
            }
            catch (IOException e)
            {
                Log("Nie można zapisać ustawień. Message" + e.Message);
                throw;
            }
        }

        private static bool LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(BuildPath(File_ConfigPath_Json));
                Settings set = JsonConvert.DeserializeObject<Settings>(json);
                if (set != null)
                {
                    settings = set;
                }
                return true;
            }
            catch (IOException e)
            {
                Log("Nie udało się wczytać ustawień. Message: " + e.Message);
            }

            return false;
        }
    }

    internal class Settings
    {
        public const string EmptyId = "00000000000000000000";


        /// <summary>
        /// Don't EDIT THIS EXTERNAL
        /// </summary>
        public string clientId { get; set; }

        [JsonIgnore]
        public byte[] ClientIddd {
            get
            {
                if (clientId != null)
                    return Convert.FromBase64String(Security.Encrypting.Decrypt(clientId));
                else
                {
                    return Encoding.UTF8.GetBytes(EmptyId);
                }
            } set
            {
                clientId = Security.Encrypting.Encrypt(Convert.ToBase64String(value));
            } }

        public string ClientDescription { get; set; } = null;
        public bool ForceRead { get; set; } = false;
        public string Version { get; set; } = null;
        public string TickTime { get; set; } = null;
        public bool SaveLogsToSystem { get; set; } = false;
    }
}
