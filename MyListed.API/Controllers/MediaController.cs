using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Services;
using System.Threading.Tasks;

namespace MyListed.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    MediaService _service;

    public MediaController(MediaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<ReadMediaDto>> Get([FromQuery] string mediaString = null)
    {
        if (string.IsNullOrEmpty(mediaString))
        {
            return await _service.GetAllAsync();
        }
        var media = await _service.GetByStringAsync(mediaString);
        return media;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var media = await _service.GetByIdAsync(id);
        if (media == null)
        {
            return NotFound();
        }
        return Ok(media);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Post([FromBody] CreateMediaDto mediaDto)
    {   
        Media media = await _service.CreateAsync(mediaDto);
        return CreatedAtAction(nameof(Get), new { id = media.Id }, media);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateMediaDto mediaDto)
    {
        var exist = await _service.UpdateAsync(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Patch(int id, PartialUpdateMediaDto mediaDto)
    {
        var exist = await _service.PartialUpdateAsync(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();

    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var exist = await _service.DeleteAsync(id);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }
}
