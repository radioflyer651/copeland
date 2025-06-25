using System.Text.Json;
using System.Text.Json.Nodes;
using Copeland.SensorParser.DataNormalization.Foo1;
using Copeland.SensorParser.DataNormalization.Foo2;

namespace Copeland.SensorParser
{
    public static class Program
    {
        public static string File1Path = "DeviceDataFoo1.json";
        public static string File2Path = "DeviceDataFoo2.json";

        static void Main(string[] args)
        {
            // Convert the file paths.
            File1Path = GetFilePath(File1Path);
            File2Path = GetFilePath(File2Path);

            var foo1Data = File.ReadAllBytes(File1Path);
            var foo2Data = File.ReadAllBytes(File2Path);

            var foo1Json = JsonSerializer.Deserialize<JsonObject>(foo1Data);
            var foo2Json = JsonSerializer.Deserialize<JsonObject>(foo2Data);

            var tester1 = new Foo1DataNormalizer();
            var tester2 = new Foo2DataNormalizer();

            var a1 = tester1.CanHandle(foo1Json);
            var a2 = tester1.CanHandle(foo2Json);

            var b1 = tester2.CanHandle(foo1Json);
            var b2 = tester2.CanHandle(foo2Json);

            var data1 = tester1.NormalizeData(foo1Json);
            var data2 = tester2.NormalizeData(foo2Json);

        }

        /// <summary>
        /// Returns the absolute path to a file, in the data folder, given a specified file name.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static string GetFilePath(string fileName)
        {
            // Get the local directory of the exe.
            var baseDirectory = AppContext.BaseDirectory;

            // Find the data folder.
            var dataFolder = Path.Join(baseDirectory, "..", "..", "..", "..", "data");


            // Return the path to the file.
            return Path.GetFullPath(Path.Combine(dataFolder, fileName));
        }
    }

}
