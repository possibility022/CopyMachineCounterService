using CopyinfoWPF.ORM.AsystentDatabase.Entities;

namespace CopyinfoWPF.Services.Extensions
{
    public static class AdresKlientExtensions
    {
        public static string ToShortAddress(this AdresKlient address)
        {
            if (address != null)
            {
                return $"{address.Ulica} {address.Miejscowosc}";
            }
            else
            {
                return string.Empty;
            }
        }        
    }
}
