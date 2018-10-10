namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Notatki {
        public virtual int IdNotatki { get; set; }
        public virtual int? IdZewnetrzne { get; set; }
        public virtual string NazwaTabeli { get; set; }
        public virtual string Tresc { get; set; }
    }
}
