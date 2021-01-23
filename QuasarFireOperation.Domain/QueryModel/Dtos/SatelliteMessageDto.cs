using Core.Entities;
using QuasarFireOperation.Domain.CommandModel.Entities;

namespace QuasarFireOperation.Domain.QueryModel.Dtos
{
    public class SatelliteMessageDto : EntityBase
    {
        public SatelliteMessageDto(Position position, string message)
        {
            Position = position;
            Message = message;
        }

        public SatelliteMessageDto()
        {
        }

        public Position Position { get; set; }
        public string Message { get; set; }
    }
}