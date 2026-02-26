using AutoMapper;
using MyListed.API.Data;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;
using System.Threading.Tasks;

namespace MyListed.API.Services;

public class GenreService
{
    private IMapper _mapper;
    private GenreRepository _repository;

    public GenreService(IMapper mapper, GenreRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ReadGenreDto>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<List<ReadGenreDto>>(result);
    }

    public async Task<ReadGenreDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return null;
        }
        return _mapper.Map<ReadGenreDto>(item);
    }

    public async Task<IEnumerable<ReadGenreDto>?> GetByStringAsync(string genreString)
    {
        var items = await _repository.GetByStringAsync(genreString);

        return _mapper.Map<List<ReadGenreDto>>(items);
    }

    public async Task<Genre> CreateAsync(GenreDto genreDto)
    {
        var genre = _mapper.Map<Genre>(genreDto);
        _repository.Add(genre);
        await _repository.SaveChangesAsync();
        return genre;
    }

    public async Task<bool> UpdateAsync(int id, GenreDto genreDto)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return false;
        }
        _mapper.Map(genreDto, item);
        _repository.Update(item);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return false;
        }
        _repository.Remove(item);
        await _repository.SaveChangesAsync();
        return true;
    }
}
