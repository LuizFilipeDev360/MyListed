using MyListed.API.Models;
using AutoMapper;
using MyListed.API.DTOs;

namespace MyListed.API.Profiles;

public class MediaProfile : Profile
{
    public MediaProfile()
    {
        CreateMap<CreateMediaDto, Media>();
        CreateMap<Media, ReadMediaDto>().ForMember(dest => dest.Kind,
        opt => opt.MapFrom(src => new MediaKindDto
        {
            Id = (int)src.Kind,
            Name = src.Kind.ToString()
        })); ;
        CreateMap<UpdateMediaDto, Media>();
    }
}
