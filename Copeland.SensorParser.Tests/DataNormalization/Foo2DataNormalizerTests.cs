using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization.DataModel;
using Copeland.SensorParser.DataNormalization.Foo2;
using FluentAssertions;

namespace Copeland.SensorParser.Tests.DataNormalization
{
    public class Foo2DataNormalizerTests : TestBase
    {
        private readonly Foo2DataNormalizer _sut;

        public Foo2DataNormalizerTests()
        {
            _sut = new Foo2DataNormalizer();
        }

        [Theory]
        [InlineData("foo2\\test1.json", true)]
        [InlineData("foo2\\test2-wrong-id.json", false)]
        [InlineData("foo2\\test3-wrong-name.json", true)]
        [InlineData("foo2\\provided-data.json", true)] // Why not??
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
            var testTarget = ReadTestFile("Foo2\\temp-data-only.json");

            var expectedResult = new NormalizedData
            {
                CompanyId = 2,
                CompanyName = "Foo2",
                DeviceId = 1,
                DeviceName = "XYZ-100",
                AverageTemperature = 6,
                TemperatureCount = 3,
                FirstReadingDtm = DateTime.Parse("08-18-2020 10:35:00"),
                LastReadingDtm = DateTime.Parse("08-18-2020 10:45:00"),
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
            var testTarget = ReadTestFile("Foo2\\humidity-data-only.json");

            var expectedResult = new NormalizedData
            {
                CompanyId = 2,
                CompanyName = "Foo2",
                DeviceId = 1,
                DeviceName = "XYZ-100",
                AverageTemperature = null,
                TemperatureCount = null,
                FirstReadingDtm = DateTime.Parse("08-18-2020 10:35:00"),
                LastReadingDtm = DateTime.Parse("08-18-2020 10:45:00"),
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
