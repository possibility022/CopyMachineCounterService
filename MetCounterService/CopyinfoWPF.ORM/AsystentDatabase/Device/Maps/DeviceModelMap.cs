using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CopyinfoWPF.ORM.AsystentDatabase.Device.Maps
{
    class DeviceModelMap : ClassMapping<DeviceModel>
    {
        public DeviceModelMap()
        {
            Lazy(false);
            Id(x => x.ID_MARKA_URZADZENIA, map => map.Generator(Generators.Identity));
            Property(x => x.ID_MODEL_URZADZENIA, map => map.NotNullable(true));
            Property(x => x.ID_RODZAJ_URZADZENIA, map => map.NotNullable(true));
            Property(x => x.NAZWA_1, map => map.NotNullable(true));
            //Property(x => x.UWAGI); // ignore
            Table("MODEL_URZADZENIA");
        }
    }
}
