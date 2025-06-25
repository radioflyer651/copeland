using System.Text.Json.Nodes;
using AutoFixture;
using Copeland.SensorParser.ApplicationServices;
using Copeland.SensorParser.DataNormalization;
using Copeland.SensorParser.DataNormalization.DataModel;
using FluentAssertions;
using Moq;

namespace Copeland.SensorParser.Tests.ApplicationServices
{
    public class DataProcessorTests : TestBase
    {
        private readonly Mock<IDataLoader> _dataLoader;
        private readonly Mock<IDataSaver> _dataSaver;
        private readonly Mock<IDataNormalizer> _dataNormalizer;

        private readonly DataProcessor _sut;

        public DataProcessorTests()
        {
            _dataLoader = Repository.Create<IDataLoader>();
            _dataSaver = Repository.Create<IDataSaver>();
            _dataNormalizer = Repository.Create<IDataNormalizer>();

            _sut = new DataProcessor(_dataLoader.Object, _dataSaver.Object, _dataNormalizer.Object);
        }

        [Fact]
        public void ProcessData_CallsAllProcesses()
        {
            // Arrange

            // We're unable to create JsonObjects with the fixuter, so we just create our own instead.
            var data = new List<JsonObject>();
            for (var i = 0; i < 3; i++)
            {
                data.Add(new JsonObject());
            }

            var normalizedData = Fixture.CreateMany<List<NormalizedData>>().ToList();
            var finalDataSet = normalizedData.SelectMany(x => x).ToList();

            _dataLoader.Setup(x => x.LoadData())
                .Returns(data);

            // Using a for loop here caused issues with scope variables, so next best thing in returning the value through a function.
            _dataNormalizer.Setup(x => x.NormalizeObject(It.IsAny<JsonObject>()))
                .Returns((JsonObject x) => normalizedData[data.IndexOf(x)]);
          
            // This validator will validate the final output being saved, as well as handle the validation
            //  in the mock setup on the save function.
            var validator = (IEnumerable<NormalizedData> x) =>
            {
                // If this fails, it will simpley error, and crash the test.  So true is the only result if it doesn't.
                x.Should().BeEquivalentTo(finalDataSet, x => x.WithoutStrictOrdering());

                return true;
            };
            _dataSaver.Setup(x => x.Save(It.Is<IEnumerable<NormalizedData>>(x => validator(x))));

            // Act
            _sut.ProcessData();

            // Assert
            Repository.VerifyAll();

            // NOTE: We already validated the data was a match to our data inside the validator of the _dataSaver.
        }
    }
}
