using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class RodzajUrzadzeniaMap : ClassMapping<RodzajUrzadzenia> {
        
        public RodzajUrzadzeniaMap() {
			Table("RODZAJ_URZADZENIA");
			
			
			Id(x => x.IdRodzajUrzadzenia, map => { map.Column("ID_RODZAJ_URZADZENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa1, map => { map.Column("NAZWA_1"); map.NotNullable(true); });
			//Bag(x => x.ModelUrzadzenia, colmap =>  { colmap.Key(x => x.Column("ID_RODZAJ_URZADZENIA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
