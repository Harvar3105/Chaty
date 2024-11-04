using Bunit;
using Chaty.Components.Account;
using Chaty.Models;
using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ChatyTests.UITests;

//TODO: Cannot mock SignInManager properly. Should registration even be tested?
public class RegisterComponentTests : TestContext
{
    private readonly Mock<UserManager<User>> _mockUserManager;
    private readonly Mock<SignInManager<User>> _mockSignInManager;
    private readonly Mock<Uow> _mockUow;
    private readonly Mock<RefreshTokenFactory> _mockRefreshTokenFactory;
    private readonly Mock<ILogger<Register>> _mockLogger;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<IOptions<IdentityOptions>> _mockIdentityOptions;

    public RegisterComponentTests()
    {
        _mockUserManager = new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
            null, null, null, null, null, null, null, null);

        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockIdentityOptions = new Mock<IOptions<IdentityOptions>>();
        _mockLogger = new Mock<ILogger<Register>>();

        _mockSignInManager = new Mock<SignInManager<User>>(
            _mockUserManager.Object,
            _mockHttpContextAccessor.Object,
            _mockIdentityOptions.Object,
            _mockLogger.Object,
            null, null, null, null);

        Services.AddSingleton(_mockSignInManager.Object);
        Services.AddSingleton(_mockUserManager.Object);
        Services.AddSingleton(_mockUow.Object);
        Services.AddSingleton(_mockRefreshTokenFactory.Object);
        Services.AddSingleton(_mockLogger.Object);
    }

    [Fact]
    public void RegisterComponent_RenderedCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Register>();

        // Act
        var usernameInput = cut.Find("input#username");
        var emailInput = cut.Find("input#email");
        var passwordInput = cut.Find("input#password");

        // Assert
        Assert.NotNull(usernameInput);
        Assert.NotNull(emailInput);
        Assert.NotNull(passwordInput);
    }

    [Fact]
    public async Task RegisterComponent_SubmitsCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Register>();
        cut.Find("input#username").Change("testuser");
        cut.Find("input#email").Change("test@example.com");
        cut.Find("input#password").Change("Password123!");
        
        // Act
        await cut.InvokeAsync(() => cut.Find("button").Click());

        // Assert
    }
}
