using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copeland.SensorParser.DataNormalization
{
    public enum NormalizedSensorDataTypes
    {
        Temperature,
        Humidity
    }

    public class NormalizedSensorData
    {
        public NormalizedSensorDataTypes SensorType { get; set; }
        public float Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
