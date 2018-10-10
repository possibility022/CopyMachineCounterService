using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class UrzadzenieKlientStatusMap : ClassMapping<UrzadzenieKlientStatus> {
        
        public UrzadzenieKlientStatusMap() {
			Table("URZADZENIE_KLIENT_STATUS");
			
			
			Id(x => x.IdUrzadzenieKlientStatus, map => { map.Column("ID_URZADZENIE_KLIENT_STATUS"); map.Generator(Generators.Assigned); });
			Property(x => x.Status);
        }
    }
}
