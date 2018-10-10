using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class KlientStatusMap : ClassMapping<KlientStatus> {
        
        public KlientStatusMap() {
			Table("KLIENT_STATUS");
			
			
			Id(x => x.IdKlientStatus, map => { map.Column("ID_KLIENT_STATUS"); map.Generator(Generators.Assigned); });
			Property(x => x.Status);
			//Bag(x => x.Serwis, colmap =>  { colmap.Key(x => x.Column("ID_KLIENT_STATUS")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
