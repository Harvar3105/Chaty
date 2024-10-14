using Base;
using DAL.Domain;
using MongoDB.Driver;

namespace DAL.Repositories;

public class MessageRepository : BaseRepository<Message>
{
    public MessageRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}