namespace CopyinfoWPF.ORM.AsystentDatabase.Address
{
    public class ClientAddress
    {
        public virtual  int ID_ADRES_KLIENT { get; set; }
        public virtual  int ID_KLIENT { get; set; }
        public virtual  int RODZAJ { get; set; }
        public virtual  string ULICA { get; set; }
        public virtual  string NR_DOMU { get; set; }
        public virtual  string NR_LOKALU { get; set; }
        public virtual  string MIEJSCOWOSC { get; set; }
        public virtual  string KOD_POCZT { get; set; }
        public virtual  string POCZTA { get; set; }
        public virtual  string TELEFON { get; set; }
        public virtual  string FAX { get; set; }
        public virtual  string EMAIL { get; set; }
        public virtual  string UWAGI { get; set; }
        public virtual  string ADRES_WWW { get; set; }
        public virtual  string WOJEWODZTWO { get; set; }
    }
}
