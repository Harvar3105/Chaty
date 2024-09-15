using System.Net;
using Chaty.API.Info;
using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Chaty.API;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly UOW _uow;

    public UserController(UOW uow)
    {
        _uow = uow;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<User>>((int) HttpStatusCode.OK)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IEnumerable<User>> GetAllUser()
    {
        return await _uow.UserRepository.GetAllAsync();
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<User>> Register(
        [FromBody] RegisterInfo info
    )
    {
        var user = new User(info.Username, info.FirstName, info.LastName, info.Email, info.Age);
        await _uow.UserRepository.AddAsync(user);
        return CreatedAtAction("Register", new { id = user.Id.ToString() }, user);
    }
}