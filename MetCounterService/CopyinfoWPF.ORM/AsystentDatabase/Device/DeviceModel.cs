namespace CopyinfoWPF.ORM.AsystentDatabase.Device
{
    public class DeviceModel
    {
        public virtual int ID_MODEL_URZADZENIA { get; set; }
        public virtual int ID_MARKA_URZADZENIA { get; set; }  // int; //fk
        public virtual int ID_RODZAJ_URZADZENIA { get; set; } // int; //fk
        public virtual string NAZWA_1 { get; set; } // not null
        public virtual object UWAGI { get; set; }   // ignore BLOB SUB_TYPE 1
    }
}
