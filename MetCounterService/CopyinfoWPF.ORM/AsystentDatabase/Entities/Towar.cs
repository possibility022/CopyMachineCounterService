namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Towar {
        public Towar() { }
        public virtual int IdTowar { get; set; }
        public virtual StawkiVat StawkiVat { get; set; }
        public virtual JednostkiMiary JednostkiMiary { get; set; }
        public virtual SymbolePkwiu SymbolePkwiu { get; set; }
        public virtual string SymbolOem { get; set; }
        public virtual int? WydajnoscMatEkspl { get; set; }
        public virtual string Nazwa2 { get; set; }
        public virtual int? IdSerwis { get; set; }
        public virtual double? Netto { get; set; }
        public virtual double? Brutto { get; set; }
        public virtual double? Ilosc { get; set; }
        public virtual string Typ { get; set; }
        public virtual string Uwagi1 { get; set; }
        public virtual string Uwagi2 { get; set; }
        public virtual string Kod { get; set; }
        public virtual string Identyfikator { get; set; }
        public virtual double? BruttoDostawcy { get; set; }
        public virtual double? NettoDostawcy { get; set; }
        public virtual int? ZaokraglajDo { get; set; }
        public virtual int? Marza { get; set; }
        public virtual double? Narzut { get; set; }
        public virtual string MarzaCzyNarzut { get; set; }
        public virtual string Nazwa1 { get; set; }
        public virtual int? NarzutProc { get; set; }
    }
}
