using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Database.LocalCache
{
    class FirebirdServiceCache
    {
        public static List<int> serviceAgreementDevices { get; private set; }
        public static List<int> serviceAgreementClients { get; private set; }

        public async static Task InitializeAsync()
        {
            await Task.Run(() =>
           {
               serviceAgreementDevices = Firebird.getServiceAgreementsDevices();
               serviceAgreementClients = Firebird.getServiceAgreementsClients();
           });
        }
    }
}
