using System.Text.Json;
using System.Text.Json.Nodes;

namespace Copeland.SensorParser.ApplicationServices
{
    public class FileDataLoader : IDataLoader
    {

        private readonly string _baseFolderPath;

        public FileDataLoader()
        {
            // Set the base folder.
            var intermediatePath = Path.Join(AppContext.BaseDirectory, "..", "..", "..", "..", "data");
            _baseFolderPath = Path.GetFullPath(intermediatePath);
        }

        public IEnumerable<JsonObject> LoadData()
        {
            // Read the files.
            var files = Directory.GetFiles(_baseFolderPath);

            // Convert them to JsonObjects and return them.  We're assuming the data format's are right without error handling.
            var data = files.Select(f => File.ReadAllText(f)).ToList();

            foreach (var d in data)
            {
                Console.WriteLine(d);
            }

            return data.Select(d => JsonSerializer.Deserialize<JsonElement>(d)!)
                    .Select(e => JsonSerializer.Deserialize<JsonObject>(e)!)
                    .ToList();
        }
    }
}
