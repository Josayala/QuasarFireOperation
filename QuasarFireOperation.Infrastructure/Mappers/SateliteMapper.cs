using AutoMapper.Configuration;
using QuasarFireOperation.Domain.CommandModel.Entities;
using QuasarFireOperation.Domain.CommandModel.Mappers;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite;

namespace QuasarFireOperation.Infrastructure.Mappers
{
    public class SateliteMapper : AutoMapperBase<SateliteMapper>, ISatelliteMapper
    {
        public Satellite BuildSatellite(AddSatelliteObject addSatelliteObject)
        {
            var satellite = (Satellite) Mapper.Map(addSatelliteObject, addSatelliteObject.GetType(),
                typeof(Satellite));
            return satellite;
        }

        public Satellite BuildSingleSatellite(AddSingleSatelliteRequest addSingleSatelliteRequest)
        {
            var satellite = (Satellite)Mapper.Map(addSingleSatelliteRequest, addSingleSatelliteRequest.GetType(),
                typeof(Satellite));
            return satellite;
        }
        protected override void ConfigureMappings(MapperConfigurationExpression cfg)
        {
            base.ConfigureMappings(cfg);

            cfg.CreateMap<AddSatelliteObject, Satellite>();


            cfg.CreateMap<AddSingleSatelliteRequest, Satellite>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SatelliteName.Trim()));
        }
    }
}