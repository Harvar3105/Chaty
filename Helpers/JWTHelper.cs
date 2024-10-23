using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Helpers;

public static class JWTHelper
{
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
        int expiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddSeconds(expiresInSeconds);
        var token = new JwtSecurityToken(
            issuer: issuer, // Who sends token
            audience: audience, // Who receives token
            claims: claims, // Users data in key:value format
            expires: expires,
            signingCredentials: signingCredentials //Key + hash = can be used for validation
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static bool ValidateJWT(string jwt, string key, string issuer, string audience)
    {
        var validationParams = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidateIssuer = true,

            ValidAudience = audience,
            ValidateAudience = true,

            ValidateLifetime = false
        };

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(jwt, validationParams, out var validatedToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}