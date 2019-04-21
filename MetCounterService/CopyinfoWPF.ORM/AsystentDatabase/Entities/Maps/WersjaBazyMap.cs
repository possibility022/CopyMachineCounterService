using NHibernate.Mapping.ByCode.Conformist;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class WersjaBazyMap : ClassMapping<WersjaBazy> {
        
        public WersjaBazyMap() {
			Table("WERSJA_BAZY");
			
			
			Property(x => x.Nr);
			Property(x => x.Data, map => map.NotNullable(true));
			Property(x => x.Czas, map => map.NotNullable(true));
        }
    }
}
