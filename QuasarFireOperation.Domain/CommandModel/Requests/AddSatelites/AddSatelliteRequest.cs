using System.Collections.Generic;
using Mediator.Core;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites
{
    public class AddSatelliteRequest : EntityRequest<IUnitOfWork, SatelliteMessageDto>
    {
        public List<AddSatelliteObject> SatelliteList { get; set; }

        protected override void ExpandResources(IUnitOfWork unitOfWork)
        {
        }
    }
}