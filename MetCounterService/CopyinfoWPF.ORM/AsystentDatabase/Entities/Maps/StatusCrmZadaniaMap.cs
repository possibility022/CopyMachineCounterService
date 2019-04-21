using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class StatusCrmZadaniaMap : ClassMapping<StatusCrmZadania> {
        
        public StatusCrmZadaniaMap() {
			Table("STATUS_CRM_ZADANIA");
			
			
			Id(x => x.IdStatusCrmZadania, map => { map.Column("ID_STATUS_CRM_ZADANIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
        }
    }
}
