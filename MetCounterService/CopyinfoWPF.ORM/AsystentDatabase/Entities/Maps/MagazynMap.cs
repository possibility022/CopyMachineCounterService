using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class MagazynMap : ClassMapping<Magazyn> {
        
        public MagazynMap() {
			
			
			Id(x => x.IdMagazyn, map => { map.Column("ID_MAGAZYN"); map.Generator(Generators.Assigned); });
			Property(x => x.Symbol);
			Property(x => x.Nazwa);
			Property(x => x.Opis);
			Property(x => x.Ulica);
			Property(x => x.NrDomu, map => map.Column("NR_DOMU"));
			Property(x => x.NrLokalu, map => map.Column("NR_LOKALU"));
			Property(x => x.Miejscowosc);
			Property(x => x.Telefon);
			Property(x => x.CzyGlowny, map => map.Column("CZY_GLOWNY"));
			//Bag(x => x.TowarRezerwacja, colmap =>  { colmap.Key(x => x.Column("ID_MAGAZYN")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.TypDokumentu, colmap =>  { colmap.Key(x => x.Column("ID_MAGAZYN")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
