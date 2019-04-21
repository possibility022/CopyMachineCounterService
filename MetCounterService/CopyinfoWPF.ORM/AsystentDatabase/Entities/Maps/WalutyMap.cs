using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class WalutyMap : ClassMapping<Waluty> {
        
        public WalutyMap() {
			
			
			Id(x => x.IdWaluta, map => { map.Column("ID_WALUTA"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.Opis);
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_WALUTA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
