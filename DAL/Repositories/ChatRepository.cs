using Base;
using DAL.Domain;
using MongoDB.Driver;

namespace DAL.Repositories;

public class ChatRepository : BaseRepository<Chat>
{
    public ChatRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}