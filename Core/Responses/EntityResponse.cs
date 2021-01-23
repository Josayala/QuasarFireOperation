namespace Core.Responses
{
    public class EntityResponse<TEntity> : StatusResponse
    {
        public EntityResponse()
        {
        }

        public EntityResponse(CompletionStatus status, TEntity entity)
        {
            Status = status;
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}