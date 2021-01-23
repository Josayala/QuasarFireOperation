using Core;
using Core.Entities;

namespace QuasarFireOperation.Domain.CommandModel.Entities
{
    public class Satellite : EntityBase
    {
        public Satellite()
        {
        }
        public string Name { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public CompletionStatus Validate()
        {
            //TODO: fluent validation

            var status = new CompletionStatus();

            if (string.IsNullOrEmpty(Name)) status.AddValidationMessage("Name is Required");


            return status;
        }
    }
}