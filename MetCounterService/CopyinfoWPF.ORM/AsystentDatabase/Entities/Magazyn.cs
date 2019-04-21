namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Magazyn {
        public Magazyn() { }
        public virtual int IdMagazyn { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string Nazwa { get; set; }
        public virtual string Opis { get; set; }
        public virtual string Ulica { get; set; }
        public virtual string NrDomu { get; set; }
        public virtual string NrLokalu { get; set; }
        public virtual string Miejscowosc { get; set; }
        public virtual string Telefon { get; set; }
        public virtual string CzyGlowny { get; set; }
    }
}
