using System;


namespace CopyinfoWPF.ORM.AsystentDatabase.Entities
{

    public class Blokady {
        public virtual int IdRekordu { get; set; }
        public virtual string Uzytkownik { get; set; }
        public virtual string NazwaTabeli { get; set; }
        public virtual DateTime DataBlokady { get; set; }
        public virtual DateTime GodzinaBlokady { get; set; }
    }
}
