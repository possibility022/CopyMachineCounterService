using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class KlientMap : ClassMapping<Klient> {
        
        public KlientMap() {
			Lazy(false);
			Id(x => x.IdKlient, map => { map.Column("ID_KLIENT"); map.Generator(Generators.Assigned); });
			Property(x => x.IdSerwis, map => { map.Column("ID_SERWIS"); map.NotNullable(true); });
			Property(x => x.IdGrupaKlient, map => { map.Column("ID_GRUPA_KLIENT"); map.NotNullable(true); });
			Property(x => x.IdHandlowiec, map => { map.Column("ID_HANDLOWIEC"); map.NotNullable(true); });
			Property(x => x.Nip);
			Property(x => x.NazwaSkr, map => { map.Column("NAZWA_SKR"); map.NotNullable(true); });
			Property(x => x.Nazwa2, map => map.Column("NAZWA_2"));
            Property(x => x.Opis); // strange type. BLOB-SUB TYPE1
            Property(x => x.KodUzytkownika, map => map.Column("KOD_UZYTKOWNIKA"));
			Property(x => x.Ulica);
			Property(x => x.NrDomu, map => map.Column("NR_DOMU"));
			Property(x => x.NrLokalu, map => map.Column("NR_LOKALU"));
			Property(x => x.Miejscowosc);
			Property(x => x.KodPoczt, map => map.Column("KOD_POCZT"));
			Property(x => x.Poczta);
			Property(x => x.CzyDostawca, map => map.Column("CZY_DOSTAWCA"));
			Property(x => x.CzyOdbiorca, map => map.Column("CZY_ODBIORCA"));
			Property(x => x.IdKlientStatus, map => map.Column("ID_KLIENT_STATUS"));
			Property(x => x.Telefon);
			Property(x => x.Fax);
			Property(x => x.Email);
			Property(x => x.Nazwa1, map => map.Column("NAZWA_1"));
			Property(x => x.Regon);
			Property(x => x.AdresWww, map => map.Column("ADRES_WWW"));
			Property(x => x.IdTypPlatnosci, map => map.Column("ID_TYP_PLATNOSCI"));
			Property(x => x.Wojewodztwo);
			Property(x => x.SposobRozlVat, map => map.Column("SPOSOB_ROZL_VAT"));
			Property(x => x.IdPoziomyCen, map => map.Column("ID_POZIOMY_CEN"));
			//Bag(x => x.AdresKlient, colmap =>  { colmap.Key(x => x.Column("ID_KLIENT")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.OsKontaktKlient, colmap =>  { colmap.Key(x => x.Column("ID_KLIENT")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
