using Base;
using Base.Contracts.Repositories;
using DAL.Domain.AddressTables;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace DAL.Repositories;

public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository<Friendship>
{
    public FriendshipRepository(IMongoDbContext ctx, string collectionName) : base(ctx, collectionName)
    {
    }

    public async Task<List<Friendship>> GetFriendshipsByUserIdAsync(string userId)
    {
        return (await Collection.FindAsync(entity => entity.FirstUserId == userId || entity.SecondUserId == userId)).ToList();
    }
}