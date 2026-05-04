using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;

namespace MyListed.API.Services;

public class UserMediaService
{
    private IMapper _mapper;
    private UserMediaRepository _repository;

    public UserMediaService(IMapper mapper, UserMediaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ReadUserMediaDto>> GetAllAsync(string userId)
    {
        var result = await _repository.GetAllAsync(userId);
        return _mapper.Map<List<ReadUserMediaDto>>(result);
    }

    public async Task<ReadUserMediaDto?> GetByIdAsync(string userId,int mediaId)
    {
        var result = await _repository.GetByIdAsync(userId, mediaId);
        if (result == null)
            return null;
        return _mapper.Map<ReadUserMediaDto>(result);
    }

    public async Task<bool> CreateAsync(string userId, CreateUserMediaDto dto)
    {
        var exists = await _repository.ExistsAsync(userId, dto.MediaId);


        if (exists)
            return true;

        var entity = new UserMedia
        {
            UserId = userId,
            MediaId = dto.MediaId
        };

        _repository.Add(entity);
        await _repository.SaveChangesAsync();

        return false;

    }

    public async Task<bool> PartialUpdateAsync(string userId,int mediaId, PartialUpdateUserMediaDto userMediaDto)
    {
        var item = await _repository.GetByIdAsync(userId, mediaId);

        if (item == null)
        {
            return false;
        }

        _mapper.Map(userMediaDto, item);

        _repository.Update(item);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(string userId,int mediaId)
    {
        var item = await _repository.GetByIdAsync(userId, mediaId);
        if (item == null)
        {
            return false;
        }
        _repository.Remove(item);
        await _repository.SaveChangesAsync();
        return true;
    }
}
