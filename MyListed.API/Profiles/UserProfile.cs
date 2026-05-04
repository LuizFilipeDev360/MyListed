using MyListed.API.Models;
using AutoMapper;
using MyListed.API.DTOs;

namespace MyListed.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, ReadUserDto>();
    }
}
