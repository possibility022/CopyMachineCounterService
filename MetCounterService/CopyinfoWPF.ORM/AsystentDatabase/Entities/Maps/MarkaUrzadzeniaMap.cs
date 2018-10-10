using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class MarkaUrzadzeniaMap : ClassMapping<MarkaUrzadzenia> {
        
        public MarkaUrzadzeniaMap() {
			Table("MARKA_URZADZENIA");
			
			
			Id(x => x.IdMarkaUrzadzenia, map => { map.Column("ID_MARKA_URZADZENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa1, map => { map.Column("NAZWA_1"); map.NotNullable(true); });
			//Bag(x => x.ModelUrzadzenia, colmap =>  { colmap.Key(x => x.Column("ID_MARKA_URZADZENIA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.TowarMarka, colmap =>  { colmap.Key(x => x.Column("ID_MARKA_URZADZENIA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
