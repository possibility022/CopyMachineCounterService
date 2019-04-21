using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class JakieRozliczenieMap : ClassMapping<JakieRozliczenie> {
        
        public JakieRozliczenieMap() {
			Table("JAKIE_ROZLICZENIE");
			
			
			Id(x => x.IdJakieRozliczenie, map => { map.Column("ID_JAKIE_ROZLICZENIE"); map.Generator(Generators.Assigned); });
			Property(x => x.Symbol);
			Property(x => x.Nazwa);
        }
    }
}
