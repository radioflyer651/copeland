using Copeland.SensorParser.DataNormalization.DataModel;
using Copeland.SensorParser.DataNormalization.Foo1;
using FluentAssertions;

namespace Copeland.SensorParser.Tests.DataNormalization
{
    public class Foo1DataNormalizerTests : TestBase
    {
        private readonly Foo1DataNormalizer _sut;

        public Foo1DataNormalizerTests()
        {
            _sut = new Foo1DataNormalizer();
        }

        [Theory]
        [InlineData("foo1\\test1.json", true)]
        [InlineData("foo1\\test2-wrong-id.json", false)]
        [InlineData("foo1\\test3-wrong-name.json", true)]
        [InlineData("foo1\\provided-data.json", true)] // Why not??
        public void CanHandle(string filePath, bool expectedResult)
        {
            // Arrange
            var testTarget = ReadTestFile(filePath);

            // Act
            var result = _sut.CanHandle(testTarget);

            // Assert
            result.Should().Be(expectedResult);
            Repository.VerifyAll();
        }

        [Fact]
        public void NormalizeData_Temperatures_ReturnsTemperatureData()
        {
            // Arrange
            var testTarget = ReadTestFile("foo1\\temp-data-only.json");

            var expectedResult = new NormalizedData
            {
                CompanyId = 1,
                CompanyName = "Foo1",
                DeviceId = 1,
                DeviceName = "ABC-100",
                AverageTemperature = 6,
                TemperatureCount = 3,
                FirstReadingDtm = DateTime.Parse("08-17-2020 10:35:00"),
                LastReadingDtm = DateTime.Parse("08-17-2020 10:45:00"),
                AverageHumidity = null,
                HumidityCount = null,
            };

            // Act
            var result = _sut.NormalizeData(testTarget);

            // Assert
            Repository.VerifyAll();
            result.Should().BeEquivalentTo([expectedResult]);
        }

        [Fact]
        public void NormalizeData_Humidities_ReturnsHumidityData()
        {
            // Arrange
            var testTarget = ReadTestFile("foo1\\humidity-data-only.json");

            var expectedResult = new NormalizedData
            {
                CompanyId = 1,
                CompanyName = "Foo1",
                DeviceId = 1,
                DeviceName = "ABC-100",
                AverageTemperature = null,
                TemperatureCount = null,
                FirstReadingDtm = DateTime.Parse("08-17-2020 10:35:00"),
                LastReadingDtm = DateTime.Parse("08-17-2020 10:45:00"),
                AverageHumidity = 6,
                HumidityCount = 3,
            };

            // Act
            var result = _sut.NormalizeData(testTarget);

            // Assert
            Repository.VerifyAll();
            result.Should().BeEquivalentTo([expectedResult]);
        }

    }
}
