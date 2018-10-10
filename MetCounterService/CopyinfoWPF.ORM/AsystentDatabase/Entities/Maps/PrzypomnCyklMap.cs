using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class PrzypomnCyklMap : ClassMapping<PrzypomnCykl> {
        
        public PrzypomnCyklMap() {
			Table("PRZYPOMN_CYKL");
			
			
			Id(x => x.IdPrzypomnCykl, map => { map.Column("ID_PRZYPOMN_CYKL"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa);
			Property(x => x.IleMinut, map => { map.Column("ILE_MINUT"); map.NotNullable(true); });
        }
    }
}
