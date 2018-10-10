using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class UrzadzenieKlientMap : ClassMapping<UrzadzenieKlient> {
        
        public UrzadzenieKlientMap() {
			Table("URZADZENIE_KLIENT");
			
			Id(x => x.IdUrzadzenieKlient, map => { map.Column("ID_URZADZENIE_KLIENT"); map.Generator(Generators.Assigned); });
			Property(x => x.IdModelUrzadzenia, map => { map.Column("ID_MODEL_URZADZENIA"); map.NotNullable(true); });
			Property(x => x.IdKlient, map => { map.Column("ID_KLIENT"); map.NotNullable(true); });
			Property(x => x.NrUrzadzenia, map => { map.Column("NR_URZADZENIA"); map.NotNullable(true); });
			Property(x => x.IdMiejsceInstalacji, map => { map.Column("ID_MIEJSCE_INSTALACJI"); map.NotNullable(true); });
			Property(x => x.DataInstalacji, map => { map.Column("DATA_INSTALACJI"); map.NotNullable(true); });
			Property(x => x.LicznikInstalacji, map => map.Column("LICZNIK_INSTALACJI"));
			Property(x => x.NrFabryczny, map => map.Column("NR_FABRYCZNY"));
			Property(x => x.WyposazenieDodatkowe, map => map.Column("WYPOSAZENIE_DODATKOWE"));
			Property(x => x.IleMcGwarancja, map => map.Column("ILE_MC_GWARANCJA"));
			Property(x => x.IleKopiiGwarancja, map => map.Column("ILE_KOPII_GWARANCJA"));
			Property(x => x.CzestotlPrzeglKopie, map => map.Column("CZESTOTL_PRZEGL_KOPIE"));
			Property(x => x.CzestotlPrzeglMc, map => map.Column("CZESTOTL_PRZEGL_MC"));
			Property(x => x.Uwagi);
			Property(x => x.DataNastPrzegl, map => map.Column("DATA_NAST_PRZEGL"));
			Property(x => x.DataPoprzPrzegl, map => map.Column("DATA_POPRZ_PRZEGL"));
			Property(x => x.LicznikNastPrzegl, map => map.Column("LICZNIK_NAST_PRZEGL"));
			Property(x => x.LicznikPoprzPrzegl, map => map.Column("LICZNIK_POPRZ_PRZEGL"));
			Property(x => x.IdSerwis, map => map.Column("ID_SERWIS"));
			Property(x => x.IdSerwisant, map => { map.Column("ID_SERWISANT"); map.NotNullable(true); });
			Property(x => x.Uzytkownik, map => map.NotNullable(true));
			Property(x => x.OpisCzynnosciPoprz, map => map.Column("OPIS_CZYNNOSCI_POPRZ"));
			Property(x => x.ZaleceniaSerwisuPoprz, map => map.Column("ZALECENIA_SERWISU_POPRZ"));
			Property(x => x.NrGwarancji, map => map.Column("NR_GWARANCJI"));
			Property(x => x.ModelUwagi, map => map.Column("MODEL_UWAGI"));
			Property(x => x.OpisMiejscaInstalacji, map => map.Column("OPIS_MIEJSCA_INSTALACJI"));
			Property(x => x.IdUrzadzenieKlientStatus, map => map.Column("ID_URZADZENIE_KLIENT_STATUS"));
        }
    }
}
