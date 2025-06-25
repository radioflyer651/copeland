using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization.Foo1.DataModel;
using Copeland.SensorParser.DataNormalization.Foo2.DataModel;

namespace Copeland.SensorParser.DataNormalization.Foo1
{
    public class Foo1DataNormalizer : IDataNormalizer
    {
        public const int PartnerIdValue = 1;
        public const string PartnerIdKey = "PartnerId";
        public const string TemperatureKey = "Temperature";
        public const string HumidityKey = "Humidty";

        public bool CanHandle(JsonObject jsonObject)
        {
            // Check that we have the required properties for this type.
            if (!Utilities.JsonObjectHasProperties(jsonObject, [nameof(DataModel.Foo1Package.PartnerId), nameof(DataModel.Foo1Package.PartnerName), nameof(DataModel.Foo1Package.Trackers)]))
            {
                return false;
            }

            // Ensure the PartnerName is a match for our target type.
            if (!int.TryParse(jsonObject[PartnerIdKey]!.ToString(), out var partnerId))
            {
                return false;
            }
            return partnerId == PartnerIdValue;
        }

        public IEnumerable<NormalizedData> NormalizeData(JsonObject jsonObject)
        {
            // Return value.
            var result = new NormalizedData();

            // Convert to the Foo1 datatype.
            var package = JsonSerializer.Deserialize<Foo1Package>(jsonObject);

            // Validate.  I expect the cast to fail above if this doesn't work, but
            //  a the linter gave a warning of possible null, so we'll roll with it.
            if (package == null)
            {
                throw new InvalidCastException(nameof(DataModel.Foo1Package));
            }

            // Create some sensor data for these crumbs.
            var sensorData = package.Trackers
                .SelectMany(t => t.Sensors
                .SelectMany(s => s.Crumbs
                .Select(c => new
                {
                    DeviceId = t.Id,
                    DeviceName = t.Model,
                    // NOTE: In the real world, I might add a bit more error checking, just in case.
                    Data = new NormalizedSensorData
                    {
                        SensorType = s.Name == TemperatureKey ? NormalizedSensorDataTypes.Temperature : NormalizedSensorDataTypes.Humidity,
                        DateTime = DateTime.Parse(c.CreatedDtm),
                        Value = c.Value
                    }
                }
                )));

            // Finally, Group and return the results.
            return sensorData.GroupBy(s => s.DeviceId).Select(g =>
            {
                var r = new NormalizedData
                {
                    CompanyId = package.PartnerId,
                    CompanyName = package.PartnerName,
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
