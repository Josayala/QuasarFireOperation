using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.QueryModel.Queries.Satellite
{
    public class SatelliteListQueryHandler : IRequestHandler<SatelliteListQuery, SatelliteMessageDto>
    {
        public SatelliteListQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<SatelliteMessageDto> Handle(SatelliteListQuery request,
            CancellationToken cancellationToken)
        {
            var cacheList = request.Cache.Get<List<AddSatelliteObject>>("satelliteList");
            var satelliteMessage =
                UnitOfWork.SatelliteRepository.FindInformation(cacheList);

            return satelliteMessage;
        }
    }
}