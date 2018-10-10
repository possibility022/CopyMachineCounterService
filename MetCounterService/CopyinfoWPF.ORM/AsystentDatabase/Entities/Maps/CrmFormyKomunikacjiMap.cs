using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class CrmFormyKomunikacjiMap : ClassMapping<CrmFormyKomunikacji> {
        
        public CrmFormyKomunikacjiMap() {
			Table("CRM_FORMY_KOMUNIKACJI");
			
			
			Id(x => x.IdCrmFormyKomunikacji, map => { map.Column("ID_CRM_FORMY_KOMUNIKACJI"); map.Generator(Generators.Assigned); });
			Property(x => x.FormaKomunikacji, map => map.Column("FORMA_KOMUNIKACJI"));
        }
    }
}
