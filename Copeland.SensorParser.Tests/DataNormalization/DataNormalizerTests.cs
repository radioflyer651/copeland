using System.Text.Json.Nodes;
using AutoFixture;
using Copeland.SensorParser.DataNormalization;
using Copeland.SensorParser.DataNormalization.DataModel;
using FluentAssertions;
using Moq;

namespace Copeland.SensorParser.Tests.DataNormalization
{
    public class DataNormalizerTests : TestBase
    {
        private readonly DataNormalizer _sut;
        private readonly Mock<IDataNormalizerService> _service1;
        private readonly Mock<IDataNormalizerService> _service2;

        public DataNormalizerTests()
        {
            _service1 = Repository.Create<IDataNormalizerService>();
            _service2 = Repository.Create<IDataNormalizerService>();

            _sut = new DataNormalizer([_service1.Object, _service2.Object]);
        }

        [Fact]
        public void GetService_WhenFound_ReturnsService()
        {
            // Arrange

            // Can't mock this, so we'll just create one.
            var target = new JsonObject();

            _service1.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(false);

            // This should trigger second, since it's second in the list.
            _service2.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(true);

            // Act
            var result = _sut.GetService(target);

            // Assert
            Repository.VerifyAll();
            result.Should().Be(_service2.Object);
        }

        [Fact]
        public void GetService_WhenNotFound_ReturnsNull()
        {
            // Arrange

            // Can't mock this, so we'll just create one.
            var target = new JsonObject();

            _service1.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(false);

            _service2.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(false);

            // Act
            var result = _sut.GetService(target);

            // Assert
            Repository.VerifyAll();
            result.Should().BeNull();
        }

        [Fact]
        public void NormalizeObject_ReturnsServiceResult()
        {
            // Arrange

            // Can't mock this, so we'll just create one.
            var target = new JsonObject();

            _service1.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(false);

            // This should trigger second, since it's second in the list.
            _service2.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(true);

            var expectedResult = Fixture.CreateMany<NormalizedData>();
            _service2.Setup(x => x.NormalizeData(target))
                .Returns(expectedResult);

            // Act
            var result = _sut.NormalizeObject(target);

            // Assert
            Repository.VerifyAll();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void NormalizeObject_WhenNoServiceFound_ShouldThrough()
        {
            // Arrange

            // Can't mock this, so we'll just create one.
            var target = new JsonObject();

            _service1.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(false);

            // This should trigger second, since it's second in the list.
            _service2.Setup(x => x.CanHandle(It.IsAny<JsonObject>()))
                .Returns(false);

            // Act
            var action = () =>  _sut.NormalizeObject(target);

            // Assert
            action.Should().Throw<NormalizationServiceNotFoundException>();
            Repository.VerifyAll();


        }
    }
}
