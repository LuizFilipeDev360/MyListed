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
public class GenreController : ControllerBase
{
    GenreService _service;

    public GenreController(GenreService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<ReadGenreDto>> Get([FromQuery] string genreString = null)
    {
        if (string.IsNullOrEmpty(genreString))
        {
            return await _service.GetAllAsync();
        }
        var genre =await _service.GetByStringAsync(genreString);
        return genre;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var genre = await _service.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] GenreDto genreDto)
    {   
        Genre genre = await _service.CreateAsync(genreDto);
        return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, [FromBody] GenreDto genreDto)
    {
        var exist =await _service.UpdateAsync(id, genreDto);
        if (exist != true)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
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
