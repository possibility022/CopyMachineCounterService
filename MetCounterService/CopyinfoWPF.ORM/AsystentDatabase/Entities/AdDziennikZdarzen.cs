using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class AdDziennikZdarzen {
        public virtual int DznId { get; set; }
        public virtual AdZdarzenia AdZdarzenia { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Czas { get; set; }
        public virtual int? UzyId { get; set; }
        public virtual string Status { get; set; }
        public virtual string Informacje { get; set; }
    }
}
