using System.Collections.Generic;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite
{
    public class AddSingleSatelliteObject
    {
        public double Distance { get; set; }
        public List<string> Message { get; set; }
    }
}