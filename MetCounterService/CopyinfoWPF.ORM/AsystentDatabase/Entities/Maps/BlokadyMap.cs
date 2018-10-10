using NHibernate.Mapping.ByCode.Conformist;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class BlokadyMap : ClassMapping<Blokady>
    {

        public BlokadyMap()
        {

            Id(x => x.IdRekordu, map => { map.Column("ID_REKORDU"); });
            Property(x => x.IdRekordu, map => { map.Column("ID_REKORDU"); map.NotNullable(true); });
            Property(x => x.Uzytkownik);
            Property(x => x.NazwaTabeli, map => map.Column("NAZWA_TABELI"));
            Property(x => x.DataBlokady, map => { map.Column("DATA_BLOKADY"); map.NotNullable(true); });
            Property(x => x.GodzinaBlokady, map => { map.Column("GODZINA_BLOKADY"); map.NotNullable(true); });
        }
    }
}
