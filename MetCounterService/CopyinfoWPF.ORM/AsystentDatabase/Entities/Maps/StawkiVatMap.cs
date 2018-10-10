using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class StawkiVatMap : ClassMapping<StawkiVat> {
        
        public StawkiVatMap() {
			Table("STAWKI_VAT");
			
			
			Id(x => x.IdStawkaVat, map => { map.Column("ID_STAWKA_VAT"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.Stawka);
			Property(x => x.Opis);
			Property(x => x.CzyZwolniony, map => map.Column("CZY_ZWOLNIONY"));
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_STAWKA_VAT")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.Towar, colmap =>  { colmap.Key(x => x.Column("ID_STAWKI_VAT")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
