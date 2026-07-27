using AutoMapper;
using Moq;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;
using MyListed.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListed.Tests;

public class GenreServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IGenreRepository> _repositoryMock;

    public GenreServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IGenreRepository>();
    }

    [Fact]
    public async Task GetAllGenreAsyncWithSuccess()
    {
        //Arrange
        var genres = new List<Genre>()
        {
            new Genre {Id = 1, Name = "Ficção Científica" },
            new Genre {Id = 2, Name = "Ação" }

        };

        var listGenreDto = new List<ReadGenreDto>()
        {
            new ReadGenreDto {Id = 1, Name = "Ficção Científica" },
            new ReadGenreDto {Id = 2, Name = "Ação" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(genres);
        _mapperMock.Setup(x => x.Map<List<ReadGenreDto>>(genres)).Returns(listGenreDto);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.GetAllAsync();

        //Assert

        Assert.NotNull(result);

        Assert.Equal(result, listGenreDto);

        _repositoryMock.Verify(
            x => x.GetAllAsync(),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<List<ReadGenreDto>>(It.IsAny<IEnumerable<Genre>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetGenreByStringWithSuccess()
    {
        //Arrange

        var genres = new List<Genre>()
        {
            new Genre {Id = 2, Name = "Ação" }
        };

        var listGenreDto = new List<ReadGenreDto>()
        {
            new ReadGenreDto {Id = 2, Name = "Ação" }
        };

        _repositoryMock.Setup(x => x.GetByStringAsync("Ação")).ReturnsAsync(genres);
        _mapperMock.Setup(x => x.Map<List<ReadGenreDto>>(genres)).Returns(listGenreDto);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        string name = "Ação";

        //Act

        var result = await genreService.GetByStringAsync(name);

        //Assert

        Assert.NotNull(result);

        Assert.Contains(result, d => d.Name == name);

        Assert.Equal("Ação", result.First().Name);

        _repositoryMock.Verify(
            x => x.GetByStringAsync(name),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<List<ReadGenreDto>>(It.IsAny<IEnumerable<Genre>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetGenreByIdAsyncWithSuccess()
    {
        //Arrange

        int id = 1;

        var genre = new Genre()
        {
            Id = id,
            Name = "Ficção Científica"
        };

        var readGenreDto = new ReadGenreDto()
        {
            Id = genre.Id,
            Name = genre.Name
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(genre);
        _mapperMock.Setup(x => x.Map<ReadGenreDto>(genre)).Returns(readGenreDto);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.GetByIdAsync(id);

        //Assert

        Assert.NotNull(result);

        Assert.Equal(id, result.Id);
        Assert.Equal(genre.Name, result.Name);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<ReadGenreDto>(It.IsAny<Genre>()),
            Times.Once);
    }

    [Fact]
    public async Task GetGenreByIdAsyncNotFoundGenre()
    {
        //Arrange

        int id = 67;

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Genre)null);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.GetByIdAsync(id);

        //Assert

        Assert.Null(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<ReadGenreDto>(It.IsAny<Genre>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateGenreWithSuccess()
    {
        //Arrange

        var genreDto = new GenreDto()
        {
            Name = "Aventura"
        };

        var genre = new Genre()
        {
            Name = genreDto.Name
        };

        var readGenreDto = new ReadGenreDto()
        {
            Name = genreDto.Name
        };

        _mapperMock.Setup(x => x.Map<Genre>(genreDto)).Returns(genre);
        _mapperMock.Setup(x => x.Map<ReadGenreDto>(genre)).Returns(readGenreDto);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.CreateAsync(genreDto);

        //Assert

        Assert.NotNull(result);

        Assert.Equal(result.Name, genreDto.Name);

        _repositoryMock.Verify(
           x => x.Add(It.Is<Genre>(m => m.Name == genreDto.Name)),
           Times.Once);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<Genre>(It.IsAny<GenreDto>()),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<ReadGenreDto>(It.IsAny<Genre>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateGenreAsyncWithSuccess()
    {
        //Arrange
        int id = 2;

        GenreDto genreDto = new GenreDto()
        {
            Name = "Comédia"
        };

        Genre genre = new Genre()
        {
            Id = 2,
            Name = "Ação"
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(genre);
        _mapperMock.Setup(x => x.Map(genreDto, genre)).Callback<GenreDto, Genre>((dto, genre) =>
        {
            genre.Id = 2;
            genre.Name = dto.Name;
        });

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.UpdateAsync(id, genreDto);

        //Assert

        Assert.True(result);

        Assert.Equal(id, genre.Id);
        Assert.Equal(genreDto.Name, genre.Name);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map(It.IsAny<GenreDto>(), It.IsAny<Genre>()),
            Times.Once);

        _repositoryMock.Verify(
           x => x.Update(genre),
           Times.Once);

        _repositoryMock.Verify(
          x => x.SaveChangesAsync(),
          Times.Once);
    }

    [Fact]
    public async Task UpdateGenreAsyncNotFoundGenre()
    {
        //Arrange

        int id = 67;

        GenreDto genreDto = new GenreDto()
        {
            Name = "Fantasia"
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Genre)null);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.UpdateAsync(id, genreDto);

        //Assert

        Assert.False(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map(It.IsAny<GenreDto>(), It.IsAny<Genre>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.Update(It.IsAny<Genre>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task DeleteGenreAsyncWithSuccess()
    {
        //Arrange

        int id = 5;

        Genre item = new Genre()
        {
            Id = 5,
            Name = "Drama"
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(item);

        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.DeleteAsync(id);

        //Assert

        Assert.True(result);

        Assert.Equal(id, item.Id);

        _repositoryMock.Verify(
            x => x.Remove(It.IsAny<Genre>()),
            Times.Once);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteGenreAsyncNotFoundGenre()
    {
        int id = 67;

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Genre)null);


        var genreService = new GenreService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await genreService.DeleteAsync(id);

        //Assert

        Assert.False(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _repositoryMock.Verify(
            x => x.Remove(It.IsAny<Genre>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }
}
