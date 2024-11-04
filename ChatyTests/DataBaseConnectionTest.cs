using Moq;
using MongoDB.Driver;
using DAL;
using Xunit.Abstractions;

namespace ChatyTests
{
    public class DataBaseConnectionTest
    {
        private readonly Mock<IMongoClient> _mockClient;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly MongoDbContext _mongoDbContext;
        private readonly ITestOutputHelper _output;

        public DataBaseConnectionTest(ITestOutputHelper output)
        {
            _output = output;
            _mockClient = new Mock<IMongoClient>();
            _mockDatabase = new Mock<IMongoDatabase>();

            _mockClient.Setup(client => client.GetDatabase(It.IsAny<string>(), null)).Returns(_mockDatabase.Object);

            _mongoDbContext = new MongoDbContext(_mockClient.Object, _mockDatabase.Object);
        }

        [Fact]
        public void ShouldConnectToMockedMongoDB()
        {
            var collectionName = "TestCollection";
            var mockCollection = new Mock<IMongoCollection<TestEntity>>();

            _mockDatabase.Setup(db => db.GetCollection<TestEntity>(collectionName, null)).Returns(mockCollection.Object);

            var collection = _mongoDbContext.GetCollection<TestEntity>(collectionName);
            
            _output.WriteLine("Collection: {0}", collectionName);

            Assert.NotNull(collection);
            Assert.Equal(mockCollection.Object, collection);
        }

        public class TestEntity
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}