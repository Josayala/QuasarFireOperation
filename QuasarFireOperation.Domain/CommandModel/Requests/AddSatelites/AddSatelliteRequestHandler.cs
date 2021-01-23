using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Responses;
using MediatR;
using QuasarFireOperation.Domain.CommandModel.Entities;
using QuasarFireOperation.Domain.CommandModel.Mappers;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites
{
    public class AddSatelliteRequestHandler : IRequestHandler<AddSatelliteRequest, EntityResponse<SatelliteMessageDto>>
    {
        public AddSatelliteRequestHandler(IUnitOfWork unitOfWork, ISatelliteMapper satelliteMapper)
        {
            UnitOfWork = unitOfWork;
            SatelliteMapper = satelliteMapper;
        }

        private IUnitOfWork UnitOfWork { get; }
        private ISatelliteMapper SatelliteMapper { get; }

        public async Task<EntityResponse<SatelliteMessageDto>> Handle(AddSatelliteRequest request,
            CancellationToken cancellationToken)
        {
            var completionStatus = new CompletionStatus();
            var hasSatelliteInvalid =
                request.SatelliteList.Any(item => !SatelliteMapper.BuildSatellite(item).Validate().IsSuccessful);
            var satelliteMessage = new SatelliteMessageDto();
            if (!hasSatelliteInvalid)
                satelliteMessage = UnitOfWork.SatelliteRepository.FindInformation(request.SatelliteList);
            else
                completionStatus.AddValidationMessage("The names of the satellites are required ");

            var response = new EntityResponse<SatelliteMessageDto> {Entity = satelliteMessage, Status = completionStatus};

            return response;
        }
    }
}