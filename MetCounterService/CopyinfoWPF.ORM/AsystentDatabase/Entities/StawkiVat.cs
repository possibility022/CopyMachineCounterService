namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class StawkiVat {
        public StawkiVat() { }
        public virtual int IdStawkaVat { get; set; }
        public virtual string Identyfikator { get; set; }
        public virtual int? Stawka { get; set; }
        public virtual string Opis { get; set; }
        public virtual string CzyZwolniony { get; set; }
    }
}
