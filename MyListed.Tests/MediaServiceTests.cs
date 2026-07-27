using AutoMapper;
using Moq;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;
using MyListed.API.Services;

namespace MyListed.Tests;

public class MediaServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMediaRepository> _repositoryMock;

    public MediaServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IMediaRepository>();
    }

    [Fact]
    public async Task GetMediaByStringWithSuccess()
    {
        //Arrange


        var medias = new List<Media>(){
            new Media {Id = 1, Title = "Duna" },
            new Media {Id = 2, Title = "Duna 2" }
        };

        _repositoryMock.Setup(x => x.GetByStringAsync(It.IsAny<string>())).ReturnsAsync(medias);
        _mapperMock.Setup(x => x.Map<List<ReadMediaDto>>(medias)).Returns(new List<ReadMediaDto>(){
            new ReadMediaDto {Id = 1, Title = "Duna" },
            new ReadMediaDto {Id = 2, Title = "Duna 2" }
        });
        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);
        string title = "Duna";

        //Act

        var result = await mediaService.GetByStringAsync(title);

        //Assert

        Assert.NotNull(result);

        Assert.Contains(result, d => d.Title == title);

        Assert.Equal("Duna", result.First().Title);

        Assert.Equal("Duna 2", result.Last().Title);

        _repositoryMock.Verify(
            x => x.GetByStringAsync(title),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<List<ReadMediaDto>>(It.IsAny<IEnumerable<Media>>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateMediaWithSuccess()
    {
        //Arrange

        var mediaDto = new CreateMediaDto()
        {
            Title = "Duna"
        };

        var media = new Media()
        {
            Title = mediaDto.Title
        };

        var readMediaDto = new ReadMediaDto()
        {
            Title = media.Title
        };

        _mapperMock.Setup(x => x.Map<Media>(mediaDto)).Returns(media);

        _mapperMock.Setup(x => x.Map<ReadMediaDto>(media)).Returns(readMediaDto);

        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.CreateAsync(mediaDto);

        //Assert

        Assert.NotNull(result);

        Assert.Equal(result.Title, mediaDto.Title);

        _repositoryMock.Verify(
           x => x.Add(It.Is<Media>(m => m.Title == mediaDto.Title)),
           Times.Once);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<Media>(It.IsAny<CreateMediaDto>()),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<ReadMediaDto>(It.IsAny<Media>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsyncWithSuccess()
    {
        //Arrange

        var medias = new List<Media>()
        {
            new Media {Id = 1, Title = "Duna" },
            new Media {Id = 2, Title = "Duna 2" },
            new Media {Id = 3, Title = "Blade Runner: O Caçador de Andróides" },
            new Media {Id = 2, Title = "Blade Runner 2049" }
        };

        var listMediaDto = new List<ReadMediaDto>()
        {
            new ReadMediaDto {Id = 1, Title = "Duna" },
            new ReadMediaDto {Id = 2, Title = "Duna 2" },
            new ReadMediaDto {Id = 3, Title = "Blade Runner: O Caçador de Andróides" },
            new ReadMediaDto {Id = 2, Title = "Blade Runner 2049" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(medias);
        _mapperMock.Setup(x => x.Map<List<ReadMediaDto>>(medias)).Returns(listMediaDto);

        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.GetAllAsync();

        //Assert

        Assert.NotNull(result);

        Assert.Equal(result, listMediaDto);

        _repositoryMock.Verify(
            x => x.GetAllAsync(),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<List<ReadMediaDto>>(It.IsAny<IEnumerable<Media>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsyncWithSuccess()
    {
        //Arrange

        int id = 1;

        var media = new Media()
        {
            Id = id,
            Title = "Duna"
        };

        media.UserMedia = new List<UserMedia>
        {
            new UserMedia
            {
                Rating = 10,
                Watched = true,
                Liked = true,
                Review = "Filmaço!",
                User = new ApplicationUser { UserName = "User1" }
            },
            new UserMedia
            {
                Rating = 8,
                Watched = true,
                Liked = false,
                Review = "Bom filme de Ficção!",
                User = new ApplicationUser { UserName = "User2" }
            }
        };


        var readMediaDto = new ReadMediaDto()
        {
            Id = media.Id,
            Title = media.Title,
            HowManyAddedToList = media.UserMedia.Count(),
            HowManyWatched = media.UserMedia.Count(item => item.Watched),
            HowManyLikes = media.UserMedia.Count(item => item.Liked),
            Reviews = media.UserMedia.Where(um => !string.IsNullOrEmpty(um.Review)).Select(um => new ReviewDto
            {
                Review = um.Review,
                UserName = um.User.UserName
            }).ToList(),
            AverageRating = media.UserMedia.Any() ? media.UserMedia.Average(r => r.Rating) : null,
        };

        var totalWatched = 2;
        var totalLiked = 1;
        var totalAddedToList = 2;
        var averageRating = (10 + 8) / 2;

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(media);
        _mapperMock.Setup(x => x.Map<ReadMediaDto>(media)).Returns(readMediaDto);

        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.GetByIdAsync(id);

        //Assert

        Assert.NotNull(result);

        Assert.Equal(id, result.Id);
        Assert.Equal(totalWatched, result.HowManyWatched);
        Assert.Equal(totalLiked, result.HowManyLikes);
        Assert.Equal(totalAddedToList, result.HowManyAddedToList);
        Assert.Equal(averageRating, result.AverageRating);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map<ReadMediaDto>(It.IsAny<Media>()),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsyncNotFoundMedia()
    {
        //Arrange

        int id = 67;

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Media)null);


        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.GetByIdAsync(id);

        //Assert

        Assert.Null(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsyncWithSuccess()
    {
        //Arrange
        int id = 3;

        UpdateMediaDto mediaDto = new UpdateMediaDto()
        {
            Title = "Blade Runner 2049",
            Year = 2019,
            GenreIds = new List<int>()
        };

        Media item = new Media()
        {
            Id = 3,
            Title = "string",
            MediaGenres = new List<MediaGenre>()
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(item);
        _mapperMock.Setup(x => x.Map(mediaDto, item)).Callback<UpdateMediaDto, Media>((dto, media) =>
        {
            media.Id = 3;
            media.Title = dto.Title;
            media.Year = dto.Year;
        });

        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.UpdateAsync(id, mediaDto);

        //Assert

        Assert.True(result);

        Assert.Equal(id, item.Id);
        Assert.Equal(mediaDto.Title, item.Title);
        Assert.Equal(mediaDto.Year, item.Year);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map(It.IsAny<UpdateMediaDto>(),It.IsAny<Media>()),
            Times.Once);

        _repositoryMock.Verify(
           x => x.Update(item),
           Times.Once);

        _repositoryMock.Verify(
          x => x.SaveChangesAsync(),
          Times.Once);
    }

    [Fact]
    public async Task UpdateAsyncNotFoundMedia()
    {
        //Arrange

        int id = 67;

        UpdateMediaDto mediaDto = new UpdateMediaDto()
        {
            Title = "La La Land: Cantando Estações",
            Year = 2016,
            GenreIds = new List<int>()
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Media)null);


        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.UpdateAsync(id, mediaDto);

        //Assert

        Assert.False(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map(It.IsAny<UpdateMediaDto>(), It.IsAny<Media>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.Update(It.IsAny<Media>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task PartialUpdateAsyncWithSuccess()
    {
        //Arrange
        int id = 3;

        PartialUpdateMediaDto mediaDto = new PartialUpdateMediaDto()
        {
            Description = "A descoberta de um segredo enterrado há muito tempo leva a um jovem a encontrar o antigo corredor de lâminas Rick Deckard, que está desaparecido há trinta anos.",
        };

        Media item = new Media()
        {
            Id = 3,
            Title = "Blade Runner 2049",
            Year = 2019,
            MediaGenres = new List<MediaGenre>()
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(item);
        _mapperMock.Setup(x => x.Map(mediaDto, item)).Callback<PartialUpdateMediaDto, Media>((dto, media) =>
        {
            item.Description = dto.Description;
        });

        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.PartialUpdateAsync(id, mediaDto);

        //Assert

        Assert.True(result);

        Assert.Equal(id, item.Id);
        Assert.Equal(mediaDto.Description, item.Description);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map(It.IsAny<PartialUpdateMediaDto>(), It.IsAny<Media>()),
            Times.Once);

        _repositoryMock.Verify(
           x => x.Update(item),
           Times.Once);

        _repositoryMock.Verify(
          x => x.SaveChangesAsync(),
          Times.Once);
    }

    [Fact]
    public async Task PartialUpdateAsyncNotFoundMedia()
    {
        //Arrange

        int id = 32;

        PartialUpdateMediaDto mediaDto = new PartialUpdateMediaDto()
        {
            Description = "Através de uma série de eventos estranhos, um grupo de atores que filmam um filme de guerra de grande orçamento são forçados a se tornarem os soldados que estão retratando.",
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Media)null);


        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.PartialUpdateAsync(id, mediaDto);

        //Assert

        Assert.False(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _mapperMock.Verify(
            x => x.Map(It.IsAny<PartialUpdateMediaDto>(), It.IsAny<Media>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.Update(It.IsAny<Media>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task DeleteAsyncWithSuccess()
    {
        //Arrange

        int id = 5;

        Media item = new Media()
        {
            Id = 5,
            Title = "Devoradores de Estrelas",
            Year = 2026,
            Description = "Um astronauta tenta salvar a Terra enquanto está sozinho no espaço sideral.",
        };

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(item);

        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.DeleteAsync(id);

        //Assert

        Assert.True(result);

        Assert.Equal(id, item.Id);

        _repositoryMock.Verify(
            x => x.Remove(It.IsAny<Media>()),
            Times.Once);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);

    }


    [Fact]
    public async Task DeleteAsyncNotFoundMedia()
    {
        int id = 67;

        _repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Media)null);


        var mediaService = new MediaService(_mapperMock.Object, _repositoryMock.Object);

        //Act

        var result = await mediaService.DeleteAsync(id);

        //Assert

        Assert.False(result);

        _repositoryMock.Verify(
            x => x.GetByIdAsync(id),
            Times.Once);

        _repositoryMock.Verify(
            x => x.Remove(It.IsAny<Media>()),
            Times.Never);

        _repositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }
}