using MyListed.API.Models;
using AutoMapper;
using MyListed.API.DTOs;

namespace MyListed.API.Profiles;

public class MediaProfile : Profile
{
    public MediaProfile()
    {
        CreateMap<CreateMediaDto, Media>();
        CreateMap<Media, ReadMediaDto>();
        CreateMap<UpdateMediaDto, Media>();
    }
}
