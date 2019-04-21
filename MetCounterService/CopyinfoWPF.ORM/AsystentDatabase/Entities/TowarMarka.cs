namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class TowarMarka {
        public virtual int IdTowarMarka { get; set; }
        public virtual Towar Towar { get; set; }
        public virtual MarkaUrzadzenia MarkaUrzadzenia { get; set; }
    }
}
