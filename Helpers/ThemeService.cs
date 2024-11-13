using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Helpers;

public class ThemeService
{
    private readonly ILogger<ThemeService> _logger;
    private readonly IJSRuntime _jsRuntime;
    private readonly ISessionStorageService _sessionStorage;
    private bool _isDarkTheme;

    public ThemeService(ILogger<ThemeService> logger, IJSRuntime jsRuntime, ISessionStorageService sessionStorage)
    {
        _logger = logger;
        _jsRuntime = jsRuntime;
        _sessionStorage = sessionStorage;
        _isDarkTheme = false;

        LoadThemeFromSession();
    }

    public bool GetThemeState()
    {
        return _isDarkTheme;
    }

    public async Task ForceUpdateTheme()
    {
        _logger.Log(LogLevel.Information, $"Force Update Theme! Current {GetCurrentTheme()}");
        await _jsRuntime.InvokeVoidAsync("changeTheme", "page", _isDarkTheme ? "page dark" : "page light");
    }

    public string GetCurrentTheme()
    {
        var theme = _isDarkTheme ? "dark" : "light";
        // _logger.Log(LogLevel.Information, $"GetCurrentTheme: {theme}");
        return theme;
    }

    public async Task SetDarkTheme(bool theme)
    {
        _isDarkTheme = theme;

        await _sessionStorage.SetItemAsync("theme", _isDarkTheme ? "dark" : "light");

        await _jsRuntime.InvokeVoidAsync("changeTheme", "page", _isDarkTheme ? "page dark" : "page light");
    }

    private async void LoadThemeFromSession()
    {
        var theme = await _sessionStorage.GetItemAsync<string>("theme");
        _logger.Log(LogLevel.Information, $"LoadThemeFromSession: {theme}");
        if (theme != null)
        {
            _isDarkTheme = theme.Equals("dark");
        }
    }
}