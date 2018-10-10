using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TransportMap : ClassMapping<Transport> {
        
        public TransportMap() {
			
			
			Id(x => x.IdTransport, map => { map.Column("ID_TRANSPORT"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.Opis);
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_TRANSPORT")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
