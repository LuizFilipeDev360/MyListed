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
    public IEnumerable<ReadMediaDto> Get([FromQuery] string mediaString = null)
    {
        if (string.IsNullOrEmpty(mediaString))
        {
            return _service.GetAll();
        }
        var media = _service.GetByString(mediaString);
        return media;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var media = _service.GetById(id);
        if (media == null)
        {
            return NotFound();
        }
        return Ok(media);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateMediaDto mediaDto)
    {   
        Media media = _service.Create(mediaDto);
        return CreatedAtAction(nameof(Get), new { id = media.Id }, media);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateMediaDto mediaDto)
    {
        var exist = await _service.UpdateAsync(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(int id, PartialUpdateMediaDto mediaDto)
    {
        var exist = await _service.PartialUpdateAsync(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var exist = await _service.Delete(id);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }
}
