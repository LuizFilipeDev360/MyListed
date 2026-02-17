using AutoMapper;
using MyListed.API.Data;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;

namespace MyListed.API.Services;

public class MediaService
{
    private IMapper _mapper;
    private MediaRepository _repository;

    public MediaService(IMapper mapper, MediaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public IEnumerable<ReadMediaDto> GetAll()
    {
        var result = _repository.GetAll();
        return _mapper.Map<List<ReadMediaDto>>(result);
    }

    public ReadMediaDto? GetById(int id)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return null;
        }
        return _mapper.Map<ReadMediaDto>(item);
    }

    public IEnumerable<ReadMediaDto>? GetByString(string mediaString)
    {
        var items = _repository.GetByString(mediaString);

        return _mapper.Map<List<ReadMediaDto>>(items);
    }

    public Media Create(CreateMediaDto mediaDto)
    {
        var media = _mapper.Map<Media>(mediaDto);
        _repository.Add(media);
        _repository.SaveChanges();
        return media;
    }

    public bool Update(int id, UpdateMediaDto mediaDto)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return false;
        }
        _mapper.Map(mediaDto, item);
        _repository.Update(item);
        _repository.SaveChanges();
        return true;
    }

    public bool PartialUpdate(int id, PartialUpdateMediaDto mediaDto)
    {
        var item = _repository.GetById(id);

        if (item == null)
        {
            return false;
        }

        _mapper.Map(mediaDto, item);
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
