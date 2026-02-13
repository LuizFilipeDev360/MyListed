using AutoMapper;
using MyListed.API.Data;
using MyListed.API.DTOs;
using MyListed.API.Models;

namespace MyListed.API.Services;

public class MediaService
{
    private IMapper _mapper;
    private MediaContext _context;

    public MediaService(IMapper mapper, MediaContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IEnumerable<ReadMediaDto> GetAllMedia()
    {
        var result = _context.Media;
        return _mapper.Map<List<ReadMediaDto>>(result);
    }

    public ReadMediaDto? GetMediaById(int id)
    {
        var item = _context.Media.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return null;
        }
        return _mapper.Map<ReadMediaDto>(item);
    }

    public IEnumerable<ReadMediaDto>? GetMedia(string mediaString)
    {
        var items = _context.Media.Where(m => m.Title.ToLower().Contains(mediaString.ToLower()));   //FindAll(m => m.Title.ToLower().Contains(mediaString.ToLower()));

        return _mapper.Map<List<ReadMediaDto>>(items);
    }

    public Media AddMedia(CreateMediaDto mediaDto)
    {
        var media = _mapper.Map<Media>(mediaDto);
        _context.Add(media);
        _context.SaveChanges();
        return media;
    }

    public bool UpdateMedia(int id, UpdateMediaDto mediaDto)
    {
        var item = _context.Media.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return false;
        }
       _mapper.Map(mediaDto, item);
        _context.Media.Update(item);
        _context.SaveChanges();
        return true;
    }

    public bool PartialUpdateMedia(int id, PartialUpdateMediaDto mediaDto)
    {
        var item = _context.Media.FirstOrDefault(m => m.Id == id);

        if (item == null)
        {
            return false;
        }

        _mapper.Map(mediaDto, item);
        _context.Media.Update(item);
        _context.SaveChanges();

        return true;
    }

    public bool DeleteMedia(int id)
    {
        var item = _context.Media.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return false;
        }
        _context.Media.Remove(item);
        _context.SaveChanges();
        return true;
    }
}
