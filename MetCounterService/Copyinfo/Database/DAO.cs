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

        public static List<Device> getDevices(int idOfClient)
        {
            return FirebirdTB.getDevices(idOfClient);
        }

        internal static List<Client> getAllClients()
        {
            return FirebirdTB.getAllClients();
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

        internal static Client getClient(int clientID)
        {
            return FirebirdTB.getClient(clientID);
        }

        internal static Client getClient(string clientNIP)
        {
            return FirebirdTB.getClient(clientNIP);
        }

        internal static List<MachineRecord> getAllReports()
        {
            return MongoTB.getAllReports();
        }

        internal static void DeleteMachineRecord(MachineRecord additionalItem)
        {
            MongoTB.DeleteMachineRecord(additionalItem);
        }

        internal static EmailData getEmailData(byte[] email_info)
        {
            return MongoTB.getEmailData(email_info);
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
            throw new NotImplementedException("DeleteDevice in DAO");
        }

        internal static List<MachineRecord> getReports(string serial_number)
        {
            return MongoTB.getReports(serial_number);
        }

        internal static MachineRecord GetOneRecord(string serial_number, DateTime month)
        {
            return MongoTB.getOneInMonth(serial_number, month);
        }
    }
}
