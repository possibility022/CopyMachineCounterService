using CopyinfoWPF.ORM.AsystentDatabase.Device;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CopyinfoWPF.ORM.AsystentDatabase.Address.Maps
{
    public class ClientDeviceMap : ClassMapping<ClientDevice>
    {

        public ClientDeviceMap()
        {
            Lazy(true);
            Id(x => x.ID_URZADZENIE_KLIENT, map => map.Generator(Generators.Identity));
            Property(x => x.ID_URZADZENIE_KLIENT, map => map.NotNullable(true));
            Property(x => x.ID_MODEL_URZADZENIA, map => map.NotNullable(true));
            Property(x => x.ID_KLIENT, map => map.NotNullable(true));
            Property(x => x.NR_URZADZENIA, map => map.NotNullable(true));
            Property(x => x.ID_MIEJSCE_INSTALACJI, map => map.NotNullable(true));
            Property(x => x.DATA_INSTALACJI, map => map.NotNullable(true));
            Table("URZADZENIE_KLIENT");
        }
    }
}
