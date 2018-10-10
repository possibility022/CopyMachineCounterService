using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TypZleceniaMap : ClassMapping<TypZlecenia> {
        
        public TypZleceniaMap() {
			Table("TYP_ZLECENIA");
			
			
			Id(x => x.IdTypZlecenia, map => { map.Column("ID_TYP_ZLECENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Typ);
        }
    }
}
