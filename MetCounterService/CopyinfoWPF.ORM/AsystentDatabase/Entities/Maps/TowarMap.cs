using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TowarMap : ClassMapping<Towar> {
        
        public TowarMap() {
			
			
			Id(x => x.IdTowar, map => { map.Column("ID_TOWAR"); map.Generator(Generators.Assigned); });
			Property(x => x.SymbolOem, map => map.Column("SYMBOL_OEM"));
			Property(x => x.WydajnoscMatEkspl, map => map.Column("WYDAJNOSC_MAT_EKSPL"));
			Property(x => x.Nazwa2, map => map.Column("NAZWA_2"));
			Property(x => x.IdSerwis, map => map.Column("ID_SERWIS"));
			Property(x => x.Netto);
			Property(x => x.Brutto);
			Property(x => x.Ilosc);
			Property(x => x.Typ);
			Property(x => x.Uwagi1, map => map.Column("UWAGI_1"));
			Property(x => x.Uwagi2, map => map.Column("UWAGI_2"));
			Property(x => x.Kod);
			Property(x => x.Identyfikator);
			Property(x => x.BruttoDostawcy, map => map.Column("BRUTTO_DOSTAWCY"));
			Property(x => x.NettoDostawcy, map => map.Column("NETTO_DOSTAWCY"));
			Property(x => x.ZaokraglajDo, map => map.Column("ZAOKRAGLAJ_DO"));
			Property(x => x.Marza);
			Property(x => x.Narzut);
			Property(x => x.MarzaCzyNarzut, map => map.Column("MARZA_CZY_NARZUT"));
			Property(x => x.Nazwa1, map => map.Column("NAZWA_1"));
			Property(x => x.NarzutProc, map => map.Column("NARZUT_PROC"));
			ManyToOne(x => x.StawkiVat, map => 
			{
				map.Column("ID_STAWKI_VAT");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.JednostkiMiary, map => 
			{
				map.Column("ID_JEDNOSTKA_MIARY");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.SymbolePkwiu, map => 
			{
				map.Column("ID_SYMBOL_PKWIU");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			//Bag(x => x.MatEksploat, colmap =>  { colmap.Key(x => x.Column("ID_TOWAR")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.TowarMarka, colmap =>  { colmap.Key(x => x.Column("ID_TOWAR")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.TowarRezerwacja, colmap =>  { colmap.Key(x => x.Column("ID_TOWAR")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
