using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class SymbolePkwiuMap : ClassMapping<SymbolePkwiu> {
        
        public SymbolePkwiuMap() {
			Table("SYMBOLE_PKWIU");
			
			
			Id(x => x.IdSymbolPkwiu, map => { map.Column("ID_SYMBOL_PKWIU"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.Opis);
			//Bag(x => x.Towar, colmap =>  { colmap.Key(x => x.Column("ID_SYMBOL_PKWIU")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
