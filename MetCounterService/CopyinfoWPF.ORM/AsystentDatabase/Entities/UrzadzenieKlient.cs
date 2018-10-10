using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class UrzadzenieKlient {
        public virtual int IdUrzadzenieKlient { get; set; }
        public virtual int IdModelUrzadzenia { get; set; }
        public virtual int IdKlient { get; set; }
        public virtual string NrUrzadzenia { get; set; }
        public virtual int IdMiejsceInstalacji { get; set; }
        public virtual DateTime DataInstalacji { get; set; }
        public virtual int? LicznikInstalacji { get; set; }
        public virtual string NrFabryczny { get; set; }
        public virtual string WyposazenieDodatkowe { get; set; }
        public virtual int? IleMcGwarancja { get; set; }
        public virtual int? IleKopiiGwarancja { get; set; }
        public virtual int? CzestotlPrzeglKopie { get; set; }
        public virtual int? CzestotlPrzeglMc { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual DateTime? DataNastPrzegl { get; set; }
        public virtual DateTime? DataPoprzPrzegl { get; set; }
        public virtual int? LicznikNastPrzegl { get; set; }
        public virtual int? LicznikPoprzPrzegl { get; set; }
        public virtual int? IdSerwis { get; set; }
        public virtual int IdSerwisant { get; set; }
        public virtual int Uzytkownik { get; set; }
        public virtual string OpisCzynnosciPoprz { get; set; }
        public virtual string ZaleceniaSerwisuPoprz { get; set; }
        public virtual string NrGwarancji { get; set; }
        public virtual string ModelUwagi { get; set; }
        public virtual string OpisMiejscaInstalacji { get; set; }
        public virtual int? IdUrzadzenieKlientStatus { get; set; }

        public virtual ModelUrzadzenia ModelUrzadzenia { get; set; }
    }
}
