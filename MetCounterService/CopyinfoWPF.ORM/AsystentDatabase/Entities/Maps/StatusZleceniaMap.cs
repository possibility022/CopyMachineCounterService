using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class StatusZleceniaMap : ClassMapping<StatusZlecenia> {
        
        public StatusZleceniaMap() {
			Table("STATUS_ZLECENIA");
			
			
			Id(x => x.IdStatusZlecenia, map => { map.Column("ID_STATUS_ZLECENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Status);
        }
    }
}
