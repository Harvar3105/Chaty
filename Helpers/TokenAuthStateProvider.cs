using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Helpers;

[Obsolete("This class is not used. Might come in handy in a future version, but requires further implementations.", true)]
public class TokenAuthStateProvider : AuthenticationStateProvider
{
    
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymousIdentity = new ClaimsIdentity();
        var anonymousPrinciple = new ClaimsPrincipal(anonymousIdentity);
        return new AuthenticationState(anonymousPrinciple);
    }
}