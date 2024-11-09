using Base.Contracts.Entities;
using MongoDB.Driver;

namespace Base.Contracts.Repositories;

public interface IRefreshTokenRepository<T> : IRepository<T> where T : class, IEntity
{
    public Task<ICollection<T?>> GetUsersRefreshTokens(string id);

    public Task<DeleteResult> DeleteMany(ICollection<string> tokensIds);
}