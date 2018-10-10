using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class NotatkiMap : ClassMapping<Notatki> {
        
        public NotatkiMap() {
			
			
			Id(x => x.IdNotatki, map => { map.Column("ID_NOTATKI"); map.Generator(Generators.Assigned); });
			Property(x => x.IdZewnetrzne, map => map.Column("ID_ZEWNETRZNE"));
			Property(x => x.NazwaTabeli, map => map.Column("NAZWA_TABELI"));
			Property(x => x.Tresc);
        }
    }
}
