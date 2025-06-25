using System.Text.Json;
using System.Text.Json.Nodes;
using AutoFixture;
using Moq;

namespace Copeland.SensorParser.Tests
{
    public abstract class TestBase
    {
        protected readonly MockRepository Repository;
        protected readonly Fixture Fixture;

        protected TestBase()
        {
            Repository = new MockRepository(MockBehavior.Strict);
            Fixture = new Fixture();
        }

        /// <summary>
        /// Reads a data file used for testing.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>
        /// We could create test objects inline, or even write the JSON inline, but writing JSON
        /// in a text editor is easier, and less error prone, since we don't have to escape quotes and such.
        /// Further, buildilng them from scratch is a PITA.
        /// </returns>
        protected JsonObject ReadTestFile(string fileName) 
        {
            // Figure out the data folder.
            var dataFolder = Path.Combine(AppContext.BaseDirectory, "test-data");

            // Determine the full file path.
            var filePath = Path.Combine(dataFolder, fileName);

            // Read the file.
            var content = File.ReadAllText(filePath);

            // Return the JSON object.
            return JsonSerializer.Deserialize<JsonObject>(content)!;
        }
    }
}