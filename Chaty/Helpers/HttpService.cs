using Chaty.Models;
using DAL.Domain;

namespace Chaty.Helpers.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
}
