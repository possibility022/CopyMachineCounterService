using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class TowarRezerwacjaMap : ClassMapping<TowarRezerwacja> {
        
        public TowarRezerwacjaMap() {
			Table("TOWAR_REZERWACJA");
			
			
			Id(x => x.IdTowarRezerwacja, map => { map.Column("ID_TOWAR_REZERWACJA"); map.Generator(Generators.Assigned); });
			Property(x => x.IloscRezerwacja, map => map.Column("ILOSC_REZERWACJA"));
			ManyToOne(x => x.Pracownik, map => { map.Column("ID_PRACOWNIK"); map.Cascade(Cascade.None); });

			ManyToOne(x => x.Towar, map => 
			{
				map.Column("ID_TOWAR");
				map.PropertyRef("IdTowar");
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Magazyn, map => 
			{
				map.Column("ID_MAGAZYN");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
