namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Klient {
        public Klient() { }
        public virtual int IdKlient { get; set; }
        public virtual int IdSerwis { get; set; }
        public virtual int IdGrupaKlient { get; set; }
        public virtual int IdHandlowiec { get; set; }
        public virtual string Nip { get; set; }
        public virtual string NazwaSkr { get; set; }
        public virtual string Nazwa2 { get; set; }
        public virtual string Opis { get; set; }
        public virtual string KodUzytkownika { get; set; }
        public virtual string Ulica { get; set; }
        public virtual string NrDomu { get; set; }
        public virtual string NrLokalu { get; set; }
        public virtual string Miejscowosc { get; set; }
        public virtual string KodPoczt { get; set; }
        public virtual string Poczta { get; set; }
        public virtual string CzyDostawca { get; set; }
        public virtual string CzyOdbiorca { get; set; }
        public virtual int? IdKlientStatus { get; set; }
        public virtual string Telefon { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual string Nazwa1 { get; set; }
        public virtual string Regon { get; set; }
        public virtual string AdresWww { get; set; }
        public virtual int? IdTypPlatnosci { get; set; }
        public virtual string Wojewodztwo { get; set; }
        public virtual string SposobRozlVat { get; set; }
        public virtual int? IdPoziomyCen { get; set; }

        public bool UmowaSerwisowa { get; set; } = false;// this is not a property for map.
    }
}
