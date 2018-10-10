using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class JednostkiMiaryMap : ClassMapping<JednostkiMiary> {
        
        public JednostkiMiaryMap() {
			Table("JEDNOSTKI_MIARY");
			
			
			Id(x => x.IdJednostkaMiary, map => { map.Column("ID_JEDNOSTKA_MIARY"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.Opis);
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_JEDNOSTKA_MIARY")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.Towar, colmap =>  { colmap.Key(x => x.Column("ID_JEDNOSTKA_MIARY")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
