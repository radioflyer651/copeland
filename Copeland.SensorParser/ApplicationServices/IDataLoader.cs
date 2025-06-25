using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Copeland.SensorParser.ApplicationServices
{
    public interface IDataLoader
    {
        IEnumerable<JsonObject> LoadData();
    }
}
