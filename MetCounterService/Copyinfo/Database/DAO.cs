using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Copyinfo.Database
{
    class DAO
    {
        static LocalCache.CacheClients clientCache = new LocalCache.CacheClients();
        static LocalCache.CacheDevices deviceCache = new LocalCache.CacheDevices();

        public static void Initialize()
        {
            clientCache.Preload();
            deviceCache.Preload();
        }

        public static Address GetAddress(int id)
        {
            return Firebird.GetAddress(id);
        }

        public static List<Device> GetAllDevices()
        {
            List<Device> list = Firebird.GetAllDevices();
            deviceCache.UpdateMany(list.ToArray());
            return list;
        }

        public async static Task<List<Device>> GetAllDevicesAsync()
        {
            return await Task.Run(() => GetAllDevices());
        }

        public static List<Device> GetDevices(int clientId, bool useCache = false)
        {
            if (useCache)
                return deviceCache.GetByClientID(clientId);
            else
                return Firebird.GetDevices(clientId);
        }

        internal static List<Client> GetAllClients()
        {
            List<Client> list = Firebird.GetAllClients();
            clientCache.UpdateMany(list.ToArray());
            return list;
        }

        internal static void SaveClient(Client client)
        {
            throw new NotImplementedException();
        }

        public static Device GetDevice(string serial_number, bool useCache = false)
        {
            if (useCache)
            {
                Device dev = deviceCache.GetBySerialNumber(serial_number);
                if (dev == null)
                    return Firebird.GetDevice(serial_number);
                else
                    return dev;
            }
            return Firebird.GetDevice(serial_number);
        }

        internal static string SaveDevice(Device d)
        {
            throw new NotImplementedException();
        }

        internal static Client GetClient(int clientID, bool useCache = false)
        {
            if (useCache)
                if (clientCache.Contains(clientID))
                    return (Client)clientCache.Get(clientID);

            Client c = Firebird.GetClient(clientID);
            clientCache.Set(c.id, c);
            return c;
        }

        internal static Client GetClient(string clientNIP)
        {
            return Firebird.GetClient(clientNIP);
        }

        internal static List<MachineRecord> GetAllReports()
        {
            return MongoTB.GetAllReports();
        }

        internal async static Task<List<MachineRecord>> GetAllReportsAsync()
        {
            return await Task.Run(() => MongoTB.GetAllReports());
        }

        internal static void DeleteMachineRecord(MachineRecord additionalItem)
        {
            MongoTB.DeleteMachineRecord(additionalItem);
        }

        internal static void ReplaceMachineRecord(MachineRecord record)
        {
            MongoTB.ReplaceMachineRecrod(record);
        }

        internal static EmailData GetEmailData(byte[] email_info)
        {
            return MongoTB.GetEmailData(email_info);
        }

        internal static HTMLCounter GetHTMLCounter(ObjectId full_counter)
        {
            return MongoTB.GetHTMLCounter(full_counter);
        }

        internal static HTMLSerial GetHTMLSerial(ObjectId full_serialnumber)
        {
            return MongoTB.GetHTMLSerial(full_serialnumber);
        }

        internal static bool DeleteDevice(Device additionalItem)
        {
            throw new NotImplementedException("DeleteDevice in DAO");
        }

        internal static List<MachineRecord> GetReports(string serial_number)
        {
            return MongoTB.GetReports(serial_number);
        }

        internal static MachineRecord GetFirstInMonth(string serial_number, DateTime month)
        {
            return MongoTB.GetFirstInMonth(serial_number, month);
        }

        internal static List<ServiceReport> GetServiceReport(int deviceId)
        {
            return Firebird.GetServiceReports(deviceId);
        }
    }
}
