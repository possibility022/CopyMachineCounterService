using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class StatusUmowyMap : ClassMapping<StatusUmowy> {
        
        public StatusUmowyMap() {
			Table("STATUS_UMOWY");
			
			
			Id(x => x.IdStatusUmowy, map => { map.Column("ID_STATUS_UMOWY"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
        }
    }
}
