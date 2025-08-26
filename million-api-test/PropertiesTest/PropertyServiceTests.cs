using NUnit.Framework;
using million_api.Services;
using million_api.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace million_api_test
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IMongoCollection<Property>> _mockCollection;
        private PropertyService _service;

        [SetUp]
        public void Setup()
        {
            _mockCollection = new Mock<IMongoCollection<Property>>();
            var mockSettings = new Mock<IOptions<million_api.Models.Constants.DataBaseSettings>>();
            mockSettings.Setup(s => s.Value).Returns(new million_api.Models.Constants.DataBaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "testdb"
            });

            // Use reflection to inject the mock collection
            _service = (PropertyService)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(PropertyService));
            typeof(PropertyService)
                .GetField("_propertiesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_service, _mockCollection.Object);
        }

        [Test]
        public async Task GetAsync_ReturnsPropertiesList()
        {
            // Arrange
            var properties = new List<Property> { new Property { Id = "1", Name = "Test Property" } };
            var mockCursor = new Mock<IAsyncCursor<Property>>();
            mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor.SetupGet(x => x.Current).Returns(properties);

            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<Property>>(),
                It.IsAny<FindOptions<Property, Property>>(),
                default))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
            Assert.That(result[0].Name, Is.EqualTo("Test Property"));
        }
    }
}