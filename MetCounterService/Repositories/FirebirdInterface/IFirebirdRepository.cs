using Copyinfo.Model;
using System.Collections;
using System.Collections.Generic;

namespace Repositories.FirebirdInterface
{
    public interface IFirebirdRepository
    {
        void SetConnectionString(string connectionString);

        Address GetAddress();

        Device GetDevice();

        IList<Device> GetDevices(IEnumerable<string> serial_numbers);

        Device GetDevice(string serial_number);

        IList<Device> GetAllDevices();

        IList<Client> GetAllClients();

        Client GetClient(int id);

        Client GetClient(string nip);

        IList<ServiceReport> GetServiceReports(int deviceId);

    }
}
