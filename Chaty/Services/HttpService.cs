using Chaty.Models;
using DAL.Domain;

namespace Chaty.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpService> _logger;

    public HttpService(HttpClient httpClient, ILogger<HttpService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<User> RegisterAsync(RegisterModel registerModel)
    {
        _logger.LogWarning("RegisterModel: " + registerModel);
        var response = await _httpClient.PostAsJsonAsync("api/User/Register", registerModel);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User> LoginAsync(LoginModel loginModel)
    {
        _logger.LogWarning("LogingModel: " + loginModel);
        var response = await _httpClient.PostAsJsonAsync("api/User/Login", loginModel);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
        
        return await response.Content.ReadFromJsonAsync<User>();
    }
}
