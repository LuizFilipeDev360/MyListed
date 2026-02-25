using AutoMapper;
using MyListed.API.Data;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;
using System.Threading.Tasks;

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

    public async Task<bool> UpdateAsync(int id, UpdateMediaDto mediaDto)
    {
        var item = await _repository.GetById(id);
        if (item == null)
        {
            return false;
        }
        _mapper.Map(mediaDto, item);

        item.MediaGenres.Clear();

        item.MediaGenres = mediaDto.GenreIds
            .Select(id => new MediaGenre
            {
                MediaId = item.Id,
                GenreId = id
            }).ToList();

        _repository.Update(item);
        _repository.SaveChanges();
        return true;
    }

    public async Task<bool> PartialUpdateAsync(int id, PartialUpdateMediaDto mediaDto)
    {
        var item = await _repository.GetById(id);

        if (item == null)
        {
            return false;
        }

        _mapper.Map(mediaDto, item);

        if (mediaDto.GenreIds != null)
        {
            item.MediaGenres.Clear();

            item.MediaGenres = mediaDto.GenreIds
                .Select(id => new MediaGenre
                {
                    MediaId = item.Id,
                    GenreId = id
                }).ToList();
        }
        _repository.Update(item);
        _repository.SaveChanges();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var item = await _repository.GetById(id);
        if (item == null)
        {
            return false;
        }
        _repository.Remove(item);
        _repository.SaveChanges();
        return true;
    }
}
