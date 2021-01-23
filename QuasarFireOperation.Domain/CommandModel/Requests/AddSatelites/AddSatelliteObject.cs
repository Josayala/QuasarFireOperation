using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites
{
    public class AddSatelliteObject
    {
        public string Name { get; set; }
        public float Distance { get; set; }
        public List<string> Message { get; set; }
    }
}
