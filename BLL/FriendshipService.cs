using Base.Contracts.Repositories;
using Base.Contracts.Services;
using DAL.Domain;
using DAL.Domain.AddressTables;
using Microsoft.AspNetCore.Identity;

namespace BLL;

public class FriendshipService : BaseService<Friendship>, IFriendshipService<Friendship>
{
    public FriendshipService(IRepository<Friendship> repository, UserManager<User> userManager) : base(repository)
    {
        _userManager = userManager;
    }
    
    private IFriendshipRepository<Friendship> FriendshipRepository => (IFriendshipRepository<Friendship>) _repository;

    private readonly UserManager<User> _userManager;

    public new async Task<List<Friendship>> GetAllAsync()
    {
        var result = await FriendshipRepository.GetAllAsync();
        foreach (var friendship in result)
        {
            friendship.FirstUser = await _userManager.FindByIdAsync(friendship.FirstUserId);
            friendship.SecondUser = await _userManager.FindByIdAsync(friendship.SecondUserId);
        }

        return result;
    }

    //TODO: lookup how to optimize. Maybe DB results may be cached
    public new async Task AddAsync(Friendship entity)
    {
        var stored = await FriendshipRepository.GetAllAsync();
        foreach (var friendship in stored)
        {
            if (entity.FirstUserId == friendship.FirstUserId && entity.SecondUserId == friendship.SecondUserId ||
                entity.FirstUserId == entity.SecondUserId && entity.FirstUserId == friendship.SecondUserId)
            {
                return;
            }
        }

        await FriendshipRepository.AddAsync(entity);
    }

    public async Task RemoveByIdsAsync(string firstId, string secondId)
    {
        await FriendshipRepository.RemoveByIdsAsync(firstId, secondId);
    }

    public async Task<bool> RemoveAsync(Friendship entity)
    {
        var stored = await FriendshipRepository.GetAllAsync();
        foreach (var friendship in stored)
        {
            if (entity.FirstUserId.Equals(friendship.FirstUserId) &&
                entity.SecondUserId.Equals(friendship.SecondUserId) ||
                entity.FirstUserId.Equals(entity.SecondUserId) && entity.FirstUserId == friendship.SecondUserId)
            {
                await FriendshipRepository.DeleteAsync(friendship.Id!);
                return true;
            }
        }

        return false;
    }

    public Task<List<Friendship>> GetFriendshipsByUserIdAsync(string userId)
    {
        return FriendshipRepository.GetFriendshipsByUserIdAsync(userId);
    }
}