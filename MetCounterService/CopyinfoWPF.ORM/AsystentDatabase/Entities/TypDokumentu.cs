namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class TypDokumentu {
        public virtual int IdTypDokumentu { get; set; }
        public virtual RodzajDokumentu RodzajDokumentu { get; set; }
        public virtual Magazyn Magazyn { get; set; }
        public virtual string TypSkr { get; set; }
        public virtual string TypNazwa { get; set; }
        public virtual string Autonumeracja { get; set; }
        public virtual int? OstatniNr { get; set; }
        public virtual int? OstatniNrKorekta { get; set; }
        public virtual string Sciezka { get; set; }
        public virtual string SciezkaKorekta { get; set; }
        public virtual string CzyAktywny { get; set; }
        public virtual int? ZeraWiodace { get; set; }
        public virtual string TylkoDoOdczytu { get; set; }
        public virtual string TypSkrMagazynowy { get; set; }
        public virtual string TypNazwaMagazynowy { get; set; }
        public virtual string TworzDokMagazynowy { get; set; }
        public virtual int? OstatniNrMagazynowy { get; set; }
        public virtual int? OstatniNrNota { get; set; }
        public virtual int? IdPoziomyCen { get; set; }
    }
}
