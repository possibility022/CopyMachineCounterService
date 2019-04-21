using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class CrmKategoriaSprMap : ClassMapping<CrmKategoriaSpr> {
        
        public CrmKategoriaSprMap() {
			Table("CRM_KATEGORIA_SPR");
			
			
			Id(x => x.IdCrmKategoriaSpr, map => { map.Column("ID_CRM_KATEGORIA_SPR"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
        }
    }
}
