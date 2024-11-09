using System.Reflection;
using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using DAL.Domain.AddressTables;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Attributes;

namespace DAL.Repositories;

public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository<Friendship>
{
    public FriendshipRepository(IMongoDbContext ctx, UserManager<User> userManager) : base(ctx, typeof(Friendship).GetCustomAttribute<CollectionNameAttribute>()!.Name)
    {
        _userManager = userManager;
    }

    private readonly UserManager<User> _userManager;

    public async Task<List<Friendship>> GetFriendshipsByUserIdAsync(string userId)
    {
        return (await Collection.FindAsync(entity => entity.FirstUserId == userId || entity.SecondUserId == userId)).ToList();
    }
}