using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Services;
using System.Security.Claims;

namespace MyListed.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var user = await _userService.GetUserById(userId);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [Authorize]
    [HttpPut("change-username")]
    public async Task<IActionResult> ChangeUsername(string newUsername)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var userNameExists = await _userService.UserNameExists(newUsername);
        if (userNameExists)
            return BadRequest("Username já está em uso.");

        var result = await _userService.ChangeUsername(newUsername, userId);

        if (!result)
            return BadRequest("Não foi possível alterar o username.");

        return Ok(new { message = "Username atualizado com sucesso." });
    }
}
