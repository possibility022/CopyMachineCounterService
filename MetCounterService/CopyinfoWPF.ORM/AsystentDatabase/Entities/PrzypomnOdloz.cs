using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class PrzypomnOdloz {
        public virtual int IdPrzypomnOdloz { get; set; }
        public virtual string Tabela { get; set; }
        public virtual int? Wiersz { get; set; }
        public virtual DateTime? DataOdloz { get; set; }
        public virtual int? IdPracownik { get; set; }
    }
}
