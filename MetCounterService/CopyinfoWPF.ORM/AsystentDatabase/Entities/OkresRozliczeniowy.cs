using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class OkresRozliczeniowy {
        public virtual int IdOkresRozliczeniowy { get; set; }
        public virtual string Identyfikator { get; set; }
        public virtual string Opis { get; set; }
        public virtual DateTime? DataOd { get; set; }
        public virtual DateTime? DataDo { get; set; }
        public virtual string Status { get; set; }
    }
}
