using AutoMapper;
using MyListed.API.DTOs;
using MyListed.API.Models;

namespace MyListed.API.Profiles;

public class UserMediaProfile : Profile
{
    public UserMediaProfile()
    {
        CreateMap<UserMedia, ReadUserMediaDto>().ForMember(dest => dest.Titulo,
                opt => opt.MapFrom(src =>
                    src.Media.Title
                ))
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src =>
                    src.Media.ImageUrl
                ));
        CreateMap<PartialUpdateUserMediaDto, UserMedia>().ForMember(dest => dest.UserId, opt => opt.Ignore()).ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Media, opt => opt.Ignore()).ForMember(dest => dest.MediaId, opt => opt.Ignore())
            .ForMember(dest => dest.Liked, opt => opt.Condition((src, dest, srcMember) => src.Liked.HasValue))
            .ForMember(dest => dest.Watched, opt => opt.Condition((src, dest, srcMember) => src.Watched.HasValue))
            .ForMember(dest => dest.Rating, opt => opt.Condition((src, dest, srcMember) => src.Rating.HasValue))
            .ForMember(dest => dest.Review, opt => opt.Condition((src, dest, srcMember) => src.Review != null));
            //.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
