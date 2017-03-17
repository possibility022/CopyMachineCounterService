using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Database
{
    class DatabaseCache
    {
        public static List<int> serviceAgreementDevices { get; private set; }
        public static List<int> serviceAgreementClients { get; private set; }

        public static void Initialize()
        {
            serviceAgreementDevices = Firebird.getServiceAgreementsDevices();
            serviceAgreementClients = Firebird.getServiceAgreementsClients();
        }


    }
}
