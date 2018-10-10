using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class PrzypomnOdlozMap : ClassMapping<PrzypomnOdloz> {
        
        public PrzypomnOdlozMap() {
			Table("PRZYPOMN_ODLOZ");
			
			
			Id(x => x.IdPrzypomnOdloz, map => { map.Column("ID_PRZYPOMN_ODLOZ"); map.Generator(Generators.Assigned); });
			Property(x => x.Tabela);
			Property(x => x.Wiersz);
			Property(x => x.DataOdloz, map => map.Column("DATA_ODLOZ"));
			Property(x => x.IdPracownik, map => map.Column("ID_PRACOWNIK"));
        }
    }
}
