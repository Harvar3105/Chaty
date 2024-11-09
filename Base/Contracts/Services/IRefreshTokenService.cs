using Base.Contracts.Entities;
using MongoDB.Driver;

namespace Base.Contracts.Services;

public interface IRefreshTokenService<T> : IService<T> where T : class, IEntity
{
    public Task<ICollection<T?>> GetUsersRefreshTokens(string id);

    public Task<DeleteResult> DeleteMany(ICollection<string> tokensIds);
}