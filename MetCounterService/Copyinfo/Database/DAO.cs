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
        public static Address getAddress(int id)
        {
            return FirebirdTB.getAddress(id);
        }

        public static List<Device> getAllDevices()
        {
            return FirebirdTB.GetAllDevices();
        }

        public static List<Device> getDevices(string[] devices)
        {
            return FirebirdTB.getDevices(devices);
        }

        internal static List<Client> getAllClients()
        {
            throw new NotImplementedException();
        }

        internal static void SaveClient(Client client)
        {
            throw new NotImplementedException();
        }

        public static Device getDevice(string serial_number)
        {
            return FirebirdTB.getDevice(serial_number);
        }

        internal static string SaveDevice(Device d)
        {
            throw new NotImplementedException();
        }

        internal static Client getClient(string clientID)
        {
            throw new NotImplementedException();
        }

        internal static List<MachineRecord> getAllReports()
        {
            return MongoTB.getAllReports();
        }

        internal static void DeleteMachineRecord(MachineRecord additionalItem)
        {
            throw new NotImplementedException();
        }

        internal static EmailData getEmailData(byte[] email_info)
        {
            throw new NotImplementedException();
        }

        internal static HTMLCounter getHTMLCounter(ObjectId full_counter)
        {
            throw new NotImplementedException();
        }

        internal static HTMLSerial getHTMLSerial(ObjectId full_serialnumber)
        {
            throw new NotImplementedException();
        }

        internal static bool DeleteDevice(Device additionalItem)
        {
            throw new NotImplementedException();
        }

        internal static List<MachineRecord> getReports(string serial_number)
        {
            return MongoTB.getReports(serial_number);
        }
    }
}
