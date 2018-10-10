using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TypDokumentuMap : ClassMapping<TypDokumentu> {
        
        public TypDokumentuMap() {
			Table("TYP_DOKUMENTU");
			
			
			Id(x => x.IdTypDokumentu, map => { map.Column("ID_TYP_DOKUMENTU"); map.Generator(Generators.Assigned); });
			Property(x => x.TypSkr, map => map.Column("TYP_SKR"));
			Property(x => x.TypNazwa, map => map.Column("TYP_NAZWA"));
			Property(x => x.Autonumeracja);
			Property(x => x.OstatniNr, map => map.Column("OSTATNI_NR"));
			Property(x => x.OstatniNrKorekta, map => map.Column("OSTATNI_NR_KOREKTA"));
			Property(x => x.Sciezka);
			Property(x => x.SciezkaKorekta, map => map.Column("SCIEZKA_KOREKTA"));
			Property(x => x.CzyAktywny, map => map.Column("CZY_AKTYWNY"));
			Property(x => x.ZeraWiodace, map => map.Column("ZERA_WIODACE"));
			Property(x => x.TylkoDoOdczytu, map => map.Column("TYLKO_DO_ODCZYTU"));
			Property(x => x.TypSkrMagazynowy, map => map.Column("TYP_SKR_MAGAZYNOWY"));
			Property(x => x.TypNazwaMagazynowy, map => map.Column("TYP_NAZWA_MAGAZYNOWY"));
			Property(x => x.TworzDokMagazynowy, map => map.Column("TWORZ_DOK_MAGAZYNOWY"));
			Property(x => x.OstatniNrMagazynowy, map => map.Column("OSTATNI_NR_MAGAZYNOWY"));
			Property(x => x.OstatniNrNota, map => map.Column("OSTATNI_NR_NOTA"));
			Property(x => x.IdPoziomyCen, map => map.Column("ID_POZIOMY_CEN"));
			ManyToOne(x => x.RodzajDokumentu, map => 
			{
				map.Column("ID_RODZAJ_DOKUMENTU");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Magazyn, map => 
			{
				map.Column("ID_MAGAZYN");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
