using Chaty.Components.Account;
using Chaty.Models;
using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChatyTests.UnitTests;

//TODO: SignInManager cannot be properly mocked. Requires custom class
public class RegisterTests
{
    private readonly Mock<SignInManager<User>> _mockSignInManager;
    private readonly Mock<Uow> _mockUow;
    private readonly Mock<RefreshTokenFactory> _mockRefreshTokenFactory;
    private readonly Mock<ILogger<Register>> _mockLogger;

    public RegisterTests()
    {
        _mockSignInManager = new Mock<SignInManager<User>>();
        _mockUow = new Mock<Uow>();
        _mockRefreshTokenFactory = new Mock<RefreshTokenFactory>();
        _mockLogger = new Mock<ILogger<Register>>();
    }

    [Fact]
    public async Task HandleRegistration_UserAlreadyExists_ThrowsException()
    {
        // Arrange
        var model = new RegisterModel
        {
            Email = "test@example.com",
            Password = "Password123!",
            Username = "testuser"
        };

        var mockUser = new User { Email = model.Email };
        _mockSignInManager.Setup(m => m.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(mockUser);

        var registerComponent = new Register(
            _mockSignInManager.Object,
            _mockUow.Object,
            _mockRefreshTokenFactory.Object,
            _mockLogger.Object
        )
        {
            Model = model
        };

        // Act
        await registerComponent.HandleRegistration();

        // Assert
        Assert.Equal("User with email test@example.com is already registered", registerComponent.ErrMessage);
    }
}