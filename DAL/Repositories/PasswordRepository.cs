using Base;
using DAL.Domain;
using MongoDB.Driver;

namespace DAL.Repositories;

public class PasswordRepository : BaseRepository<Password>
{
    public PasswordRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    public async Task<Password> GetLatestByUserId(string id)
    {
        return await _collection.Find(p => p.UserId.Equals(id))
            .Sort(Builders<Password>.Sort.Descending(p => p.CreationDate))
            .FirstOrDefaultAsync();
    }
}