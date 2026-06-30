using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Services;

namespace MyListed.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterAuthDto dto)
    {
        try
        {
            await _authService.Register(dto);

            return Ok(new
            {
                message = "User registered successfully"
            });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAuthDto dto)
    {
        try
        {
            var token = await _authService.Login(dto);
            return Ok(new
            {
                accessToken = token
            });
        }
        catch (ApplicationException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
