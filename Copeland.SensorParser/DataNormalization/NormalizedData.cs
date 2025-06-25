using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization.Foo2.DataModel;

namespace Copeland.SensorParser.DataNormalization
{
    public class NormalizedData
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int? DeviceId { get; set; }

        public string DeviceName { get; set; }

        public DateTime? FirstReadingDtm { get; set; }

        public DateTime? LastReadingDtm { get; set; }

        public int? TemperatureCount { get; set; }

        public double? AverageTemperature { get; set; }

        public int? HumidityCount { get; set; }

        public double? AverageHumdity { get; set; }

        /// <summary>
        /// Sets the temperature and humidity data from a specified set of sensor data.
        /// </summary>
        /// <param name="data"></param>
        public void SetValueFromSensorData(IEnumerable<NormalizedSensorData> data)
        {
            var groups = data.GroupBy(s => s.SensorType).ToDictionary(g => g.Key, g => g.ToArray());

            if (groups.TryGetValue(NormalizedSensorDataTypes.Temperature, out var tempVals))
            {
                TemperatureCount = tempVals.Length;
                AverageTemperature = tempVals.Average(x => x.Value);
            }

            if (groups.TryGetValue(NormalizedSensorDataTypes.Humidity, out var humidityVals))
            {
                HumidityCount = humidityVals.Length;
                AverageHumdity = humidityVals.Average(x => x.Value);
            }

            // Get the sensor date/times, in order, for performance.
            var sensorDateTimes = data.Select(x => x.DateTime).Order();

            // Set the sensor date/times on this data set.
            FirstReadingDtm = sensorDateTimes.FirstOrDefault();
            LastReadingDtm  = sensorDateTimes.LastOrDefault();
        }
    }
}
