using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class RodzajDokumentuMap : ClassMapping<RodzajDokumentu> {
        
        public RodzajDokumentuMap() {
			Table("RODZAJ_DOKUMENTU");
			
			
			Id(x => x.IdRodzajDokumentu, map => { map.Column("ID_RODZAJ_DOKUMENTU"); map.Generator(Generators.Assigned); });
			Property(x => x.Rodzaj);
			//Bag(x => x.TypDokumentu, colmap =>  { colmap.Key(x => x.Column("ID_RODZAJ_DOKUMENTU")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
