using Base;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken>
{
    public RefreshTokenRepository(IMongoDbContext database, string collectionName) : base(database, collectionName)
    {
    }

    public async Task<ICollection<RefreshToken>> GetUsersRefreshTokens(string id)
    {
        return await _collection.Find(user => user.UserId.Equals(id)).ToListAsync();
    }

    public async Task<DeleteResult> DeleteMany(ICollection<string> tokensIds)
    {
        return await _collection.DeleteManyAsync(token => tokensIds.Contains(token.Id));
    }
}