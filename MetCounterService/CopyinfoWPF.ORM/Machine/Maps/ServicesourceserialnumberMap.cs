using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.Machine.Maps
{
    public class ServiceSourceSerialNumberMap : ClassMapping<ServiceSourceSerialNumber> {
        
        public ServiceSourceSerialNumberMap() {
			Schema("Machine");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Content, map => map.NotNullable(true));
			//Bag(x => x.Record, colmap =>  { colmap.Key(x => x.Column("ServiceSourceSerialNumber")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
