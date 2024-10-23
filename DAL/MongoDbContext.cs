using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL;

public class MongoDbContext(IMongoClient client, IMongoDatabase database) : IMongoDbContext
{
    public IMongoClient Client { get; } = client;
    public IMongoDatabase Database { get; } = database;

    public IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null)
    {
        if (string.IsNullOrWhiteSpace(partitionKey))
        {
            partitionKey = typeof(TDocument).Name;
        }
        
        return Database.GetCollection<TDocument>(partitionKey);
    }

    public void DropCollection<TDocument>(string partitionKey = null)
    {
        if (string.IsNullOrWhiteSpace(partitionKey))
        {
            partitionKey = typeof(TDocument).Name;
        }
    
        Database.DropCollection(partitionKey);
    }

    public void SetGuidRepresentation(GuidRepresentation guidRepresentation)
    {
        throw new NotImplementedException();
    }
    
}