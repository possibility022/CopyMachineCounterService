using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class StatusPrzypomnieniaMap : ClassMapping<StatusPrzypomnienia> {
        
        public StatusPrzypomnieniaMap() {
			Table("STATUS_PRZYPOMNIENIA");
			
			
			Id(x => x.IdStatusPrzypomnienia, map => { map.Column("ID_STATUS_PRZYPOMNIENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
        }
    }
}
