using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.Models;

namespace MyListed.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    private static List<Media> _lista = new List<Media>();
    static int _nextId = 1;

    [HttpGet]
    public IEnumerable<Media> GetAllMedia()
    {
        return _lista;
    }

    [HttpPost]
    public IActionResult PostMedia([FromBody] Media media)
    {
        media.Id = _nextId++;
        _lista.Add(media);
        return CreatedAtAction(nameof(GetAllMedia), new { id = media.Id }, media);
    }

    [HttpPut("{id}")]
    public IActionResult PutMedia(int id, [FromBody] Media media)
    {
        var item = _lista.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        item.Title = media.Title;
        item.Description = media.Description;
        item.Year = media.Year;
        item.Type = media.Type;
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteMedia(int id)
    {
        var item = _lista.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        _lista.Remove(item);
        return NoContent();
    }
}
