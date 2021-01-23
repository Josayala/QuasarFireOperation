using Core.Entities;

namespace QuasarFireOperation.Domain.CommandModel.Entities
{
    public class SatelliteMessage : EntityBase
    {
        public SatelliteMessage(Position position, string message)
        {
            Position = position;
            Message = message;
        }

        public SatelliteMessage()
        {
        }
        public Position Position { get; set; }
        public string Message { get; set; }
    }
}