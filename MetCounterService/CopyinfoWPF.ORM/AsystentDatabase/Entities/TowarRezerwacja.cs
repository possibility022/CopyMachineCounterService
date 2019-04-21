namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class TowarRezerwacja {
        public virtual int IdTowarRezerwacja { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual Towar Towar { get; set; }
        public virtual Magazyn Magazyn { get; set; }
        public virtual double? IloscRezerwacja { get; set; }
    }
}
