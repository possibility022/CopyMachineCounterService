using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class CrmRodzajZadaniaMap : ClassMapping<CrmRodzajZadania> {
        
        public CrmRodzajZadaniaMap() {
			Table("CRM_RODZAJ_ZADANIA");
			
			
			Id(x => x.IdCrmRodzajZadania, map => { map.Column("ID_CRM_RODZAJ_ZADANIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
        }
    }
}
