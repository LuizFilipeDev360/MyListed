using AutoMapper;
using MyListed.API.DTOs;
using MyListed.API.Models;

namespace MyListed.API.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterAuthDto, ApplicationUser>();
    }
}
