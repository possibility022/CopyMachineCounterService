using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class ModelUrzadzeniaMap : ClassMapping<ModelUrzadzenia> {
        
        public ModelUrzadzeniaMap() {
			Table("MODEL_URZADZENIA");
			
			
			Id(x => x.IdModelUrzadzenia, map => { map.Column("ID_MODEL_URZADZENIA"); map.Generator(Generators.Assigned); });
			Property(x => x.Nazwa1, map => { map.Column("NAZWA_1"); map.NotNullable(true); });
			Property(x => x.Uwagi);
			ManyToOne(x => x.MarkaUrzadzenia, map => { map.Column("ID_MARKA_URZADZENIA"); map.Cascade(Cascade.None); });

			ManyToOne(x => x.RodzajUrzadzenia, map => { map.Column("ID_RODZAJ_URZADZENIA"); map.Cascade(Cascade.None); });

			//Bag(x => x.MatEksploat, colmap =>  { colmap.Key(x => x.Column("ID_MODEL_URZADZENIA")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
