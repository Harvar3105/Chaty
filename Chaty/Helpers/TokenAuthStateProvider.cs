using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Chaty.Helpers.Services;

public class TokenAuthStateProvider : AuthenticationStateProvider
{
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymousIdentity = new ClaimsIdentity();
        var anonymousPrinciple = new ClaimsPrincipal(anonymousIdentity);
        return new AuthenticationState(anonymousPrinciple);
    }
}