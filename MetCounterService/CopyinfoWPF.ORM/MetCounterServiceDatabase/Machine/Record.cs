using System;


namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine
{

    public class Record
    {
        public virtual int Id { get; set; }
        public virtual EmailSource EmailSource { get; set; }
        public virtual ServiceSourceCounters ServiceSourceCounters { get; set; }
        public virtual ServiceSourceSerialNumber ServiceSourceSerialNumber { get; set; }
        public virtual int? CounterBlackAndWhite { get; set; }
        public virtual int? CounterColor { get; set; }
        public virtual int? CounterScanner { get; set; }
        public virtual string Description { get; set; }
        public virtual string AddressIp { get; set; }
        public virtual DateTime ReadDatetime { get; set; } = new DateTime();
        public virtual string SerialNumber { get; set; }
        public virtual string TonerLevelBlack { get; set; }
        public virtual string TonerLevelCyan { get; set; }
        public virtual string TonerLevelYellow { get; set; }
        public virtual string TonerLevelMagenta { get; set; }
        public virtual string AddressMac { get; set; }
        public virtual bool Printed { get; set; }
        public virtual int? EmailSourceId { get; set; }
        public virtual int? ServiceSourceSerialNumberId { get; set; }
        public virtual int? ServiceSourceCountersId { get; set; }
    }
}
