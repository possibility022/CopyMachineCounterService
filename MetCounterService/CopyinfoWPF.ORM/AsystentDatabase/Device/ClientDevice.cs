using System;

namespace CopyinfoWPF.ORM.AsystentDatabase.Device
{
    public class ClientDevice
    {
        public virtual int ID_URZADZENIE_KLIENT { get; set; }
        public virtual int ID_MODEL_URZADZENIA { get; set; }
        public virtual int ID_KLIENT { get; set; }
        public virtual string NR_URZADZENIA { get; set; }
        public virtual int ID_MIEJSCE_INSTALACJI { get; set; }
        public virtual DateTime DATA_INSTALACJI { get; set; }
        public virtual int? LICZNIK_INSTALACJI { get; set; }
        public virtual string NR_FABRYCZNY { get; set; }
        public virtual object WYPOSAZENIE_DODATKOWE { get; set; } //ignore?
        public virtual int? ILE_MC_GWARANCJA { get; set; }
        public virtual int? ILE_KOPII_GWARANCJA { get; set; }
        public virtual int? CZESTOTL_PRZEGL_KOPIE { get; set; }
        public virtual int? CZESTOTL_PRZEGL_MC { get; set; }
        public virtual object UWAGI { get; set; } //ignore?
        public virtual DateTime DATA_NAST_PRZEGL { get; set; }
        public virtual DateTime DATA_POPRZ_PRZEGL { get; set; }
        public virtual int? LICZNIK_NAST_PRZEGL { get; set; }
        public virtual int? LICZNIK_POPRZ_PRZEGL { get; set; }
        public virtual int? ID_SERWIS { get; set; }
        public virtual int? ID_SERWISANT { get; set; }
        public virtual int? UZYTKOWNIK { get; set; }
        public virtual int? OPIS_CZYNNOSCI_POPRZ { get; set; }
        public virtual object ZALECENIA_SERWISU_POPRZ { get; set; } //ignore?
        public virtual string NR_GWARANCJI { get; set; }
        public virtual object MODEL_UWAGI { get; set; } //ignore?
        public virtual object OPIS_MIEJSCA_INSTALACJI { get; set; } //ignore?
        public virtual int? ID_URZADZENIE_KLIENT_STATUS { get; set; }
    }
}
