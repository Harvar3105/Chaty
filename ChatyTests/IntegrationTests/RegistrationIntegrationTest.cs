using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit.Abstractions;

//TODO: tests are not emulated. Need to delete all changes after testing.
namespace ChatyTests.IntegrationTests;

public class RegistrationIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public RegistrationIntegrationTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _output = output;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_ValidUser_ReturnsSuccess()
    {
        // Arrange
        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe",
            Age = 30
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("api/User/Register", content);

        _output.WriteLine(response.Content.ReadAsStringAsync().Result);
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
}