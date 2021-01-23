using Core.DataAnnotations;
using Mediator.Core;
using Microsoft.Extensions.Caching.Memory;
using QuasarFireOperation.Domain.CommandModel.Entities;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite
{
    public class AddSingleSatelliteRequest : EntityRequest<IUnitOfWork, SatelliteMessageDto>
    {
        public AddSingleSatelliteObject Satelite { get; set; }
        [JsonExclude] public string SatelliteName { get; set; }
        [JsonExclude]  public IMemoryCache Cache { get; set; }

        protected override void ExpandResources(IUnitOfWork unitOfWork)
        {
        }
    }
}