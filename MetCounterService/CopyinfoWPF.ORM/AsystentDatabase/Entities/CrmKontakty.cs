using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class CrmKontakty {
        public virtual int IdCrmKontakty { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual int? IdKlient { get; set; }
        public virtual string NazwaSkrKl { get; set; }
        public virtual string Nazwa1Kl { get; set; }
        public virtual string Nazwa2Kl { get; set; }
        public virtual string NipKl { get; set; }
        public virtual string OpisKl { get; set; }
        public virtual string UlicaKl { get; set; }
        public virtual string NrDomuKl { get; set; }
        public virtual string NrLokaluKl { get; set; }
        public virtual string MiejscowoscKl { get; set; }
        public virtual string KodPocztKl { get; set; }
        public virtual string PocztaKl { get; set; }
        public virtual string TelefonKl { get; set; }
        public virtual string FaxKl { get; set; }
        public virtual string EmailKl { get; set; }
        public virtual DateTime? DataDodaniaCrm { get; set; }
        public virtual DateTime? GodzDodaniaCrm { get; set; }
        public virtual int? StatusSprawy { get; set; }
        public virtual string Opis { get; set; }
        public virtual string NazwaSprawy { get; set; }
        public virtual DateTime? DataKontaktu { get; set; }
        public virtual int? RodzajSprawy { get; set; }
        public virtual string Tresc { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual int? RodzajKontaktu { get; set; }
    }
}
