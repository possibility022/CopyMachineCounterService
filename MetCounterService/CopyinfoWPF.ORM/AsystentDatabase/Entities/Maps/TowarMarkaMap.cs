using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TowarMarkaMap : ClassMapping<TowarMarka> {
        
        public TowarMarkaMap() {
			Table("TOWAR_MARKA");
			
			
			Id(x => x.IdTowarMarka, map => { map.Column("ID_TOWAR_MARKA"); map.Generator(Generators.Assigned); });
			ManyToOne(x => x.Towar, map => 
			{
				map.Column("ID_TOWAR");
				//map.PropertyRef("IdTowar");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.MarkaUrzadzenia, map => 
			{
				map.Column("ID_MARKA_URZADZENIA");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
