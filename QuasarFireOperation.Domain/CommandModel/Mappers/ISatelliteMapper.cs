using QuasarFireOperation.Domain.CommandModel.Entities;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite;

namespace QuasarFireOperation.Domain.CommandModel.Mappers
{
    public interface ISatelliteMapper
    {
        Satellite BuildSatellite(AddSatelliteObject addSatelliteObject);

        Satellite BuildSingleSatellite(AddSingleSatelliteRequest addSingleSatelliteRequest);
    }
}