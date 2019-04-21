using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class CrmKontaktyMap : ClassMapping<CrmKontakty> {
        
        public CrmKontaktyMap() {
			Table("CRM_KONTAKTY");
			
			
			Id(x => x.IdCrmKontakty, map => { map.Column("ID_CRM_KONTAKTY"); map.Generator(Generators.Assigned); });
			Property(x => x.IdKlient, map => map.Column("ID_KLIENT"));
			Property(x => x.NazwaSkrKl, map => map.Column("NAZWA_SKR_KL"));
			Property(x => x.Nazwa1Kl, map => map.Column("NAZWA_1_KL"));
			Property(x => x.Nazwa2Kl, map => map.Column("NAZWA_2_KL"));
			Property(x => x.NipKl, map => map.Column("NIP_KL"));
			Property(x => x.OpisKl, map => map.Column("OPIS_KL"));
			Property(x => x.UlicaKl, map => map.Column("ULICA_KL"));
			Property(x => x.NrDomuKl, map => map.Column("NR_DOMU_KL"));
			Property(x => x.NrLokaluKl, map => map.Column("NR_LOKALU_KL"));
			Property(x => x.MiejscowoscKl, map => map.Column("MIEJSCOWOSC_KL"));
			Property(x => x.KodPocztKl, map => map.Column("KOD_POCZT_KL"));
			Property(x => x.PocztaKl, map => map.Column("POCZTA_KL"));
			Property(x => x.TelefonKl, map => map.Column("TELEFON_KL"));
			Property(x => x.FaxKl, map => map.Column("FAX_KL"));
			Property(x => x.EmailKl, map => map.Column("EMAIL_KL"));
			Property(x => x.DataDodaniaCrm, map => map.Column("DATA_DODANIA_CRM"));
			Property(x => x.GodzDodaniaCrm, map => map.Column("GODZ_DODANIA_CRM"));
			Property(x => x.StatusSprawy, map => map.Column("STATUS_SPRAWY"));
			Property(x => x.Opis);
			Property(x => x.NazwaSprawy, map => map.Column("NAZWA_SPRAWY"));
			Property(x => x.DataKontaktu, map => map.Column("DATA_KONTAKTU"));
			Property(x => x.RodzajSprawy, map => map.Column("RODZAJ_SPRAWY"));
			Property(x => x.Tresc);
			Property(x => x.Uwagi);
			Property(x => x.RodzajKontaktu, map => map.Column("RODZAJ_KONTAKTU"));
			ManyToOne(x => x.Pracownik, map => 
			{
				map.Column("DODAJACY_CRM");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
