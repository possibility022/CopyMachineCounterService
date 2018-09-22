using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.Maps
{
    public class ServicesourceserialnumberMap : ClassMapping<Servicesourceserialnumber> {
        
        public ServicesourceserialnumberMap() {
			Schema("Machine");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Content, map => map.NotNullable(true));
			//Bag(x => x.Record, colmap =>  { colmap.Key(x => x.Column("ServiceSourceSerialNumber")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
