using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class MatEksploatMap : ClassMapping<MatEksploat> {
        
        public MatEksploatMap() {
			Table("MAT_EKSPLOAT");
			
			
			Id(x => x.IdMatEksploat, map => { map.Column("ID_MAT_EKSPLOAT"); map.Generator(Generators.Assigned); });
            ManyToOne(x => x.ModelUrzadzenia, map =>
            {
                map.Column("ID_MODEL_URZADZENIA");
                //map.PropertyRef("IdModelUrzadzenia");
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Towar, map =>
            {
                map.Column("ID_TOWAR");
                //map.PropertyRef("IdTowar");
                map.NotNullable(true);
                map.Cascade(Cascade.None);
            });

        }
    }
}
