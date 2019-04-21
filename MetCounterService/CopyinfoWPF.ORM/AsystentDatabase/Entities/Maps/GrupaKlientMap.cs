using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class GrupaKlientMap : ClassMapping<GrupaKlient> {
        
        public GrupaKlientMap() {
			Table("GRUPA_KLIENT");
			
			
			Id(x => x.IdGrupaKlient, map => { map.Column("ID_GRUPA_KLIENT"); map.Generator(Generators.Assigned); });
			Property(x => x.IdSerwis, map => { map.Column("ID_SERWIS"); map.NotNullable(true); });
			Property(x => x.Nazwa1, map => { map.Column("NAZWA_1"); map.NotNullable(true); });
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_GRUPA_KLIENT")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
