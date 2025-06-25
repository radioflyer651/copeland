using System.Text.Json.Nodes;
using Copeland.SensorParser.DataNormalization.DataModel;

namespace Copeland.SensorParser.DataNormalization
{
    public interface IDataNormalizer
    {
        IEnumerable<NormalizedData> NormalizeObject(JsonObject element);
    }
}
