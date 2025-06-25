using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization.DataModel;

namespace Copeland.SensorParser.DataNormalization
{
    public interface IDataNormalizer
    {
        IEnumerable<NormalizedData> NormalizeObject(JsonObject element);
    }
}
