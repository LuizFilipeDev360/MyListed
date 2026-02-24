using AutoMapper;
using MyListed.API.Data;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;

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

    public IEnumerable<ReadGenreDto> GetAll()
    {
        var result = _repository.GetAll();
        return _mapper.Map<List<ReadGenreDto>>(result);
    }

    public ReadGenreDto? GetById(int id)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return null;
        }
        return _mapper.Map<ReadGenreDto>(item);
    }

    public IEnumerable<ReadGenreDto>? GetByString(string genreString)
    {
        var items = _repository.GetByString(genreString);

        return _mapper.Map<List<ReadGenreDto>>(items);
    }

    public Genre Create(GenreDto genreDto)
    {
        var genre = _mapper.Map<Genre>(genreDto);
        _repository.Add(genre);
        _repository.SaveChanges();
        return genre;
    }

    public bool Update(int id, GenreDto genreDto)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return false;
        }
        _mapper.Map(genreDto, item);
        _repository.Update(item);
        _repository.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return false;
        }
        _repository.Remove(item);
        _repository.SaveChanges();
        return true;
    }
}
