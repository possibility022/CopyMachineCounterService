using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class SposobZglZleceniaMap : ClassMapping<SposobZglZlecenia> {
        
        public SposobZglZleceniaMap() {
			Table("SPOSOB_ZGL_ZLECENIA");
			
			
			Id(x => x.IdSposobZglZlecenia, map => { map.Column("ID_SPOSOB_ZGL_ZLECENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.SposobZgl, map => map.Column("SPOSOB_ZGL"));
        }
    }
}
