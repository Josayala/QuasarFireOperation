using Core.Repositories;

namespace QuasarFireOperation.Domain.CommandModel.Repositories
{
    public interface IUnitOfWork : ICoreUnitOfWork
    {
        ISatelliteRepository SatelliteRepository { get; }
    }
}