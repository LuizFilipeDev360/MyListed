using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;

namespace MyListed.API.Services;

public class UserService
{
    private UserManager<ApplicationUser> _userManager;
    private IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ReadUserDto> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return null;

        var userDto = _mapper.Map<ReadUserDto>(user);

        return userDto;
    }

    public async Task<bool> ChangeUsername(string newUsername, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return false;

        user.UserName = newUsername;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return false;

        return true;
    }

    public async Task<bool> UserNameExists(string newUsername)
    {
        var existingUser = await _userManager.FindByNameAsync(newUsername);
        if (existingUser != null)
            return true;
        return false;
    }

}
