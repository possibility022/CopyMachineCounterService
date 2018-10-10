namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Dostawcy {
        public Dostawcy() { }
        public virtual int IdDostawcy { get; set; }
        public virtual Serwis Serwis { get; set; }
        public virtual string NazwaSkr { get; set; }
        public virtual string Nazwa1 { get; set; }
        public virtual string Nazwa2 { get; set; }
        public virtual string Nazwa3 { get; set; }
        public virtual string KodPocztowy { get; set; }
        public virtual string Miejscowosc { get; set; }
        public virtual string Ulica { get; set; }
        public virtual string NrDomu { get; set; }
        public virtual string NrLokalu { get; set; }
        public virtual string Wojewodztwo { get; set; }
        public virtual int? Rabat { get; set; }
        public virtual string Nip { get; set; }
        public virtual string NrRachunku { get; set; }
        public virtual string NazwaBanku { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual string Oddzial { get; set; }
        public virtual string Regon { get; set; }
        public virtual string OsobaPodpisujaca { get; set; }
        public virtual string PeselOf { get; set; }
        public virtual string DokumentOf { get; set; }
        public virtual string NrDokumentuOf { get; set; }
        public virtual string WydanyPrzezOf { get; set; }
    }
}
