using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine.Maps
{
    public class RecordMap : ClassMapping<Record>
    {

        public RecordMap()
        {
            Schema("Machine");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.CounterBlackAndWhite);
            Property(x => x.CounterColor);
            Property(x => x.CounterScanner);
            Property(x => x.Description);
            Property(x => x.AddressIp);
            Property(x => x.ReadDatetime, map => map.NotNullable(true));
            Property(x => x.SerialNumber, map => map.NotNullable(true));
            Property(x => x.TonerLevelBlack);
            Property(x => x.TonerLevelCyan);
            Property(x => x.TonerLevelYellow);
            Property(x => x.TonerLevelMagenta);
            Property(x => x.AddressMac);
            Property(x => x.Printed);
            
            // ToDo - Fix this!

            //Property(x => x.EmailSourceId, map => { map.Column("EmailSource"); map.NotNullable(false); });
            //Property(x => x.ServiceSourceSerialNumberId, map => {
            //    map.Column("ServiceSourceSerialNumber");
            //    map.NotNullable(false);
            //});
            //Property(x => x.ServiceSourceCountersId, map => { map.Column("ServiceSourceCounters"); map.NotNullable(false); });

            ManyToOne(x => x.EmailSource, map =>
            {
                map.Column("EmailSource");
                map.NotNullable(false);
                map.Cascade(Cascade.DeleteOrphans);
            });

            ManyToOne(x => x.ServiceSourceCounters, map =>
            {
                map.Column("ServiceSourceCounters");
                map.NotNullable(false);
                map.Cascade(Cascade.DeleteOrphans);
            });

            ManyToOne(x => x.ServiceSourceSerialNumber, map =>
            {
                map.Column("ServiceSourceSerialNumber");
                map.NotNullable(false);
                map.Cascade(Cascade.DeleteOrphans);
            });

        }
    }
}
