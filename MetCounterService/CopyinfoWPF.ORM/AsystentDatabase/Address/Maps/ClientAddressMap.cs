using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CopyinfoWPF.ORM.AsystentDatabase.Address.Maps
{
    public class ClientAddressMap : ClassMapping<ClientAddress>
    {

        public ClientAddressMap()
        {
            Lazy(true);
            Id(x => x.ID_ADRES_KLIENT, map => map.Generator(Generators.Identity));
            Property(x => x.ID_ADRES_KLIENT, map => map.NotNullable(true));
            Property(x => x.ID_KLIENT, map => map.NotNullable(true));
            Property(x => x.RODZAJ, map => map.NotNullable(true));
            Table("ADRES_KLIENT");
        }
    }
}
