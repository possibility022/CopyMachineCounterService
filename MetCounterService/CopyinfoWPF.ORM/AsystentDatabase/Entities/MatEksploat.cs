namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class MatEksploat
    {
        public virtual int IdMatEksploat { get; set; }
        public virtual ModelUrzadzenia ModelUrzadzenia { get; set; }
        public virtual Towar Towar { get; set; }
    }
}
