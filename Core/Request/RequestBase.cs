using Core.Repositories;

namespace Core.Request
{
    public abstract class RequestBase
    {
    }

    public abstract class RequestBase<TUnitOfWork> : RequestBase where TUnitOfWork : ICoreUnitOfWork
    {
        public void ExpandRequestResources(TUnitOfWork unitOfWork)
        {
            ExpandResources(unitOfWork);
        }

        protected abstract void ExpandResources(TUnitOfWork unitOfWork);
    }
}