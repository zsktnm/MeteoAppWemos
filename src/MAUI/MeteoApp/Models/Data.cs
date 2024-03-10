using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MeteoApp.Models
{
    public class Data
    {
        private const double pascalsToMercuryCoeff = 133.322;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double Light { get; set; }

        [JsonIgnore]
        public double PressureMercury => Pressure / pascalsToMercuryCoeff;
    }
}
