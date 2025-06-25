using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization.Foo1.DataModel;
using Copeland.SensorParser.DataNormalization.Foo2.DataModel;

namespace Copeland.SensorParser.DataNormalization.Foo2
{
    public class Foo2DataNormalizer : IDataNormalizerService
    {
        public const int CompanyIdValue = 2;
        public const string CompanyIdKey = "CompanyId";
        public const string TemperatureKey = "TEMP";
        public const string HumidityKey = "HUM";

        public bool CanHandle(JsonObject jsonObject)
        {
            // Check that we have the required properties for this type.
            if (!Utilities.JsonObjectHasProperties(jsonObject, [nameof(Foo2Package.CompanyId), nameof(Foo2Package.Company), nameof(Foo2Package.Devices)]))
            {
                return false;
            }

            // Ensure the PartnerName is a match for our target type.
            if (!int.TryParse(jsonObject[CompanyIdKey]!.ToString(), out var companyId))
            {
                return false;
            }
            return companyId == CompanyIdValue;

        }

        public IEnumerable<NormalizedData> NormalizeData(JsonObject jsonObject)
        {
            // Return value.
            var result = new NormalizedData();

            // Convert to the Foo1 datatype.
            var package = JsonSerializer.Deserialize<Foo2Package>(jsonObject);

            // Validate.  I expect the cast to fail above if this doesn't work, but
            //  a the linter gave a warning of possible null, so we'll roll with it.
            if (package == null)
            {
                throw new InvalidCastException(nameof(DataModel.Foo2Package));
            }

            // Convert to sensor data.
            var sensorData = package.Devices.SelectMany(d => d.SensorData.Select(s => new
            {
                DeviceId = d.DeviceID,
                DeviceName = d.Name,
                // NOTE: In the real world, I might add a bit more error checking, just in case.
                Data = new NormalizedSensorData
                {
                    SensorType = s.SensorType == TemperatureKey ? NormalizedSensorDataTypes.Temperature : NormalizedSensorDataTypes.Humidity,
                    Value = s.Value,
                    DateTime = DateTime.Parse(s.DateTime)
                }
            }));

            // Finally, Group and return the results.
            return sensorData.GroupBy(s => s.DeviceId).Select(g =>
            {
                var r = new NormalizedData
                {
                    CompanyId = package.CompanyId,
                    CompanyName = package.Company,
                    DeviceId = g.Key,
                    DeviceName = g.First().DeviceName, // We have to assume the Device Name is the same for all.
                };

                // Set the data on this item.
                r.SetValueFromSensorData(g.Select(x => x.Data));

                // Return this data set.
                return r;
            }).ToList();
        }
    }
}
