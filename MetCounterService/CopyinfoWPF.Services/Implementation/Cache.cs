using CopyinfoWPF.Common.CustomCollections;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using System.Collections.Generic;

namespace CopyinfoWPF.Services.Implementation
{
    public static class Cache
    {
        internal static IConditionalCache<string, UrzadzenieKlient> DeviceCache;
        internal static IConditionalCache<int, AdresKlient> AddressCache;
        internal static IConditionalCache<int, Klient> ClientCache;
        internal static IConditionalCache<int, Pracownik> EmployeeCache;
        internal static HashSet<int> ServiceAgreementCache;

        public static void InitializeCache()
        {
            DeviceCache = new Cache<string, UrzadzenieKlient>();
            AddressCache = new Cache<int, AdresKlient>();
            ClientCache = new Cache<int, Klient>();
            EmployeeCache = new Cache<int, Pracownik>();
            ServiceAgreementCache = new HashSet<int>();
        }
    }
}
