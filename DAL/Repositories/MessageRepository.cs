using Base;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL.Repositories;

public class MessageRepository : BaseRepository<Message>
{
    public MessageRepository(IMongoDbContext ctx, string collectionName) : base(ctx, collectionName)
    {
    }
}