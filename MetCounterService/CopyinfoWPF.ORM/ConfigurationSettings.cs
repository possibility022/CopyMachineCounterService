using CopyinfoWPF.ORM.Machine;
using CopyinfoWPF.ORM.Machine.Maps;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace CopyinfoWPF.ORM
{
    public static class ConfigurationSettings
    {

        public static HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();

            mapper.AddMapping<EmailsourceMap>();
            mapper.AddMapping<RecordMap>();
            mapper.AddMapping<ServiceSourceSerialNumberMap>();
            mapper.AddMapping<ServiceSourceCountersMap>();


            var mapping = mapper.CompileMappingFor(
                new[]
                {
                    typeof(EmailSource),
                    typeof(Record),
                    typeof(ServiceSourceSerialNumber),
                    typeof(ServiceSourceCounters)
                });

            return mapping;
        }

    }
}
