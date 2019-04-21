namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class TypPlatnosci {
        public TypPlatnosci() { }
        public virtual int IdTypPlatnosci { get; set; }
        public virtual string Identyfikator { get; set; }
        public virtual int? IleDni { get; set; }
        public virtual string Opis { get; set; }
        public virtual string TylkoDoOdczytu { get; set; }
    }
}
