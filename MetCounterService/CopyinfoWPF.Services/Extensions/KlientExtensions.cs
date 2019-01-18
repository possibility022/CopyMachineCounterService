using CopyinfoWPF.ORM.AsystentDatabase.Entities;

namespace CopyinfoWPF.Services.Extensions
{
    public static class KlientExtensions
    {
        public static string ToShortName(this Klient client) 
            => client == null ? 
                string.Empty : 
                client.NazwaSkr + client.Nazwa1;
    }
}
