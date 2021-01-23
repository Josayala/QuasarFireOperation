using Core.Entities;

namespace QuasarFireOperation.Domain.CommandModel.Entities
{
    public class Position : EntityBase
    {
        public Position(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
        public Position()
        {
   
        }
        public double x { get; set; }
        public double  y { get; set; }
    }
}