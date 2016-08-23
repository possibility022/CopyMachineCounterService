using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace WindowsMetService
{
    static class LocalDatabase
    {
        private static string[] ipsOfCopymachines;

        private const string FolderName = "LicznikMetService";
        private const string FileIps = "ip.cfg";
        private const string MacToWebMapping = "mactoweb.xml";
        private const string LastTickFile = "ticktime.log";
        private const string MachineStorage = "machinestorage.stor";
        private const string Log = "log.log";
        private const string KeyName = "MCservice";


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

        public static void Initialize()
        {
            loadConfig();
            downloadMacToWebXML();
            setupLocalLog();
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
            File.AppendAllLines(buildPath(Log), new string[] { "", DateTime.Today.ToShortDateString() + " " + DateTime.Now.TimeOfDay.ToString() + " Message: " + message });
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
                File.WriteAllLines(buildPath(LastTickFile), new string[] { DateTime.Today.ToShortDateString()  });
            }catch(Exception ex)
            {

            }
        }

        private static void loadConfig()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            try
            {
                ipsOfCopymachines = File.ReadAllLines(buildPath(FileIps));
            }catch
            {
                ipsOfCopymachines = new string[] { };
            }
        }

        private static void downloadMacToWebXML()
        {
            if (Network.ServerOffer.downloadMacToWebMapping(buildPath("mactoweb-new.xml.part")))
                File.Copy(buildPath("mactoweb-new.xml.part"), buildPath(MacToWebMapping), true);
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
            }catch (Exception ex)
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
            catch(Exception ex)
            {
                return new List<Machine>();
            }
        }

        private static byte[] createRegistryID()
        {
            Random rnd = new Random();
            Byte[] b = new Byte[256];
            //TO DO. Autoryzowac klucz. Sprawdzic czy juz taki nie istnieje.
            rnd.NextBytes(b);
            return b;
        }

        public static bool saveRegistryID()
        {
            byte[] b = createRegistryID();
            RegistryKey key = Registry.CurrentUser.CreateSubKey(KeyName);
            byte[] encryptedKey = Security.Encrypting.Encrypt(b);
            key.SetValue(@"Software\AppName\Key", encryptedKey, RegistryValueKind.Binary);

            byte[] readedKey = getRegistryID();

            if (readedKey.Length != b.Length)
                throw new Exception("ID ERROR in saving and reading - wrong lenght");

            for (int i = 0; i < b.Length; i++)
            {
                if (readedKey[i] != b[i])
                    throw new Exception("ID ERROR in saving and reading - diffrent values");
            }

            return true;
        }

        public static byte[] getRegistryID()
        {
            byte[] value = createRegistryID();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyName);
            byte[] Data = (byte[])key.GetValue(@"Software\AppName\Key", value);
            return Security.Encrypting.Decrypt(Data);
        }



    }
}
