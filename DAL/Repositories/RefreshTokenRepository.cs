using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository<RefreshToken>
{
    public RefreshTokenRepository(IMongoDbContext database, string collectionName) : base(database, collectionName)
    {
    }

    public async Task<ICollection<RefreshToken?>> GetUsersRefreshTokens(string id)
    {
        return await Collection.Find(user => user.UserId.Equals(id)).ToListAsync();
    }

    public async Task<DeleteResult> DeleteMany(ICollection<string> tokensIds)
    {
        return await Collection.DeleteManyAsync(token => tokensIds.Contains(token.Id));
    }
}