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
        await _authService.Register(dto);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAuthDto dto)
    {
        var token = await _authService.Login(dto);
        return Ok(new
        {
            accessToken = token
        });
    }
}
