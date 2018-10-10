namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class AdresKlient {
        public virtual int IdAdresKlient { get; set; }
        public virtual int IdKlient { get; set; }
        public virtual string Rodzaj { get; set; }
        public virtual string Ulica { get; set; }
        public virtual string NrDomu { get; set; }
        public virtual string NrLokalu { get; set; }
        public virtual string Miejscowosc { get; set; }
        public virtual string KodPoczt { get; set; }
        public virtual string Poczta { get; set; }
        public virtual string Telefon { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual string AdresWww { get; set; }
        public virtual string Wojewodztwo { get; set; }
    }
}
