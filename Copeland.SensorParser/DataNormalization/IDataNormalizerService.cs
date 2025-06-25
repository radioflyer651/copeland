using System.Text.Json.Nodes;
using Copeland.SensorParser.DataNormalization.DataModel;

namespace Copeland.SensorParser.DataNormalization
{
    public interface IDataNormalizerService
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
