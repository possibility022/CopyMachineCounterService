namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class ModelUrzadzenia {
        public ModelUrzadzenia() { }
        public virtual int IdModelUrzadzenia { get; set; }
        public virtual MarkaUrzadzenia MarkaUrzadzenia { get; set; }
        public virtual RodzajUrzadzenia RodzajUrzadzenia { get; set; }
        public virtual string Nazwa1 { get; set; }
        public virtual string Uwagi { get; set; }
    }
}
