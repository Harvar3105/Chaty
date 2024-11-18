using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Helpers;

public class ThemeService(ILogger<ThemeService> logger, IJSRuntime jsRuntime, ISessionStorageService sessionStorage)
{
    public async Task ToggleThemeAsync()
    {
        var theme = await GetCurrentTheme();
        theme = theme.Equals("light") ? "dark" : "light";
        await sessionStorage.SetItemAsync("theme", theme);
        await ForceUpdateTheme(theme);
    }

    public async Task ForceUpdateTheme(string? theme = null)
    {
        theme ??= await GetCurrentTheme();
        logger.LogInformation($"Force Update Theme! Current {theme}");
        await jsRuntime.InvokeVoidAsync("changeTheme", "page", $"page {theme}");
    }

    public async Task<string> GetCurrentTheme()
    {
        var theme = await sessionStorage.GetItemAsync<string>("theme");
        return theme ?? "light";
    }
}