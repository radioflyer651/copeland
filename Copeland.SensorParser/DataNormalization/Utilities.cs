using System.Text.Json.Nodes;

namespace Copeland.SensorParser.DataNormalization
{
    /// <summary>
    /// Contains general utilities for application operation.
    /// </summary>
    public static class Utilities
    {
        public static bool JsonObjectHasProperties(JsonObject jsonObject, IEnumerable<string> propNames)
        {
            // Check that each property is in the element.
            //  I'm assuming a dictionary is efficient enough, but it's possible it's not.
            return propNames.All(n => jsonObject.ContainsKey(n));
        }
    }
}
