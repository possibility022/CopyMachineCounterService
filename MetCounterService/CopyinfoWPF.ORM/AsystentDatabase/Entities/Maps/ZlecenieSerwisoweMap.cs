using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{
    public class ZlecenieSerwisoweMap : ClassMapping<ZlecenieSerwisowe> {
        
        public ZlecenieSerwisoweMap() {
			Table("ZLECENIE_SERWISOWE");
			Lazy(false);
			Id(x => x.IdZlecenieSerwisowe, map => { map.Column("ID_ZLECENIE_SERWISOWE"); map.Generator(Generators.Assigned); });
			Property(x => x.IdUrzadzenieKlient, map => { map.Column("ID_URZADZENIE_KLIENT"); map.NotNullable(true); });
			Property(x => x.NrZlecenia, map => { map.Column("NR_ZLECENIA"); map.NotNullable(true); });
			Property(x => x.DataPrzyjeciaZlec, map => map.Column("DATA_PRZYJECIA_ZLEC"));
			Property(x => x.DataWykonaniaZlec, map => map.Column("DATA_WYKONANIA_ZLEC"));
			Property(x => x.DataZamknieciaZlec, map => map.Column("DATA_ZAMKNIECIA_ZLEC"));
			Property(x => x.DataZgloszenia, map => map.Column("DATA_ZGLOSZENIA"));
			Property(x => x.OsobaZglaszajaca, map => map.Column("OSOBA_ZGLASZAJACA"));
			Property(x => x.ZglaszanaUsterka, map => map.Column("ZGLASZANA_USTERKA"));
			Property(x => x.OpisCzynnosciSerwisowych, map => map.Column("OPIS_CZYNNOSCI_SERWISOWYCH"));
			Property(x => x.ZaleceniaSerwisu, map => map.Column("ZALECENIA_SERWISU"));
			Property(x => x.IdKlient, map => { map.Column("ID_KLIENT"); map.NotNullable(true); });
			Property(x => x.IdModelUrzadzenia, map => { map.Column("ID_MODEL_URZADZENIA"); map.NotNullable(true); });
			Property(x => x.IdMiejsceInstalacji, map => { map.Column("ID_MIEJSCE_INSTALACJI"); map.NotNullable(true); });
			Property(x => x.NrUrzadzenia, map => map.Column("NR_URZADZENIA"));
			Property(x => x.NrFabryczny, map => map.Column("NR_FABRYCZNY"));
			Property(x => x.IdSerwis, map => map.Column("ID_SERWIS"));
			Property(x => x.DataNastPrzegl, map => map.Column("DATA_NAST_PRZEGL"));
			Property(x => x.LicznikNastPrzegl, map => map.Column("LICZNIK_NAST_PRZEGL"));
			Property(x => x.CzyAktualizowanoStanUrz, map => map.Column("CZY_AKTUALIZOWANO_STAN_URZ"));
			Property(x => x.LicznikBiezacy, map => map.Column("LICZNIK_BIEZACY"));
			Property(x => x.DataKoncaGwarancji, map => map.Column("DATA_KONCA_GWARANCJI"));
			Property(x => x.LicznikKoncaGwarancji, map => map.Column("LICZNIK_KONCA_GWARANCJI"));
			Property(x => x.CzyJestGwarancja, map => map.Column("CZY_JEST_GWARANCJA"));
			Property(x => x.LicznikPoprzPrzegladu, map => map.Column("LICZNIK_POPRZ_PRZEGLADU"));
			Property(x => x.Priorytet);
			Property(x => x.Uzytkownik);
			//Property(x => x.GodzinaPrzyjeciaZlec, map => map.Column("GODZINA_PRZYJECIA_ZLEC"));
			//Property(x => x.GodzinaWykonaniaZlec, map => map.Column("GODZINA_WYKONANIA_ZLEC"));
			//Property(x => x.GodzinaZamknieciaZlec, map => map.Column("GODZINA_ZAMKNIECIA_ZLEC"));
			//Property(x => x.GodzinaZgloszenia, map => map.Column("GODZINA_ZGLOSZENIA"));
			Property(x => x.OpisCzynnosciPoprz, map => map.Column("OPIS_CZYNNOSCI_POPRZ"));
			Property(x => x.ZaleceniaSerwisuPoprz, map => map.Column("ZALECENIA_SERWISU_POPRZ"));
			Property(x => x.NrFakturyZewn, map => map.Column("NR_FAKTURY_ZEWN"));
			Property(x => x.DataPoprzPrzegladu, map => map.Column("DATA_POPRZ_PRZEGLADU"));
			Property(x => x.PrzewidDataZakoncz, map => map.Column("PRZEWID_DATA_ZAKONCZ"));
			Property(x => x.CzySerwisPlanowany, map => map.Column("CZY_SERWIS_PLANOWANY"));
			Property(x => x.LicznikZglaszany, map => map.Column("LICZNIK_ZGLASZANY"));
			Property(x => x.Notatka);
			Property(x => x.IdSerwisant, map => map.Column("ID_SERWISANT"));
			Property(x => x.IdStatusZlecenia, map => map.Column("ID_STATUS_ZLECENIA"));
			Property(x => x.IdRodzajZlecenia, map => map.Column("ID_RODZAJ_ZLECENIA"));
			Property(x => x.IdTypZlecenia, map => map.Column("ID_TYP_ZLECENIA"));
			Property(x => x.IdSposobZglZlecenia, map => map.Column("ID_SPOSOB_ZGL_ZLECENIA"));
			Property(x => x.IdPoziomyCen, map => map.Column("ID_POZIOMY_CEN"));
			//Bag(x => x.LicznikiUrzZlec, colmap =>  { colmap.Key(x => x.Column("ID_ZLECENIE_SERWISOWE")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.WymienioneCzesci, colmap =>  { colmap.Key(x => x.Column("ID_ZLECENIE_SERWISOWE")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
