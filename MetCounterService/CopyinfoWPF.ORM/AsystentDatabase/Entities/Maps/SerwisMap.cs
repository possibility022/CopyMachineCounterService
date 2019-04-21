using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class SerwisMap : ClassMapping<Serwis> {
        
        public SerwisMap() {
			
			
			Id(x => x.IdSerwis, map => { map.Column("ID_SERWIS"); map.Generator(Generators.Assigned); });
			Property(x => x.Nip);
			Property(x => x.NazwaSkr, map => map.Column("NAZWA_SKR"));
			Property(x => x.Nazwa1, map => map.Column("NAZWA_1"));
			Property(x => x.Nazwa2, map => map.Column("NAZWA_2"));
			Property(x => x.Miejscowosc);
			Property(x => x.Ulica);
			Property(x => x.NrDomu, map => map.Column("NR_DOMU"));
			Property(x => x.NrLokalu, map => map.Column("NR_LOKALU"));
			Property(x => x.KodPocztowy, map => map.Column("KOD_POCZTOWY"));
			Property(x => x.Poczta);
			Property(x => x.EMail, map => map.Column("E_MAIL"));
			Property(x => x.Logo);
			Property(x => x.Uwagi);
			Property(x => x.Opis);
			Property(x => x.KodAktywacyjny, map => map.Column("KOD_AKTYWACYJNY"));
			Property(x => x.DataInstalacji, map => map.Column("DATA_INSTALACJI"));
			Property(x => x.BlokadaDemo, map => map.Column("BLOKADA_DEMO"));
			Property(x => x.CzyAktywnyAutonumer, map => map.Column("CZY_AKTYWNY_AUTONUMER"));
			Property(x => x.Autonumeracja);
			Property(x => x.AutonumeracjaBlob, map => map.Column("AUTONUMERACJA_BLOB"));
			Property(x => x.OstatniNr, map => map.Column("OSTATNI_NR"));
			Property(x => x.ZeraWiodace, map => map.Column("ZERA_WIODACE"));
			Property(x => x.CzyAutonumerUrz, map => map.Column("CZY_AUTONUMER_URZ"));
			Property(x => x.AutonumeracjaUrz, map => map.Column("AUTONUMERACJA_URZ"));
			Property(x => x.AutonumeracjaBlobUrz, map => map.Column("AUTONUMERACJA_BLOB_URZ"));
			Property(x => x.OstatniNrUrz, map => map.Column("OSTATNI_NR_URZ"));
			Property(x => x.ZeraWiodaceUrz, map => map.Column("ZERA_WIODACE_URZ"));
			Property(x => x.CzyInfoZlecen, map => map.Column("CZY_INFO_ZLECEN"));
			Property(x => x.IleDniInfoZlecen, map => map.Column("ILE_DNI_INFO_ZLECEN"));
			Property(x => x.IleDniInfoSerwisow, map => map.Column("ILE_DNI_INFO_SERWISOW"));
			Property(x => x.CzyAutonumerUmow, map => map.Column("CZY_AUTONUMER_UMOW"));
			Property(x => x.AutonumeracjaUmow, map => map.Column("AUTONUMERACJA_UMOW"));
			Property(x => x.AutonumeracjaBlobUmow, map => map.Column("AUTONUMERACJA_BLOB_UMOW"));
			Property(x => x.OstatniNrUmow, map => map.Column("OSTATNI_NR_UMOW"));
			Property(x => x.ZeraWiodaceUmow, map => map.Column("ZERA_WIODACE_UMOW"));
			Property(x => x.CzyAutonumerZlecJedn, map => map.Column("CZY_AUTONUMER_ZLEC_JEDN"));
			Property(x => x.AutonumeracjaZlecJedn, map => map.Column("AUTONUMERACJA_ZLEC_JEDN"));
			Property(x => x.AutonumeracjaBlobZlecJedn, map => map.Column("AUTONUMERACJA_BLOB_ZLEC_JEDN"));
			Property(x => x.OstatniNrZlecJedn, map => map.Column("OSTATNI_NR_ZLEC_JEDN"));
			Property(x => x.ZeraWiodaceZlecJedn, map => map.Column("ZERA_WIODACE_ZLEC_JEDN"));
			Property(x => x.Telefon);
			Property(x => x.CzyAutonumerSpr, map => map.Column("CZY_AUTONUMER_SPR"));
			Property(x => x.AutonumeracjaSpr, map => map.Column("AUTONUMERACJA_SPR"));
			Property(x => x.AutonumeracjaBlobSpr, map => map.Column("AUTONUMERACJA_BLOB_SPR"));
			Property(x => x.OstatniNrSpr, map => map.Column("OSTATNI_NR_SPR"));
			Property(x => x.ZeraWiodaceSpr, map => map.Column("ZERA_WIODACE_SPR"));
			Property(x => x.JakieSpr, map => map.Column("JAKIE_SPR"));
			Property(x => x.SposobRozlVat, map => map.Column("SPOSOB_ROZL_VAT"));
			Property(x => x.JakieDoc, map => map.Column("JAKIE_DOC"));
			Property(x => x.CenaNazwa1, map => map.Column("CENA_NAZWA_1"));
			Property(x => x.CenaDomNarzut1, map => map.Column("CENA_DOM_NARZUT_1"));
			Property(x => x.CenaCzyStala1, map => map.Column("CENA_CZY_STALA_1"));
			Property(x => x.CenaNazwa2, map => map.Column("CENA_NAZWA_2"));
			Property(x => x.CenaDomNarzut2, map => map.Column("CENA_DOM_NARZUT_2"));
			Property(x => x.CenaCzyStala2, map => map.Column("CENA_CZY_STALA_2"));
			Property(x => x.CenaNazwa3, map => map.Column("CENA_NAZWA_3"));
			Property(x => x.CenaDomNarzut3, map => map.Column("CENA_DOM_NARZUT_3"));
			Property(x => x.CenaCzyStala3, map => map.Column("CENA_CZY_STALA_3"));
			Property(x => x.CenaNazwa4, map => map.Column("CENA_NAZWA_4"));
			Property(x => x.CenaDomNarzut4, map => map.Column("CENA_DOM_NARZUT_4"));
			Property(x => x.CenaCzyStala4, map => map.Column("CENA_CZY_STALA_4"));
			Property(x => x.CenaNazwa5, map => map.Column("CENA_NAZWA_5"));
			Property(x => x.CenaDomNarzut5, map => map.Column("CENA_DOM_NARZUT_5"));
			Property(x => x.CenaCzyStala5, map => map.Column("CENA_CZY_STALA_5"));
			Property(x => x.CenaNazwa6, map => map.Column("CENA_NAZWA_6"));
			Property(x => x.CenaDomNarzut6, map => map.Column("CENA_DOM_NARZUT_6"));
			Property(x => x.CenaCzyStala6, map => map.Column("CENA_CZY_STALA_6"));
			Property(x => x.CenaNazwa7, map => map.Column("CENA_NAZWA_7"));
			Property(x => x.CenaDomNarzut7, map => map.Column("CENA_DOM_NARZUT_7"));
			Property(x => x.CenaCzyStala7, map => map.Column("CENA_CZY_STALA_7"));
			Property(x => x.CenaNazwa8, map => map.Column("CENA_NAZWA_8"));
			Property(x => x.CenaDomNarzut8, map => map.Column("CENA_DOM_NARZUT_8"));
			Property(x => x.CenaCzyStala8, map => map.Column("CENA_CZY_STALA_8"));
			Property(x => x.CenaNazwa9, map => map.Column("CENA_NAZWA_9"));
			Property(x => x.CenaDomNarzut9, map => map.Column("CENA_DOM_NARZUT_9"));
			Property(x => x.CenaCzyStala9, map => map.Column("CENA_CZY_STALA_9"));
			Property(x => x.CenaNazwa10, map => map.Column("CENA_NAZWA_10"));
			Property(x => x.CenaDomNarzut10, map => map.Column("CENA_DOM_NARZUT_10"));
			Property(x => x.CenaCzyStala10, map => map.Column("CENA_CZY_STALA_10"));
			ManyToOne(x => x.Transport, map => 
			{
				map.Column("ID_TRANSPORT");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.TypPlatnosci, map => 
			{
				map.Column("ID_TYP_PLATNOSCI");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Waluty, map => 
			{
				map.Column("ID_WALUTA");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.StawkiVat, map => 
			{
				map.Column("ID_STAWKA_VAT");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.JednostkiMiary, map => 
			{
				map.Column("ID_JEDNOSTKA_MIARY");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.GrupaKlient, map => 
			{
				map.Column("ID_GRUPA_KLIENT");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.KlientStatus, map => 
			{
				map.Column("ID_KLIENT_STATUS");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			//Bag(x => x.Dostawcy, colmap =>  { colmap.Key(x => x.Column("ID_SERWIS")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
