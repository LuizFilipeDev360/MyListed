using AutoMapper;
using MyListed.API.DTOs;
using MyListed.API.Models;

namespace MyListed.API.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<GenreDto, Genre>();
        CreateMap<Genre, ReadGenreDto>();
    }
}
