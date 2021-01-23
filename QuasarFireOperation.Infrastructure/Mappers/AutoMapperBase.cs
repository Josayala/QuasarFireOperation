using AutoMapper;
using AutoMapper.Configuration;

namespace QuasarFireOperation.Infrastructure.Mappers
{
    public abstract class AutoMapperBase<T> //where T : new()
    {
        protected AutoMapperBase()
        {
            ConfigureMappings(Configuration);
            var mapperConfig = new MapperConfiguration(Configuration);
            Mapper = new Mapper(mapperConfig);
        }

        protected IMapper Mapper { get; set; }
        private MapperConfigurationExpression Configuration { get; } = new MapperConfigurationExpression();

        protected virtual void ConfigureMappings(MapperConfigurationExpression cfg)
        {
            //universal maps could be added here
        }
    }
}