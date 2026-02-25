using MyListed.API.Models;
using AutoMapper;
using MyListed.API.DTOs;

namespace MyListed.API.Profiles;

public class MediaProfile : Profile
{
    public MediaProfile()
    {
        CreateMap<CreateMediaDto, Media>().ForMember(dest => dest.MediaGenres,
            opt => opt.MapFrom(src => 
            src.GenreIds.Select(id => new MediaGenre
                {
                    GenreId = id
                })
            ));
        CreateMap<Media, ReadMediaDto>().ForMember(dest => dest.Kind,
        opt => opt.MapFrom(src => new MediaKindDto
        {
            Id = (int)src.Kind,
            Name = src.Kind.ToString()
        })).ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src =>
                    src.MediaGenres.Select(mg => mg.Genre.Name)
                ));
        CreateMap<UpdateMediaDto, Media>().ForMember(dest => dest.MediaGenres, opt => opt.Ignore());

        CreateMap<PartialUpdateMediaDto, Media>().ForMember(dest => dest.MediaGenres, opt => opt.Ignore()).ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));


    }
}
