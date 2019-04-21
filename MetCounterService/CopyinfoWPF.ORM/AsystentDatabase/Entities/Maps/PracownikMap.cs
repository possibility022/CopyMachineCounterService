using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities.Maps
{


    public class PracownikMap : ClassMapping<Pracownik> {
        
        public PracownikMap() {
			
			
			Id(x => x.IdPracownik, map => { map.Column("ID_PRACOWNIK"); map.Generator(Generators.Assigned); });
			Property(x => x.Imie, map => map.NotNullable(true));
			Property(x => x.Nazwisko, map => map.NotNullable(true));
			Property(x => x.Telefon);
			Property(x => x.TelefonKom, map => map.Column("TELEFON_KOM"));
			Property(x => x.Email);
			Property(x => x.CzySerwisant, map => map.Column("CZY_SERWISANT"));
			Property(x => x.CzySprzedawca, map => map.Column("CZY_SPRZEDAWCA"));
			Property(x => x.CzyUzytkownik, map => map.Column("CZY_UZYTKOWNIK"));
			Property(x => x.Uzytkownik, map => map.NotNullable(true));
			Property(x => x.Haslo);
			Property(x => x.Rola);
			Property(x => x.Pracuje);
			Property(x => x.Blokada);
			Property(x => x.Zalogowany);
			Property(x => x.EdycjaUzytkownikow, map => map.Column("EDYCJA_UZYTKOWNIKOW"));
			Property(x => x.DodawanieKlientow, map => map.Column("DODAWANIE_KLIENTOW"));
			Property(x => x.EdycjaKlientow, map => map.Column("EDYCJA_KLIENTOW"));
			Property(x => x.UsuwanieKlientow, map => map.Column("USUWANIE_KLIENTOW"));
			Property(x => x.DodawanieZlecen, map => map.Column("DODAWANIE_ZLECEN"));
			Property(x => x.EdycjaZlecen, map => map.Column("EDYCJA_ZLECEN"));
			Property(x => x.UsuwanieZlecen, map => map.Column("USUWANIE_ZLECEN"));
			Property(x => x.DodawanieUrzadzen, map => map.Column("DODAWANIE_URZADZEN"));
			Property(x => x.EdycjaUrzadzen, map => map.Column("EDYCJA_URZADZEN"));
			Property(x => x.UsuwanieUrzadzen, map => map.Column("USUWANIE_URZADZEN"));
			Property(x => x.PrzekazDoRealiz, map => map.Column("PRZEKAZ_DO_REALIZ"));
			Property(x => x.ZakZlec, map => map.Column("ZAK_ZLEC"));
			Property(x => x.CofanieDoRealiz, map => map.Column("COFANIE_DO_REALIZ"));
			Property(x => x.CofanieDoPrzyj, map => map.Column("COFANIE_DO_PRZYJ"));
			Property(x => x.DrukowanieList, map => map.Column("DRUKOWANIE_LIST"));
			Property(x => x.DodawanieUmow, map => map.Column("DODAWANIE_UMOW"));
			Property(x => x.EdycjaUmow, map => map.Column("EDYCJA_UMOW"));
			Property(x => x.UsuwanieUmow, map => map.Column("USUWANIE_UMOW"));
			Property(x => x.DodawanieZamowien, map => map.Column("DODAWANIE_ZAMOWIEN"));
			Property(x => x.EdycjaZamowien, map => map.Column("EDYCJA_ZAMOWIEN"));
			Property(x => x.UsuwanieZamowien, map => map.Column("USUWANIE_ZAMOWIEN"));
			Property(x => x.DodawanieSprawCrm, map => map.Column("DODAWANIE_SPRAW_CRM"));
			Property(x => x.EdycjaSprawCrm, map => map.Column("EDYCJA_SPRAW_CRM"));
			Property(x => x.UsuwanieSprawCrm, map => map.Column("USUWANIE_SPRAW_CRM"));
			Property(x => x.DodawanieFaktur, map => map.Column("DODAWANIE_FAKTUR"));
			Property(x => x.EdycjaFaktur, map => map.Column("EDYCJA_FAKTUR"));
			Property(x => x.UsuwanieFaktur, map => map.Column("USUWANIE_FAKTUR"));
			Property(x => x.Raportowanie);
			Property(x => x.PrzegladKlientow, map => map.Column("PRZEGLAD_KLIENTOW"));
			Property(x => x.PrzegladUmow, map => map.Column("PRZEGLAD_UMOW"));
			Property(x => x.PrzegladSprawCrm, map => map.Column("PRZEGLAD_SPRAW_CRM"));
			Property(x => x.PrzegladZlecen, map => map.Column("PRZEGLAD_ZLECEN"));
			Property(x => x.PrzegladUrzadzen, map => map.Column("PRZEGLAD_URZADZEN"));
			Property(x => x.PrzegladZamowien, map => map.Column("PRZEGLAD_ZAMOWIEN"));
			Property(x => x.PrzegladFaktur, map => map.Column("PRZEGLAD_FAKTUR"));
			Property(x => x.DodawanieMagazyn, map => map.Column("DODAWANIE_MAGAZYN"));
			Property(x => x.EdycjaMagazyn, map => map.Column("EDYCJA_MAGAZYN"));
			Property(x => x.UsuwanieMagazyn, map => map.Column("USUWANIE_MAGAZYN"));
			Property(x => x.PrzegladMagazyn, map => map.Column("PRZEGLAD_MAGAZYN"));
			Property(x => x.Aktualizacja);
			Property(x => x.InformatorMagazyn, map => map.Column("INFORMATOR_MAGAZYN"));
			//Bag(x => x.CrmKontakty, colmap =>  { colmap.Key(x => x.Column("DODAJACY_CRM")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.TowarRezerwacja, colmap =>  { colmap.Key(x => x.Column("ID_PRACOWNIK")); colmap.Inverse(true); }, map => { map.OneToMany(); });
			//Bag(x => x.ZamowieniaCzesci, colmap =>  { colmap.Key(x => x.Column("DODAJACY_ZAM")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
