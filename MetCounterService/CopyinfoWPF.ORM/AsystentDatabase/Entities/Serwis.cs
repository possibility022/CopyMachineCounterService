using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Serwis {
        public Serwis() { }
        public virtual int IdSerwis { get; set; }
        public virtual Transport Transport { get; set; }
        public virtual TypPlatnosci TypPlatnosci { get; set; }
        public virtual Waluty Waluty { get; set; }
        public virtual StawkiVat StawkiVat { get; set; }
        public virtual JednostkiMiary JednostkiMiary { get; set; }
        public virtual GrupaKlient GrupaKlient { get; set; }
        public virtual KlientStatus KlientStatus { get; set; }
        public virtual string Nip { get; set; }
        public virtual string NazwaSkr { get; set; }
        public virtual string Nazwa1 { get; set; }
        public virtual string Nazwa2 { get; set; }
        public virtual string Miejscowosc { get; set; }
        public virtual string Ulica { get; set; }
        public virtual string NrDomu { get; set; }
        public virtual string NrLokalu { get; set; }
        public virtual string KodPocztowy { get; set; }
        public virtual string Poczta { get; set; }
        public virtual string EMail { get; set; }
        public virtual string Logo { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual string Opis { get; set; }
        public virtual string KodAktywacyjny { get; set; }
        public virtual DateTime? DataInstalacji { get; set; }
        public virtual string BlokadaDemo { get; set; }
        public virtual string CzyAktywnyAutonumer { get; set; }
        public virtual string Autonumeracja { get; set; }
        public virtual string AutonumeracjaBlob { get; set; }
        public virtual int? OstatniNr { get; set; }
        public virtual int? ZeraWiodace { get; set; }
        public virtual string CzyAutonumerUrz { get; set; }
        public virtual string AutonumeracjaUrz { get; set; }
        public virtual string AutonumeracjaBlobUrz { get; set; }
        public virtual int? OstatniNrUrz { get; set; }
        public virtual int? ZeraWiodaceUrz { get; set; }
        public virtual string CzyInfoZlecen { get; set; }
        public virtual int? IleDniInfoZlecen { get; set; }
        public virtual int? IleDniInfoSerwisow { get; set; }
        public virtual string CzyAutonumerUmow { get; set; }
        public virtual string AutonumeracjaUmow { get; set; }
        public virtual string AutonumeracjaBlobUmow { get; set; }
        public virtual int? OstatniNrUmow { get; set; }
        public virtual int? ZeraWiodaceUmow { get; set; }
        public virtual string CzyAutonumerZlecJedn { get; set; }
        public virtual string AutonumeracjaZlecJedn { get; set; }
        public virtual string AutonumeracjaBlobZlecJedn { get; set; }
        public virtual int? OstatniNrZlecJedn { get; set; }
        public virtual int? ZeraWiodaceZlecJedn { get; set; }
        public virtual string Telefon { get; set; }
        public virtual string CzyAutonumerSpr { get; set; }
        public virtual string AutonumeracjaSpr { get; set; }
        public virtual string AutonumeracjaBlobSpr { get; set; }
        public virtual int? OstatniNrSpr { get; set; }
        public virtual int? ZeraWiodaceSpr { get; set; }
        public virtual string JakieSpr { get; set; }
        public virtual string SposobRozlVat { get; set; }
        public virtual string JakieDoc { get; set; }
        public virtual string CenaNazwa1 { get; set; }
        public virtual double? CenaDomNarzut1 { get; set; }
        public virtual string CenaCzyStala1 { get; set; }
        public virtual string CenaNazwa2 { get; set; }
        public virtual double? CenaDomNarzut2 { get; set; }
        public virtual string CenaCzyStala2 { get; set; }
        public virtual string CenaNazwa3 { get; set; }
        public virtual double? CenaDomNarzut3 { get; set; }
        public virtual string CenaCzyStala3 { get; set; }
        public virtual string CenaNazwa4 { get; set; }
        public virtual double? CenaDomNarzut4 { get; set; }
        public virtual string CenaCzyStala4 { get; set; }
        public virtual string CenaNazwa5 { get; set; }
        public virtual double? CenaDomNarzut5 { get; set; }
        public virtual string CenaCzyStala5 { get; set; }
        public virtual string CenaNazwa6 { get; set; }
        public virtual double? CenaDomNarzut6 { get; set; }
        public virtual string CenaCzyStala6 { get; set; }
        public virtual string CenaNazwa7 { get; set; }
        public virtual double? CenaDomNarzut7 { get; set; }
        public virtual string CenaCzyStala7 { get; set; }
        public virtual string CenaNazwa8 { get; set; }
        public virtual double? CenaDomNarzut8 { get; set; }
        public virtual string CenaCzyStala8 { get; set; }
        public virtual string CenaNazwa9 { get; set; }
        public virtual double? CenaDomNarzut9 { get; set; }
        public virtual string CenaCzyStala9 { get; set; }
        public virtual string CenaNazwa10 { get; set; }
        public virtual double? CenaDomNarzut10 { get; set; }
        public virtual string CenaCzyStala10 { get; set; }
    }
}
