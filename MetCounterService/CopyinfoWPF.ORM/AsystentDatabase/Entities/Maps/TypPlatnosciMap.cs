using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TypPlatnosciMap : ClassMapping<TypPlatnosci> {
        
        public TypPlatnosciMap() {
			Table("TYP_PLATNOSCI");
			
			
			Id(x => x.IdTypPlatnosci, map => { map.Column("ID_TYP_PLATNOSCI"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.IleDni, map => map.Column("ILE_DNI"));
			Property(x => x.Opis);
			Property(x => x.TylkoDoOdczytu, map => map.Column("TYLKO_DO_ODCZYTU"));
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_TYP_PLATNOSCI")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
