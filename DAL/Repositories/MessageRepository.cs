using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository<Message>
{
    public MessageRepository(IMongoDbContext ctx, string collectionName) : base(ctx, collectionName)
    {
    }
}