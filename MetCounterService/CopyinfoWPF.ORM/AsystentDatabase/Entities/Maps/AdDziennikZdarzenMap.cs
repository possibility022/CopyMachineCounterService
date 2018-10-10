using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class AdDziennikZdarzenMap : ClassMapping<AdDziennikZdarzen> {
        
        public AdDziennikZdarzenMap() {
			Table("AD_DZIENNIK_ZDARZEN");
			
			
			Id(x => x.DznId, map => { map.Column("DZN_ID"); map.Generator(Generators.Assigned); });
			Property(x => x.Data, map => map.NotNullable(true));
			Property(x => x.Czas, map => map.NotNullable(true));
			Property(x => x.UzyId, map => map.Column("UZY_ID"));
			Property(x => x.Status);
			Property(x => x.Informacje);
			ManyToOne(x => x.AdZdarzenia, map => 
			{
				map.Column("ZDR_ID");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
