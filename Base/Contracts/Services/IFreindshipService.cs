using Base.Contracts.Entities;

namespace Base.Contracts.Services;

public interface IFriendshipService<T> : IService<T> where T : class, IEntity
{
    public Task<List<T>> GetFriendshipsByUserIdAsync(string userId);
    public Task<bool> RemoveAsync(T entity);
}