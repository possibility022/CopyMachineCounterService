using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine.Maps
{
    public class ServiceSourceCountersMap : ClassMapping<ServiceSourceCounters> {
        
        public ServiceSourceCountersMap() {
			Schema("Machine");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Content, map => map.NotNullable(true));
			//Bag(x => x.Record, colmap =>  { colmap.Key(x => x.Column("ServiceSourceCounters")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
