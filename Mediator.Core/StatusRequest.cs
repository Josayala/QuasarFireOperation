using Core.Repositories;
using Core.Request;
using Core.Responses;
using MediatR;

namespace Mediator.Core
{
    public abstract class StatusRequest<TUnitOfWork> : RequestBase<TUnitOfWork>, IRequest<StatusResponse>
        where TUnitOfWork : ICoreUnitOfWork
    {
    }
}