using CopyinfoWPF.ORM.AsystentDatabase.Entities;

namespace CopyinfoWPF.Helpers
{
    public static class AddressHelpers
    {
        public static string AddressHouseNumberToString(AdresKlient address)
        {
            return $"{address.NrDomu}" + (string.IsNullOrWhiteSpace(address.NrLokalu) ? string.Empty : $"\\{address.NrLokalu}");
        }
    }
}
