using AutoMapper;
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
    public IEnumerable<ReadMediaDto> GetAllMedia()
    {
        return _service.GetAllMedia();
    }

    [HttpPost]
    public IActionResult PostMedia([FromBody] CreateMediaDto mediaDto)
    {   
        Media media = _service.AddMedia(mediaDto);
        return CreatedAtAction(nameof(GetAllMedia), new { id = media.Id }, media);
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
