using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class ZlecenieSerwisowe {
        public ZlecenieSerwisowe() { }
        public virtual int IdZlecenieSerwisowe { get; set; }
        public virtual int IdUrzadzenieKlient { get; set; }
        public virtual string NrZlecenia { get; set; }
        public virtual DateTime? DataPrzyjeciaZlec { get; set; }
        public virtual DateTime? DataWykonaniaZlec { get; set; }
        public virtual DateTime? DataZamknieciaZlec { get; set; }
        public virtual DateTime? DataZgloszenia { get; set; }
        public virtual string OsobaZglaszajaca { get; set; }
        public virtual string ZglaszanaUsterka { get; set; }
        public virtual string OpisCzynnosciSerwisowych { get; set; }
        public virtual string ZaleceniaSerwisu { get; set; }
        public virtual int IdKlient { get; set; }
        public virtual int IdModelUrzadzenia { get; set; }
        public virtual int IdMiejsceInstalacji { get; set; }
        public virtual string NrUrzadzenia { get; set; }
        public virtual string NrFabryczny { get; set; }
        public virtual int? IdSerwis { get; set; }
        public virtual DateTime? DataNastPrzegl { get; set; }
        public virtual int? LicznikNastPrzegl { get; set; }
        public virtual string CzyAktualizowanoStanUrz { get; set; }
        public virtual int? LicznikBiezacy { get; set; }
        public virtual DateTime? DataKoncaGwarancji { get; set; }
        public virtual int? LicznikKoncaGwarancji { get; set; }
        public virtual string CzyJestGwarancja { get; set; }
        public virtual int? LicznikPoprzPrzegladu { get; set; }
        public virtual int? Priorytet { get; set; }
        public virtual int? Uzytkownik { get; set; }
        //public virtual DateTime? GodzinaPrzyjeciaZlec { get; set; } // In Assystent database it is a "TIME" type. Don't know how to map it.
        //public virtual DateTime? GodzinaWykonaniaZlec { get; set; }
        //public virtual DateTime? GodzinaZamknieciaZlec { get; set; }
        //public virtual DateTime? GodzinaZgloszenia { get; set; }
        public virtual string OpisCzynnosciPoprz { get; set; }
        public virtual string ZaleceniaSerwisuPoprz { get; set; }
        public virtual string NrFakturyZewn { get; set; }
        public virtual DateTime? DataPoprzPrzegladu { get; set; }
        public virtual DateTime? PrzewidDataZakoncz { get; set; }
        public virtual string CzySerwisPlanowany { get; set; }
        public virtual int? LicznikZglaszany { get; set; }
        public virtual string Notatka { get; set; }
        public virtual int? IdSerwisant { get; set; }
        public virtual int? IdStatusZlecenia { get; set; }
        public virtual int? IdRodzajZlecenia { get; set; }
        public virtual int? IdTypZlecenia { get; set; }
        public virtual int? IdSposobZglZlecenia { get; set; }
        public virtual int? IdPoziomyCen { get; set; }
    }
}
