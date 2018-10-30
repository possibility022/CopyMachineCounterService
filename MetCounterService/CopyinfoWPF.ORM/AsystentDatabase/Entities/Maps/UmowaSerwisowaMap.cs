using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class UmowaSerwisowaMap : ClassMapping<UmowaSerwisowa> {
        
        public UmowaSerwisowaMap() {
			Table("UMOWA_SERWISOWA");
			Lazy(false);
			Id(x => x.IdUmowaSerwisowa, map => { map.Column("ID_UMOWA_SERWISOWA"); map.Generator(Generators.Assigned); });
			Property(x => x.IdKlient, map => { map.Column("ID_KLIENT"); map.NotNullable(true); });
			Property(x => x.IdUzytkownik, map => { map.Column("ID_UZYTKOWNIK"); map.NotNullable(true); });
			Property(x => x.NrUmowy, map => map.Column("NR_UMOWY"));
			Property(x => x.DataZawarcia, map => map.Column("DATA_ZAWARCIA"));
			Property(x => x.DataRozpoczecia, map => map.Column("DATA_ROZPOCZECIA"));
			Property(x => x.DataZakonczenia, map => map.Column("DATA_ZAKONCZENIA"));
			Property(x => x.RodzajUmowy, map => map.Column("RODZAJ_UMOWY"));
			Property(x => x.Status);
			Property(x => x.CzestotRozlicz, map => map.Column("CZESTOT_ROZLICZ"));
			Property(x => x.DataNastRozlicz, map => map.Column("DATA_NAST_ROZLICZ"));
			Property(x => x.SzczegoloweWarunki, map => map.Column("SZCZEGOLOWE_WARUNKI"));
			Property(x => x.Uwagi);
			Property(x => x.ReprezentatSprz, map => map.Column("REPREZENTAT_SPRZ"));
			Property(x => x.ReprezentantKl, map => map.Column("REPREZENTANT_KL"));
			Property(x => x.Handlowiec);
			//Bag(x => x.UmowaDokumenty, colmap =>  { colmap.Key(x => x.Column("ID_UMOWA_SERWISOWA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.UmowaPliki, colmap =>  { colmap.Key(x => x.Column("ID_UMOWA_SERWISOWA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.UmowaSerwisowaPozycja, colmap =>  { colmap.Key(x => x.Column("ID_UMOWA_SERWISOWA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
