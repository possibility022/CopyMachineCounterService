using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CopyinfoWPF.ORM.AsystentDatabase.Device.Maps
{
    public class ClientDeviceMap : ClassMapping<ClientDevice>
    {

        public ClientDeviceMap()
        {
            Lazy(false);
            Id(x => x.ID_URZADZENIE_KLIENT, map => map.Generator(Generators.Identity));
            Property(x => x.ID_URZADZENIE_KLIENT, map => map.NotNullable(true));

            //Property(x => x.ID_MODEL_URZADZENIA, map => map.NotNullable(true));

            ManyToOne(x => x.DeviceModel, map =>
            {
                map.Column(nameof(ClientDevice.ID_MODEL_URZADZENIA));
                map.NotNullable(false);
            });

            Property(x => x.ID_KLIENT, map => map.NotNullable(true));
            Property(x => x.NR_URZADZENIA, map => map.NotNullable(true));
            Property(x => x.ID_MIEJSCE_INSTALACJI, map => map.NotNullable(true));
            Property(x => x.DATA_INSTALACJI, map => map.NotNullable(true));
            Property(x => x.LICZNIK_INSTALACJI);
            Property(x => x.NR_FABRYCZNY);
            //Property(x => x.WYPOSAZENIE_DODATKOWE);
            Property(x => x.ILE_MC_GWARANCJA);
            Property(x => x.ILE_KOPII_GWARANCJA);
            Property(x => x.CZESTOTL_PRZEGL_KOPIE);
            Property(x => x.CZESTOTL_PRZEGL_MC);
            //Property(x => x.UWAGI);
            Property(x => x.DATA_NAST_PRZEGL);
            Property(x => x.DATA_POPRZ_PRZEGL);
            Property(x => x.LICZNIK_NAST_PRZEGL);
            Property(x => x.LICZNIK_POPRZ_PRZEGL);
            Property(x => x.ID_SERWIS);
            Property(x => x.ID_SERWISANT, map => map.NotNullable(true));
            Property(x => x.UZYTKOWNIK, map => map.NotNullable(true));
            //Property(x => x.OPIS_CZYNNOSCI_POPRZ);
            //Property(x => x.ZALECENIA_SERWISU_POPRZ);
            Property(x => x.NR_GWARANCJI);
            //Property(x => x.MODEL_UWAGI);
            //Property(x => x.OPIS_MIEJSCA_INSTALACJI);
            Property(x => x.ID_URZADZENIE_KLIENT_STATUS);

            Table("URZADZENIE_KLIENT");
        }
    }
}
