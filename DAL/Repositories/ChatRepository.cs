using System.Reflection;
using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Attributes;

namespace DAL.Repositories;

public class ChatRepository : BaseRepository<Chat>, IChatRepository<Chat>
{
    public ChatRepository(IMongoDbContext database) : base(database, typeof(Chat).GetCustomAttribute<CollectionNameAttribute>()!.Name)
    {
    }
}