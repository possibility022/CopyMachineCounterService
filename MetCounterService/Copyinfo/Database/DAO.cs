using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Database
{
    class DAO
    {
        public static Address getAddress(int id)
        {
            return FirebirdTB.getAddress(id);
        }

        public static List<Device> getDevices()
        {
            return FirebirdTB.getDevices();
        }

        public static Device getDevice(string serial_number)
        {
            return FirebirdTB.getDevice(serial_number);
        }
    }
}
