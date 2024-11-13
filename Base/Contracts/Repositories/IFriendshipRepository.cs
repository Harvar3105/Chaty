using Base.Contracts.Entities;

namespace Base.Contracts.Repositories;

public interface IFriendshipRepository<T> : IRepository<T> where T : class, IEntity
{
    public Task<List<T>> GetFriendshipsByUserIdAsync(string userId);

    public Task RemoveByIdsAsync(string firstId, string secondId);
}