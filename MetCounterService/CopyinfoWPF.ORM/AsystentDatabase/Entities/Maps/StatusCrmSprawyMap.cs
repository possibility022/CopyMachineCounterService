using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class StatusCrmSprawyMap : ClassMapping<StatusCrmSprawy> {
        
        public StatusCrmSprawyMap() {
			Table("STATUS_CRM_SPRAWY");
			
			
			Id(x => x.IdStatusCrmSprawy, map => { map.Column("ID_STATUS_CRM_SPRAWY"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
        }
    }
}
