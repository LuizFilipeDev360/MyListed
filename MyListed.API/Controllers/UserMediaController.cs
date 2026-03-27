using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyListed.API.Data;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Services;
using System.Security.Claims;

namespace MyListed.API.Controllers;


[Route("[controller]")]
[ApiController]
public class UserMediaController : ControllerBase
{

    UserMediaService _service;

    public UserMediaController(UserMediaService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<ReadUserMediaDto>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //tenho que retornar algo se nao tiver userId

        return await _service.GetAllAsync(userId);
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(CreateUserMediaDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        bool exists = await _service.CreateAsync(userId, dto);

        if (exists)
            return BadRequest("Você já adicionou essa mídia.");

        return Ok();
    }

    [HttpPatch("{mediaId}")]
    [Authorize]
    public async Task<IActionResult> Patch(int mediaId, PartialUpdateUserMediaDto mediaDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var exist = await _service.PartialUpdateAsync(userId, mediaId, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();

    }

    [HttpDelete("{mediaId}")]
    [Authorize]
    public async Task<IActionResult> Delete(int mediaId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var exist = await _service.DeleteAsync(userId ,mediaId);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }
}
