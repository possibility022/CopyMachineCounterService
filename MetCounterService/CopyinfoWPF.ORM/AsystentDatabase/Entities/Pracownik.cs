namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Pracownik {
        public Pracownik() { }
        public virtual int IdPracownik { get; set; }
        public virtual string Imie { get; set; }
        public virtual string Nazwisko { get; set; }
        public virtual string Telefon { get; set; }
        public virtual string TelefonKom { get; set; }
        public virtual string Email { get; set; }
        public virtual string CzySerwisant { get; set; }
        public virtual string CzySprzedawca { get; set; }
        public virtual string CzyUzytkownik { get; set; }
        public virtual string Uzytkownik { get; set; }
        public virtual string Haslo { get; set; }
        public virtual int? Rola { get; set; }
        public virtual string Pracuje { get; set; }
        public virtual string Blokada { get; set; }
        public virtual string Zalogowany { get; set; }
        public virtual string EdycjaUzytkownikow { get; set; }
        public virtual string DodawanieKlientow { get; set; }
        public virtual string EdycjaKlientow { get; set; }
        public virtual string UsuwanieKlientow { get; set; }
        public virtual string DodawanieZlecen { get; set; }
        public virtual string EdycjaZlecen { get; set; }
        public virtual string UsuwanieZlecen { get; set; }
        public virtual string DodawanieUrzadzen { get; set; }
        public virtual string EdycjaUrzadzen { get; set; }
        public virtual string UsuwanieUrzadzen { get; set; }
        public virtual string PrzekazDoRealiz { get; set; }
        public virtual string ZakZlec { get; set; }
        public virtual string CofanieDoRealiz { get; set; }
        public virtual string CofanieDoPrzyj { get; set; }
        public virtual string DrukowanieList { get; set; }
        public virtual string DodawanieUmow { get; set; }
        public virtual string EdycjaUmow { get; set; }
        public virtual string UsuwanieUmow { get; set; }
        public virtual string DodawanieZamowien { get; set; }
        public virtual string EdycjaZamowien { get; set; }
        public virtual string UsuwanieZamowien { get; set; }
        public virtual string DodawanieSprawCrm { get; set; }
        public virtual string EdycjaSprawCrm { get; set; }
        public virtual string UsuwanieSprawCrm { get; set; }
        public virtual string DodawanieFaktur { get; set; }
        public virtual string EdycjaFaktur { get; set; }
        public virtual string UsuwanieFaktur { get; set; }
        public virtual string Raportowanie { get; set; }
        public virtual string PrzegladKlientow { get; set; }
        public virtual string PrzegladUmow { get; set; }
        public virtual string PrzegladSprawCrm { get; set; }
        public virtual string PrzegladZlecen { get; set; }
        public virtual string PrzegladUrzadzen { get; set; }
        public virtual string PrzegladZamowien { get; set; }
        public virtual string PrzegladFaktur { get; set; }
        public virtual string DodawanieMagazyn { get; set; }
        public virtual string EdycjaMagazyn { get; set; }
        public virtual string UsuwanieMagazyn { get; set; }
        public virtual string PrzegladMagazyn { get; set; }
        public virtual string Aktualizacja { get; set; }
        public virtual string InformatorMagazyn { get; set; }
    }
}
