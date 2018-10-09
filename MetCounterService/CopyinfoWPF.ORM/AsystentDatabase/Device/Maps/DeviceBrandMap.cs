using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CopyinfoWPF.ORM.AsystentDatabase.Device.Maps
{
    class DeviceBrandMap : ClassMapping<DeviceBrand>
    {
        public DeviceBrandMap()
        {
            Id(x => x.ID_MARKA_URZADZENIA, map => map.Generator(Generators.Identity));
            Property(x => x.NAZWA_1, map => map.NotNullable(true));
            Table("MARKA_URZADZENIA");
        }
    }
}
