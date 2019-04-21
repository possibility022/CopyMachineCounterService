using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class OkresRozliczeniowyMap : ClassMapping<OkresRozliczeniowy> {
        
        public OkresRozliczeniowyMap() {
			Table("OKRES_ROZLICZENIOWY");
			
			
			Id(x => x.IdOkresRozliczeniowy, map => { map.Column("ID_OKRES_ROZLICZENIOWY"); map.Generator(Generators.Assigned); });
			Property(x => x.Identyfikator);
			Property(x => x.Opis);
			Property(x => x.DataOd, map => map.Column("DATA_OD"));
			Property(x => x.DataDo, map => map.Column("DATA_DO"));
			Property(x => x.Status);
        }
    }
}
