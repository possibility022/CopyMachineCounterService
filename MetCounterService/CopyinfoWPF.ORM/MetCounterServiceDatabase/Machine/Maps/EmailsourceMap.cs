using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine.Maps
{
    public class EmailsourceMap : ClassMapping<EmailSource> {
        
        public EmailsourceMap() {
			Schema("Machine");
			
			Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Content, map => map.NotNullable(true));
			//Bag(x => x.Record, colmap =>  { colmap.Key(x => x.Column("EmailSource")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
