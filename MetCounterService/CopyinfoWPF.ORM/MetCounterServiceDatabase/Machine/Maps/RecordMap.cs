using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine.Maps
{
    public class RecordMap : ClassMapping<Record> {
        
        public RecordMap() {
			Schema("Machine");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Counterblackandwhite);
			Property(x => x.Countercolor);
			Property(x => x.Counterscanner);
			Property(x => x.Description);
			Property(x => x.Addressip);
			Property(x => x.Readdatetime, map => map.NotNullable(true));
			Property(x => x.Serialnumber, map => map.NotNullable(true));
			Property(x => x.Tonerlevelblack);
			Property(x => x.Tonerlevelcyan);
			Property(x => x.Tonerlevelyellow);
			Property(x => x.Tonerlevelmagenta);
			Property(x => x.Addressmac);
			ManyToOne(x => x.Emailsource, map => 
			{
				map.Column("EmailSource");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Servicesourcecounters, map => 
			{
				map.Column("ServiceSourceCounters");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Servicesourceserialnumber, map => 
			{
				map.Column("ServiceSourceSerialNumber");
				map.NotNullable(true);
				map.Cascade(Cascade.None);
			});

        }
    }
}
