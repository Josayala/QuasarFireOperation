using Core.Entities;
using Core.Repositories;
using Core.Request;
using Core.Responses;
using MediatR;

namespace Mediator.Core
{
    public abstract class EntityRequest<TUnitOfWork, TEntity> : RequestBase<TUnitOfWork>,
        IRequest<EntityResponse<TEntity>>
        where TUnitOfWork : ICoreUnitOfWork
        where TEntity : EntityBase
    {
    }
}