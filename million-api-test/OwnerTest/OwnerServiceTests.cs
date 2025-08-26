using million_api.Services;
using million_api.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace million_api_test.OwnerTest
{
    [TestFixture]
    public class OwnerServiceTests
    {
        private Mock<IMongoCollection<Owner>> _mockCollection;
        private OwnerService _service;

        [SetUp]
        public void Setup()
        {
            _mockCollection = new Mock<IMongoCollection<Owner>>();
            var mockSettings = new Mock<IOptions<DataBaseSettings>>();
            mockSettings.Setup(s => s.Value).Returns(new DataBaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "testdb"
            });

            // Use reflection to inject the mock collection
            _service = (OwnerService)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(OwnerService));
            typeof(OwnerService)
                .GetField("_ownersCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_service, _mockCollection.Object);
        }

        [Test]
        public async Task GetAsync_ReturnsOwnersList()
        {
            // Arrange
            var owners = new List<Owner> { new Owner { Id = "1", Name = "Test Owner" } };

            var mockCursor = new Mock<IAsyncCursor<Owner>>();
            mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor.Setup(x => x.Current).Returns(owners);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test Owner", result[0].Name);
        }
      
       
    }
}