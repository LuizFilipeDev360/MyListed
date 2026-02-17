using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Services;

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
    public IActionResult Put(int id, [FromBody] UpdateMediaDto mediaDto)
    {
        var exist = _service.Update(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, PartialUpdateMediaDto mediaDto)
    {
        var exist = _service.PartialUpdate(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();

    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var exist = _service.Delete(id);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }
}
