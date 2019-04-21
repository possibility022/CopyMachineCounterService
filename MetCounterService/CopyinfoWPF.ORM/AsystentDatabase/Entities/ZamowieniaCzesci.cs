using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class ZamowieniaCzesci {
        public virtual int IdZamowieniaCzesci { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual Dostawcy Dostawcy { get; set; }
        public virtual int? IdTowar { get; set; }
        public virtual string Nazwa1 { get; set; }
        public virtual string Nazwa2 { get; set; }
        public virtual string Uwagi1 { get; set; }
        public virtual string Uwagi2 { get; set; }
        public virtual string Kod { get; set; }
        public virtual string SymbolOem { get; set; }
        public virtual DateTime? DataDodaniaZam { get; set; }
        public virtual string Przeznaczenie { get; set; }
        public virtual DateTime? DataZlozeniaZam { get; set; }
        public virtual DateTime? PlanTermDostawy { get; set; }
        public virtual int? Status { get; set; }
        public virtual DateTime? NaKiedy { get; set; }
        public virtual string Opis { get; set; }
        public virtual DateTime? GodzDodaniaZam { get; set; }
        public virtual DateTime? GodzZlozeniaZam { get; set; }
        public virtual string ZamawiajacyImie { get; set; }
        public virtual string ZamawiajacyNazwisko { get; set; }
        public virtual int? IdZamawiajacy { get; set; }
        public virtual double? Ilosc { get; set; }
    }
}
