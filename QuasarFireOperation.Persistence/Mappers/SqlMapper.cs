using AutoMapper.Configuration;

namespace QuasarFireOperation.Persistence.Mappers
{
    public class SqlMapper : SqlAutoMapperBase<SqlMapper>
    {
        private SqlMapper()
        {
        }

        protected override void ConfigureMappings(MapperConfigurationExpression mapperConfig)
        {
            base.ConfigureMappings(mapperConfig);
        }
    }
}