using Core.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.QueryModel.Queries.Satellite
{
    public class SatelliteListQuery : IRequest<SatelliteMessageDto>
    {
        [JsonExclude] public IMemoryCache Cache { get; set; }
    }
}