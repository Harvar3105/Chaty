using System.Reflection;
using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Attributes;

namespace DAL.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository<Message>
{
    public MessageRepository(IMongoDbContext ctx) : base(ctx, typeof(Message).GetCustomAttribute<CollectionNameAttribute>()!.Name)
    {
    }
}