using AutoMapper;
using MyListed.API.DTOs;
using MyListed.API.Models;

namespace MyListed.API.Services;

public class MediaService
{
    private static List<Media> _lista = new List<Media>();
    static int _nextId = 1;

    IMapper _mapper;

    public MediaService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<ReadMediaDto> GetAllMedia()
    {
        return _mapper.Map<List<ReadMediaDto>>(_lista);
    }

    public Media AddMedia(CreateMediaDto mediaDto)
    {
        var media = _mapper.Map<Media>(mediaDto);
        media.Id = _nextId++;
        _lista.Add(media);
        return media;
    }

    public bool UpdateMedia(int id, UpdateMediaDto mediaDto)
    {
        var item = _lista.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return false;
        }
        var media = _mapper.Map<Media>(mediaDto);
        item.Title = media.Title;
        item.Description = media.Description;
        item.Year = media.Year;
        item.Kind = media.Kind;
        return true;
    }

    public bool DeleteMedia(int id)
    {
        var item = _lista.FirstOrDefault(m => m.Id == id);
        if (item == null)
        {
            return false;
        }
        _lista.Remove(item);
        return true;
    }
}
