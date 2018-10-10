using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class AdZdarzeniaMap : ClassMapping<AdZdarzenia> {
        
        public AdZdarzeniaMap() {
			Table("AD_ZDARZENIA");
			
			
			Id(x => x.ZdrId, map => { map.Column("ZDR_ID"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
			Property(x => x.Wlaczone);
			//Bag(x => x.AdDziennikZdarzen, colmap =>  { colmap.Key(x => x.Column("ZDR_ID")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
