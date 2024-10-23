using DAL.Domain;
using Microsoft.Extensions.Options;

namespace DAL;

public class RefreshTokenFactory
{
    private readonly RefreshTokenSettings _settings;

    public RefreshTokenFactory(IOptions<RefreshTokenSettings> settings)
    {
        _settings = settings.Value;
    }

    public RefreshToken Generate(string userId)
    {
        var token = new RefreshToken()
        {
            UserId = userId,
        };
        token.ExpirationDateTime = DateTime.UtcNow.AddDays(_settings.ExpiryDays);
        token.PreviousExpirationDateTime = DateTime.UtcNow.AddDays(_settings.ExpiryDays);
        return token;
    }
}