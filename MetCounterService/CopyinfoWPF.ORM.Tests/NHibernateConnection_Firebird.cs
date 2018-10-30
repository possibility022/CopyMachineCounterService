using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.ConfigurationSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace CopyinfoWPF.ORM.Tests
{
    [TestClass]
    public class NHibernateConnection_Firebird
    {
        private static ISessionFactory SessionFactory { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SessionFactory = AsystentFactorySettings.GetSessionFactory();
        }

        [TestMethod]
        public void TestConnection()
        {
            bool isConnected = false;

            using (var session = SessionFactory.OpenSession())
            {
                isConnected = session.IsConnected;
            }

            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public void TestConnection_GetOneRecord()
        {
            var list = new List<object>();

            using (var session = SessionFactory.OpenSession()) // Comented out lines have empty table rows in database.
            {
                list.Add(session.Query<AdDziennikZdarzen>().FirstOrDefault());
                list.Add(session.Query<AdresKlient>().FirstOrDefault());
                list.Add(session.Query<AdZdarzenia>().FirstOrDefault());
                //list.Add(session.Query<Blokady>().FirstOrDefault());
                list.Add(session.Query<CrmFormyKomunikacji>().FirstOrDefault());
                list.Add(session.Query<CrmKategoriaSpr>().FirstOrDefault());
                //list.Add(session.Query<CrmKontakty>().FirstOrDefault());
                list.Add(session.Query<CrmRodzajZadania>().FirstOrDefault());
                //list.Add(session.Query<Dostawcy>().FirstOrDefault());
                list.Add(session.Query<GrupaKlient>().FirstOrDefault());
                list.Add(session.Query<JakieRozliczenie>().FirstOrDefault());
                list.Add(session.Query<JednostkiMiary>().FirstOrDefault());
                list.Add(session.Query<KlientStatus>().FirstOrDefault());
                list.Add(session.Query<Magazyn>().FirstOrDefault());
                list.Add(session.Query<MarkaUrzadzenia>().FirstOrDefault());
                list.Add(session.Query<MatEksploat>().FirstOrDefault());
                list.Add(session.Query<ModelUrzadzenia>().FirstOrDefault());
                list.Add(session.Query<Notatki>().FirstOrDefault());
                list.Add(session.Query<OkresRozliczeniowy>().FirstOrDefault());
                list.Add(session.Query<Pracownik>().FirstOrDefault());
                list.Add(session.Query<PrzypomnCykl>().FirstOrDefault());
                //list.Add(session.Query<PrzypomnOdloz>().FirstOrDefault());
                list.Add(session.Query<RodzajDokumentu>().FirstOrDefault());
                list.Add(session.Query<RodzajUrzadzenia>().FirstOrDefault());
                list.Add(session.Query<RodzajZlecenia>().FirstOrDefault());
                list.Add(session.Query<Serwis>().FirstOrDefault());
                list.Add(session.Query<SposobZglZlecenia>().FirstOrDefault());
                list.Add(session.Query<StatusCrmSprawy>().FirstOrDefault());
                list.Add(session.Query<StatusCrmZadania>().FirstOrDefault());
                list.Add(session.Query<StatusPrzypomnienia>().FirstOrDefault());
                list.Add(session.Query<StatusUmowy>().FirstOrDefault());
                list.Add(session.Query<StatusZlecenia>().FirstOrDefault());
                list.Add(session.Query<StawkiVat>().FirstOrDefault());
                //list.Add(session.Query<SymbolePkwiu>().FirstOrDefault());
                list.Add(session.Query<Towar>().FirstOrDefault());
                list.Add(session.Query<TowarMarka>().FirstOrDefault());
                //list.Add(session.Query<TowarRezerwacja>().FirstOrDefault());
                list.Add(session.Query<Transport>().FirstOrDefault());
                list.Add(session.Query<TypDokumentu>().FirstOrDefault());
                list.Add(session.Query<TypPlatnosci>().FirstOrDefault());
                list.Add(session.Query<TypZlecenia>().FirstOrDefault());
                list.Add(session.Query<UrzadzenieKlient>().FirstOrDefault());
                list.Add(session.Query<UrzadzenieKlientStatus>().FirstOrDefault());
                list.Add(session.Query<Waluty>().FirstOrDefault());
                list.Add(session.Query<UmowaSerwisowa>().FirstOrDefault());
                //list.Add(session.Query<WersjaBazy>().FirstOrDefault()); // In this case, we have to do something with mapping. Will return an error. Is adding ID to Select even when there is no ID in mapping?
                //list.Add(session.Query<ZamowieniaCzesci>().FirstOrDefault());
                list.Add(session.Query<Klient>().FirstOrDefault());
                list.Add(session.Query<ZlecenieSerwisowe>().FirstOrDefault());
            }

            Assert.IsFalse(list.Any(o => o == null));
                
        }

        [TestMethod]
        public void BrowseRecordsUrzadzenieKlients()
        {
            List<UrzadzenieKlient> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<UrzadzenieKlient>().Take(100).ToList();
            }

            Trace.Write(list.Count);
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsUrzadzenieKlients_CheckMappingModelUrzadzenia()
        {
            List<UrzadzenieKlient> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<UrzadzenieKlient>().Take(100).ToList();
            }

            Assert.IsTrue(list.All(a => a.ModelUrzadzenia != null));
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsModelUrzadzenia_MarkaUrzadzenia()
        {
            List<ModelUrzadzenia> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<ModelUrzadzenia>().Take(100).ToList();
            }

            Assert.IsTrue(list.All(a => a.MarkaUrzadzenia != null));
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsModelUrzadzenia()
        {
            List<ModelUrzadzenia> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<ModelUrzadzenia>().Take(100).ToList();
            }

            Trace.Write(list.Count);
            //(new System.Xml.Serialization.XmlSerializer(list.GetType())).Serialize(new System.IO.StreamWriter(@"d:\tmp\text2.xml"), list);
        }

        [TestMethod]
        public void BrowseRecordsMarkaUrzadzenia()
        {
            List<MarkaUrzadzenia> list;
            using (var session = SessionFactory.OpenSession())
            {
                list = session.Query<MarkaUrzadzenia>().Take(100).ToList();
            }

            Trace.Write(list.Count);
        }


    }
}
