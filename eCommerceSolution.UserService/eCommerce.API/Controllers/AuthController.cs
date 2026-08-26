using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSolution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IUsersService _usersService;

    public AuthController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest? request)
    {
        if (request is null) return BadRequest();

        var authResponse = await _usersService.Register(request);

        if (authResponse is null || !authResponse.Success) return BadRequest(authResponse);

        return Ok(authResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest? request)
    {
        if (request is null) return BadRequest("Invalid login data");
        var authResponse = await _usersService.Login(request);

        if (authResponse is null || !authResponse.Success) return Unauthorized(authResponse);
        return Ok(authResponse);
    }
}