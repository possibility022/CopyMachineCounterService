using CopyinfoWPF.ORM.Maps;
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

            var mapping = mapper.CompileMappingFor(
                new[]
                {
                    typeof(Emailsource)
                });

            return mapping;
        }

    }
}
