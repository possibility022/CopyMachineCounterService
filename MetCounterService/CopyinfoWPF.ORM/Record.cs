using System;


namespace CopyinfoWPF.ORM
{

    public class Record {
        public virtual int Id { get; set; }
        public virtual Emailsource Emailsource { get; set; }
        public virtual Servicesourcecounters Servicesourcecounters { get; set; }
        public virtual Servicesourceserialnumber Servicesourceserialnumber { get; set; }
        public virtual int? Counterblackandwhite { get; set; }
        public virtual int? Countercolor { get; set; }
        public virtual int? Counterscanner { get; set; }
        public virtual string Description { get; set; }
        public virtual string Addressip { get; set; }
        public virtual DateTime Readdatetime { get; set; }
        public virtual string Serialnumber { get; set; }
        public virtual string Tonerlevelblack { get; set; }
        public virtual string Tonerlevelcyan { get; set; }
        public virtual string Tonerlevelyellow { get; set; }
        public virtual string Tonerlevelmagenta { get; set; }
        public virtual string Addressmac { get; set; }
    }
}
