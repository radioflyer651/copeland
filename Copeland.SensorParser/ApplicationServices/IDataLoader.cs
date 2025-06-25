using System.Text.Json.Nodes;

namespace Copeland.SensorParser.ApplicationServices
{
    public interface IDataLoader
    {
        IEnumerable<JsonObject> LoadData();
    }
}
