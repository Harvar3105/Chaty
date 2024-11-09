using Base.Contracts.Repositories;
using Base.Contracts.Services;
using DAL.Domain;
using DAL.Repositories;
using MongoDB.Driver;

namespace BLL;

public class RefreshTokenService : BaseService<RefreshToken>, IRefreshTokenService<RefreshToken>
{
    public RefreshTokenService(Lazy<IRepository<RefreshToken>> repository) : base(repository)
    {
    }
    
    private IRefreshTokenRepository<RefreshToken> RefreshTokenRepository =>
        (IRefreshTokenRepository<RefreshToken>) _repository.Value;
    
    public Task<ICollection<RefreshToken?>> GetUsersRefreshTokens(string id)
    {
        return RefreshTokenRepository.GetUsersRefreshTokens(id);
    }

    public Task<DeleteResult> DeleteMany(ICollection<string> tokensIds)
    {
        return RefreshTokenRepository.DeleteMany(tokensIds);
    }
}