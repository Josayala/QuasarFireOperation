using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using QuasarFireOperation.Domain.CommandModel.Mappers;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite
{
    public class
        AddSingleSatelliteRequestHandler : IRequestHandler<AddSingleSatelliteRequest,
            EntityResponse<SatelliteMessageDto>>
    {
        public AddSingleSatelliteRequestHandler(IUnitOfWork unitOfWork, ISatelliteMapper sateliteMapper)
        {
            UnitOfWork = unitOfWork;
            SateliteMapper = sateliteMapper;
        }

        private IUnitOfWork UnitOfWork { get; }
        private ISatelliteMapper SateliteMapper { get; }

        public async Task<EntityResponse<SatelliteMessageDto>> Handle(AddSingleSatelliteRequest request,
            CancellationToken cancellationToken)
        {
            var satellite = SateliteMapper.BuildSingleSatellite(request);
            var completionStatus = satellite.Validate();

            var satelliteObject = new AddSatelliteObject
            {
                Distance = request.Satelite.Distance,
                Name = request.SatelliteName,
                Message = request.Satelite.Message
            };

            var cacheList = request.Cache.Get<List<AddSatelliteObject>>("satelliteList");
            if (cacheList == null)
                request.Cache.Set("satelliteList", new List<AddSatelliteObject> {satelliteObject});
            else
                cacheList.Add(satelliteObject);


            var satelliteMessage = new SatelliteMessageDto();

            if (completionStatus.IsSuccessful)

                satelliteMessage =
                    UnitOfWork.SatelliteRepository.FindInformation(cacheList);

            else
                completionStatus.AddValidationMessage("The names of the satellites are required ");

            var response = new EntityResponse<SatelliteMessageDto>
                {Entity = satelliteMessage, Status = completionStatus};

            return response;
        }
    }
}