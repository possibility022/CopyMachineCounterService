using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings
{
    public static class AsystentFactorySettings
    {
        private static HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();
            // Generate
            //var files = Directory.GetFiles(@"D:\Programowanie\C#2017\MetCounterService\MetCounterService\CopyinfoWPF.ORM\AsystentDatabase\Entities\Maps");
            //foreach (var file in files)
            //{
            //    Console.WriteLine($"mapper.AddMapping<{Path.GetFileName(file)}>();");
            //}

            mapper.AddMapping<AdDziennikZdarzenMap>();
            mapper.AddMapping<AdresKlientMap>();
            mapper.AddMapping<AdZdarzeniaMap>();
            mapper.AddMapping<BlokadyMap>();
            mapper.AddMapping<CrmFormyKomunikacjiMap>();
            mapper.AddMapping<CrmKategoriaSprMap>();
            mapper.AddMapping<CrmKontaktyMap>();
            mapper.AddMapping<CrmRodzajZadaniaMap>();
            mapper.AddMapping<DostawcyMap>();
            mapper.AddMapping<GrupaKlientMap>();
            mapper.AddMapping<JakieRozliczenieMap>();
            mapper.AddMapping<JednostkiMiaryMap>();
            mapper.AddMapping<KlientStatusMap>();
            mapper.AddMapping<MagazynMap>();
            mapper.AddMapping<MarkaUrzadzeniaMap>();
            mapper.AddMapping<MatEksploatMap>();
            mapper.AddMapping<ModelUrzadzeniaMap>();
            mapper.AddMapping<NotatkiMap>();
            mapper.AddMapping<OkresRozliczeniowyMap>();
            mapper.AddMapping<PracownikMap>();
            mapper.AddMapping<PrzypomnCyklMap>();
            mapper.AddMapping<PrzypomnOdlozMap>();
            mapper.AddMapping<RodzajDokumentuMap>();
            mapper.AddMapping<RodzajUrzadzeniaMap>();
            mapper.AddMapping<RodzajZleceniaMap>();
            mapper.AddMapping<SerwisMap>();
            mapper.AddMapping<SposobZglZleceniaMap>();
            mapper.AddMapping<StatusCrmSprawyMap>();
            mapper.AddMapping<StatusCrmZadaniaMap>();
            mapper.AddMapping<StatusPrzypomnieniaMap>();
            mapper.AddMapping<StatusUmowyMap>();
            mapper.AddMapping<StatusZleceniaMap>();
            mapper.AddMapping<StawkiVatMap>();
            mapper.AddMapping<SymbolePkwiuMap>();
            mapper.AddMapping<TowarMap>();
            mapper.AddMapping<TowarMarkaMap>();
            mapper.AddMapping<TowarRezerwacjaMap>();
            mapper.AddMapping<TransportMap>();
            mapper.AddMapping<TypDokumentuMap>();
            mapper.AddMapping<TypPlatnosciMap>();
            mapper.AddMapping<TypZleceniaMap>();
            mapper.AddMapping<UrzadzenieKlientMap>();
            mapper.AddMapping<UrzadzenieKlientStatusMap>();
            mapper.AddMapping<WalutyMap>();
            mapper.AddMapping<WersjaBazyMap>();
            mapper.AddMapping<ZamowieniaCzesciMap>();
            mapper.AddMapping<KlientMap>();
            mapper.AddMapping<UmowaSerwisowaMap>();
            mapper.AddMapping<ZlecenieSerwisoweMap>();

            var mapping = mapper.CompileMappingFor(
                new[]
                {
                typeof(AdDziennikZdarzen),
                typeof(AdresKlient),
                typeof(AdZdarzenia),
                typeof(Blokady),
                typeof(CrmFormyKomunikacji),
                typeof(CrmKategoriaSpr),
                typeof(CrmKontakty),
                typeof(CrmRodzajZadania),
                typeof(Dostawcy),
                typeof(GrupaKlient),
                typeof(JakieRozliczenie),
                typeof(JednostkiMiary),
                typeof(KlientStatus),
                typeof(Magazyn),
                typeof(MarkaUrzadzenia),
                typeof(MatEksploat),
                typeof(ModelUrzadzenia),
                typeof(Notatki),
                typeof(OkresRozliczeniowy),
                typeof(Pracownik),
                typeof(PrzypomnCykl),
                typeof(PrzypomnOdloz),
                typeof(RodzajDokumentu),
                typeof(RodzajUrzadzenia),
                typeof(RodzajZlecenia),
                typeof(Serwis),
                typeof(SposobZglZlecenia),
                typeof(StatusCrmSprawy),
                typeof(StatusCrmZadania),
                typeof(StatusPrzypomnienia),
                typeof(StatusUmowy),
                typeof(StatusZlecenia),
                typeof(StawkiVat),
                typeof(SymbolePkwiu),
                typeof(Towar),
                typeof(TowarMarka),
                typeof(TowarRezerwacja),
                typeof(Transport),
                typeof(TypDokumentu),
                typeof(TypPlatnosci),
                typeof(TypZlecenia),
                typeof(UrzadzenieKlient),
                typeof(UrzadzenieKlientStatus),
                typeof(Waluty),
                typeof(WersjaBazy),
                typeof(ZamowieniaCzesci),
                typeof(Klient),
                typeof(UmowaSerwisowa),
                typeof(ZlecenieSerwisowe)
                });

            return mapping;
        }

        public static ISessionFactory GetSessionFactory()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "***REMOVED***Database=D:\\data\\test.fdb;DataSource=WIN-RP56U0UJDMQ; Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0;";
                x.Dialect<global::NHibernate.Dialect.FirebirdDialect>();
            }
            );

            cfg.AddDeserializedMapping(GetMapping(), null);
            return cfg.BuildSessionFactory();
        }

    }
}
