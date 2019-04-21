using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class AdresKlientMap : ClassMapping<AdresKlient> {
        
        public AdresKlientMap() {
			Table("ADRES_KLIENT");
			Id(x => x.IdAdresKlient, map => { map.Column("ID_ADRES_KLIENT"); map.Generator(Generators.Assigned); });
			Property(x => x.IdKlient, map => { map.Column("ID_KLIENT"); map.NotNullable(true); });
			Property(x => x.Rodzaj, map => map.NotNullable(true));
			Property(x => x.Ulica);
			Property(x => x.NrDomu, map => map.Column("NR_DOMU"));
			Property(x => x.NrLokalu, map => map.Column("NR_LOKALU"));
			Property(x => x.Miejscowosc);
			Property(x => x.KodPoczt, map => map.Column("KOD_POCZT"));
			Property(x => x.Poczta);
			Property(x => x.Telefon);
			Property(x => x.Fax);
			Property(x => x.Email);
			Property(x => x.Uwagi);
			Property(x => x.AdresWww, map => map.Column("ADRES_WWW"));
			Property(x => x.Wojewodztwo);
        }
    }
}
