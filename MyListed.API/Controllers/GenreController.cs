using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Services;

namespace MyListed.API.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    GenreService _service;

    public GenreController(GenreService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<ReadGenreDto> Get([FromQuery] string genreString = null)
    {
        if (string.IsNullOrEmpty(genreString))
        {
            return _service.GetAll();
        }
        var genre = _service.GetByString(genreString);
        return genre;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var genre = _service.GetById(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }

    [HttpPost]
    public IActionResult Post([FromBody] GenreDto genreDto)
    {   
        Genre genre = _service.Create(genreDto);
        return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] GenreDto genreDto)
    {
        var exist = _service.Update(id, genreDto);
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
