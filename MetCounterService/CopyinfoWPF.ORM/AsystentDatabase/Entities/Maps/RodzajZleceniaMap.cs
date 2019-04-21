using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class RodzajZleceniaMap : ClassMapping<RodzajZlecenia> {
        
        public RodzajZleceniaMap() {
			Table("RODZAJ_ZLECENIA");
			
			
			Id(x => x.IdRodzajZlecenia, map => { map.Column("ID_RODZAJ_ZLECENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Rodzaj);
        }
    }
}
