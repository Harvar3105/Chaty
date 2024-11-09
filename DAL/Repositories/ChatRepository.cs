using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL.Repositories;

public class ChatRepository : BaseRepository<Chat>, IChatRepository<Chat>
{
    public ChatRepository(IMongoDbContext database, string collectionName) : base(database, collectionName)
    {
    }
}