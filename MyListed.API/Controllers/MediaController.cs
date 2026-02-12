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
    public IEnumerable<ReadMediaDto> GetMedia([FromQuery] string mediaString = null)
    {
        if (string.IsNullOrEmpty(mediaString))
        {
            return _service.GetAllMedia();
        }
        var media = _service.GetMedia(mediaString);
        return media;
    }

    [HttpGet("{id}")]
    public IActionResult GetMediaById(int id)
    {
        var media = _service.GetMediaById(id);
        if (media == null)
        {
            return NotFound();
        }
        return Ok(media);
    }

    [HttpPost]
    public IActionResult PostMedia([FromBody] CreateMediaDto mediaDto)
    {   
        Media media = _service.AddMedia(mediaDto);
        return CreatedAtAction(nameof(GetMedia), new { id = media.Id }, media);
    }

    [HttpPut("{id}")]
    public IActionResult PutMedia(int id, [FromBody] UpdateMediaDto mediaDto)
    {
        var exist = _service.UpdateMedia(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PatchMedia(int id, PartialUpdateMediaDto mediaDto)
    {
        var exist = _service.PartialUpdateMedia(id, mediaDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();

    }


    [HttpDelete("{id}")]
    public IActionResult DeleteMedia(int id)
    {
        var exist = _service.DeleteMedia(id);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }
}
