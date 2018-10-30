using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class UmowaSerwisowa {
        public UmowaSerwisowa() { }
        public virtual int IdUmowaSerwisowa { get; set; }
        public virtual int IdKlient { get; set; }
        public virtual int IdUzytkownik { get; set; }
        public virtual string NrUmowy { get; set; }
        public virtual DateTime? DataZawarcia { get; set; }
        public virtual DateTime? DataRozpoczecia { get; set; }
        public virtual DateTime? DataZakonczenia { get; set; }
        public virtual int? RodzajUmowy { get; set; }
        public virtual int? Status { get; set; }
        public virtual int? CzestotRozlicz { get; set; }
        public virtual DateTime? DataNastRozlicz { get; set; }
        public virtual string SzczegoloweWarunki { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual int? ReprezentatSprz { get; set; }
        public virtual int? ReprezentantKl { get; set; }
        public virtual int? Handlowiec { get; set; }
    }
}
