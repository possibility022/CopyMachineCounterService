using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class ZamowieniaCzesciMap : ClassMapping<ZamowieniaCzesci> {
        
        public ZamowieniaCzesciMap() {
			Table("ZAMOWIENIA_CZESCI");
			
			
			Id(x => x.IdZamowieniaCzesci, map => { map.Column("ID_ZAMOWIENIA_CZESCI"); map.Generator(Generators.Assigned); });
			Property(x => x.IdTowar, map => map.Column("ID_TOWAR"));
			Property(x => x.Nazwa1, map => map.Column("NAZWA_1"));
			Property(x => x.Nazwa2, map => map.Column("NAZWA_2"));
			Property(x => x.Uwagi1, map => map.Column("UWAGI_1"));
			Property(x => x.Uwagi2, map => map.Column("UWAGI_2"));
			Property(x => x.Kod);
			Property(x => x.SymbolOem, map => map.Column("SYMBOL_OEM"));
			Property(x => x.DataDodaniaZam, map => map.Column("DATA_DODANIA_ZAM"));
			Property(x => x.Przeznaczenie);
			Property(x => x.DataZlozeniaZam, map => map.Column("DATA_ZLOZENIA_ZAM"));
			Property(x => x.PlanTermDostawy, map => map.Column("PLAN_TERM_DOSTAWY"));
			Property(x => x.Status);
			Property(x => x.NaKiedy, map => map.Column("NA_KIEDY"));
			Property(x => x.Opis);
			Property(x => x.GodzDodaniaZam, map => map.Column("GODZ_DODANIA_ZAM"));
			Property(x => x.GodzZlozeniaZam, map => map.Column("GODZ_ZLOZENIA_ZAM"));
			Property(x => x.ZamawiajacyImie, map => map.Column("ZAMAWIAJACY_IMIE"));
			Property(x => x.ZamawiajacyNazwisko, map => map.Column("ZAMAWIAJACY_NAZWISKO"));
			Property(x => x.IdZamawiajacy, map => map.Column("ID_ZAMAWIAJACY"));
			Property(x => x.Ilosc);
			ManyToOne(x => x.Pracownik, map => 
			{
				map.Column("DODAJACY_ZAM");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Dostawcy, map => 
			{
				map.Column("ID_DOSTAWCY");
				map.PropertyRef("IdDostawcy");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
