using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites
{
    public class AddSatelliteObject
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public List<string> Message { get; set; }
    }
}
