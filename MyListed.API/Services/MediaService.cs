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

    public async Task<IEnumerable<ReadMediaDto>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<List<ReadMediaDto>>(result);
    }

    public async Task<ReadMediaDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return null;
        }

        var dto = _mapper.Map<ReadMediaDto>(item);

        dto.AverageRating = item.UserMedia.Any() ? item.UserMedia.Average(r => r.Rating) : null;

        dto.Reviews = item.UserMedia.Where(um => !string.IsNullOrEmpty(um.Review)).Select(um => um.Review).ToList();

        dto.HowManyAddedToList = item.UserMedia.Count();

        dto.HowManyWatched = item.UserMedia.Count(um => um.Watched is true);

        dto.HowManyLikes = item.UserMedia.Count(um => um.Liked is true);

        return dto;
    }

    public async Task<IEnumerable<ReadMediaDto>?> GetByStringAsync(string mediaString)
    {
        var items = await _repository.GetByStringAsync(mediaString);

        return _mapper.Map<List<ReadMediaDto>>(items);
    }

    public async Task<Media> CreateAsync(CreateMediaDto mediaDto)
    {
        var media = _mapper.Map<Media>(mediaDto);
        _repository.Add(media);
        await _repository.SaveChangesAsync();
        return media;
    }

    public async Task<bool> UpdateAsync(int id, UpdateMediaDto mediaDto)
    {
        var item = await _repository.GetByIdAsync(id);
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
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PartialUpdateAsync(int id, PartialUpdateMediaDto mediaDto)
    {
        var item = await _repository.GetByIdAsync(id);

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

        if(mediaDto.Kind != null)
        {
            item.Kind = mediaDto.Kind.Value;
        }

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
