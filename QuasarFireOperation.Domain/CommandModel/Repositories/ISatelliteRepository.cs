using System.Collections.Generic;
using QuasarFireOperation.Domain.CommandModel.Entities;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Domain.CommandModel.Repositories
{
    public interface ISatelliteRepository
    {
        Position GetLocation(List<AddSatelliteObject> satelliteList);
        string GetMessage(List<AddSatelliteObject> satelliteList);
        SatelliteMessageDto FindInformation(List<AddSatelliteObject> satelliteList);
    }
}