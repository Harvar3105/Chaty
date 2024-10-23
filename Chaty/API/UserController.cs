using System.Net;
using System.Security.Claims;
using Chaty.Helpers;
using Chaty.Helpers.Models;
using Chaty.Helpers.Services;
using Chaty.Models;
using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chaty.API;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly Uow _uow;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<UserController> _logger;

    public UserController(Uow uow, IConfiguration configuration, UserManager<User> userManager, ILogger<UserController> logger, SignInManager<User> signInManager)
    {
        _uow = uow;
        _configuration = configuration;
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<JWT>((int) HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<JWT>> Register(
        [FromBody] RegisterModel model,
        [FromQuery] int expiresInSeconds
    )
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>("JWT:expiresInSeconds")
            ? expiresInSeconds
            : _configuration.GetValue<int>("JWT:expiresInSeconds");
        
        _logger.LogWarning("RegisterModel: " + model);
        
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            _logger.LogWarning("User with email {} is already registered", model.Email);
            return BadRequest(
                new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = $"User with email {model.Email} is already registered"
                }
            );
        }
        
        user = new User(username: model.Username,
            firstName: model.FirstName,
            lastName: model.LastName,
            email: model.Email,
            age: model.Age);
        
        var refreshToken = new RefreshToken
        {
            UserId = user.Id
        };
        await _uow.RefreshTokenRepository.AddAsync(refreshToken);
        user.RefreshTokens.Add(refreshToken);
        
        
        if (string.IsNullOrWhiteSpace(user.Id))
        {
           return BadRequest(
                new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "User add does not succeeded!"
                }
            );
        }
        
        refreshToken.UserId = user.Id;
        _logger.LogCritical("User: " + user);
        
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            _logger.LogCritical("Failed to create new user!");
            return BadRequest(
                new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = result.Errors.First().Description
                }
            );
        }
        
        result = await _userManager.AddClaimsAsync(user, new List<Claim>()
        {
            new (ClaimTypes.Name, user.UserName)
        });
        if (!result.Succeeded)
        {
            _logger.LogCritical("Failed to register user claims!");
            return BadRequest(
                new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = result.Errors.First().Description
                }
            );
        }

        user = await _userManager.FindByEmailAsync(user.Email);
        if (user == null)
        {
            _logger.LogWarning("User with email {} is not found after registration", model.Email);
            return BadRequest(
                new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = $"User with email {model.Email} is not found after registration"
                }
            );
        }
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        var jwt = JWTHelper.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWT:key")!,
            _configuration.GetValue<string>("JWT:issuer")!,
            _configuration.GetValue<string>("JWT:audience")!,
            expiresInSeconds
        );
        var res = new JWT()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.RefreshToken,
        };
        return Ok(res);
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<JWT>((int) HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<JWT>> Login(
        [FromBody] LoginModel model,
        [FromQuery] int expiresInSeconds = Int32.MaxValue
        )
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>("JWT:expiresInSeconds")
            ? expiresInSeconds
            : _configuration.GetValue<int>("JWT:expiresInSeconds");
        
        // verify user
        User user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", model.Email);
            return NotFound("User/Password problem");
        }
        
        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", model.Password,
                model.Email);
            return NotFound("User/Password problem");
        }
        
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("WebApi login failed, claimsPrincipal null");
            return NotFound("User/Password problem");
        }

        var tokens = await _uow.RefreshTokenRepository.GetUsersRefreshTokens(user.Id!);
        var tokensToDelete = tokens
            .Where(t => t.ExpirationDateTime < DateTime.UtcNow)
            .Select(t => t.Id).ToList();
        if (tokensToDelete.Any()) await _uow.RefreshTokenRepository.DeleteMany(tokensToDelete!);
        _logger.LogInformation("Deleted {} refresh tokens", tokensToDelete.Count);
        
        var refreshToken = new RefreshToken()
        {
            UserId = user.Id!
        };
        await _uow.RefreshTokenRepository.AddAsync(refreshToken);
        

        var jwt = JWTHelper.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWT:key")!,
            _configuration.GetValue<string>("JWT:issuer")!,
            _configuration.GetValue<string>("JWT:audience")!,
            expiresInSeconds
        );

        var responseData = new JWT()
        {
            UserId = user.Id!,
            Jwt = jwt,
            RefreshToken = refreshToken.RefreshToken
        };

        return Ok(responseData);
    }

    

    private void ValidateUser(User user, IEnumerable<User> users)
    {
        
        foreach (User data in users)
        {
            if (data.UserName.Equals(user.UserName)) throw new Exception("ERROR: 1. User with such username already exists! " + user.UserName);
            if (data.Email!.Equals(user.Email)) throw new Exception("ERROR: 2. Email is already in use!");
        }
    }
}