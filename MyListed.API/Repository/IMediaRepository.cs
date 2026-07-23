using Microsoft.EntityFrameworkCore;
using MyListed.API.DTOs;
using MyListed.API.Models;

namespace MyListed.API.Repository;

public interface IMediaRepository
{
    Task<IEnumerable<Media>> GetAllAsync();
    Task<Media?> GetByIdAsync(int id);
    Task<IEnumerable<Media>> GetByStringAsync(string s);
    void Add(Media media);
    void Update(Media media);
    void Remove(Media media);
    Task SaveChangesAsync();
}
