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
        public static Address GetAddress(int id)
        {
            return Firebird.GetAddress(id);
        }

        public static List<Device> GetAllDevices()
        {
            return Firebird.GetAllDevices();
        }

        public static List<Device> GetDevices(int idOfClient)
        {
            return Firebird.GetDevices(idOfClient);
        }

        internal static List<Client> GetAllClients()
        {
            return Firebird.GetAllClients();
        }

        internal static void SaveClient(Client client)
        {
            throw new NotImplementedException();
        }

        public static Device GetDevice(string serial_number)
        {
            return Firebird.GetDevice(serial_number);
        }

        internal static string SaveDevice(Device d)
        {
            throw new NotImplementedException();
        }

        internal static Client GetClient(int clientID)
        {
            return Firebird.GetClient(clientID);
        }

        internal static Client GetClient(string clientNIP)
        {
            return Firebird.GetClient(clientNIP);
        }

        internal static List<MachineRecord> GetAllReports()
        {
            return MongoTB.GetAllReports();
        }

        internal static void DeleteMachineRecord(MachineRecord additionalItem)
        {
            MongoTB.DeleteMachineRecord(additionalItem);
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
    }
}
