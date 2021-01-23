using System.Collections.Generic;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite
{
    public class AddSingleSatelliteObject
    {
        public float Distance { get; set; }
        public List<string> Message { get; set; }
    }
}