using System.Text.Json;
using Copeland.SensorParser.DataNormalization.DataModel;

namespace Copeland.SensorParser.ApplicationServices
{
    public class FileDataSaver : IDataSaver
    {
        private readonly string _outFilePath;

        public FileDataSaver()
        {
            // Set the out folder.
            var intermediatePath = Path.Join(AppContext.BaseDirectory, "..", "..", "..", "..", "data", "output");
            _outFilePath = Path.Join(intermediatePath, "output.json");

            // We assume the data folder exists, leaving just the question of the output folder.
            if (!Directory.Exists(intermediatePath))
            {
                Directory.CreateDirectory(intermediatePath);
            }
        }

        public void Save(IEnumerable<NormalizedData> data)
        {
            // Delete the output file if it exists.
            if (File.Exists(_outFilePath))
            {
                File.Delete(_outFilePath);
            }

            // Serialize the data to a string.
            var outData = JsonSerializer.Serialize(data);

            // Write it to disk.
            File.WriteAllText(_outFilePath, outData);
        }
    }
}
