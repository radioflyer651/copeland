using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Copeland.SensorParser.DataNormalization
{
    public interface IDataNormalizer
    {
        /// <summary>
        /// Given a specified JsonObject, returns a boolean value indicating whether or not this converter can
        /// work with a specified JSON object.
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        bool CanHandle(JsonObject jsonObject);

        /// <summary>
        /// Given a specified JsonObject which CanHandle would return true with, returns the normalized data for the Json object.
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        IEnumerable<NormalizedData> NormalizeData(JsonObject jsonObject);
    }
}
