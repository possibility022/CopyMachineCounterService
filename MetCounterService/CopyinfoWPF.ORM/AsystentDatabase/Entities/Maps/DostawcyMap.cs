using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class DostawcyMap : ClassMapping<Dostawcy> {
        
        public DostawcyMap() {
			
			
			Id(x => x.IdDostawcy, map => { map.Column("ID_DOSTAWCY"); map.Generator(Generators.Assigned); });
			Property(x => x.NazwaSkr, map => { map.Column("NAZWA_SKR"); map.NotNullable(true); });
			Property(x => x.Nazwa1);
			Property(x => x.Nazwa2);
			Property(x => x.Nazwa3);
			Property(x => x.KodPocztowy, map => map.Column("KOD_POCZTOWY"));
			Property(x => x.Miejscowosc);
			Property(x => x.Ulica);
			Property(x => x.NrDomu, map => map.Column("NR_DOMU"));
			Property(x => x.NrLokalu, map => map.Column("NR_LOKALU"));
			Property(x => x.Wojewodztwo);
			Property(x => x.Rabat);
			Property(x => x.Nip);
			Property(x => x.NrRachunku, map => map.Column("NR_RACHUNKU"));
			Property(x => x.NazwaBanku, map => map.Column("NAZWA_BANKU"));
			Property(x => x.Uwagi);
			Property(x => x.Oddzial);
			Property(x => x.Regon);
			Property(x => x.OsobaPodpisujaca, map => map.Column("OSOBA_PODPISUJACA"));
			Property(x => x.PeselOf, map => map.Column("PESEL_OF"));
			Property(x => x.DokumentOf, map => map.Column("DOKUMENT_OF"));
			Property(x => x.NrDokumentuOf, map => map.Column("NR_DOKUMENTU_OF"));
			Property(x => x.WydanyPrzezOf, map => map.Column("WYDANY_PRZEZ_OF"));
			ManyToOne(x => x.Serwis, map => 
			{
				map.Column("ID_SERWIS");
				map.PropertyRef("IdSerwis");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			//Bag(x => x.ZamowieniaCzesci, colmap =>  { colmap.Key(x => x.Column("ID_DOSTAWCY")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
