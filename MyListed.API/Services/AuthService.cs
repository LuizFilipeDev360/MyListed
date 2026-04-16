using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyListed.API.DTOs;
using MyListed.API.Models;
using MyListed.API.Repository;

namespace MyListed.API.Services;

public class AuthService
{
    private IMapper _mapper;
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;
    private TokenService _tokenService;

    public AuthService(IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Register(RegisterAuthDto dto)
    {
        ApplicationUser user = _mapper.Map<ApplicationUser>(dto);

        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("User registration failed");
        }

        await _userManager.AddToRoleAsync(user, "User");
    }

    public async Task<string> Login(LoginAuthDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

        if(!result.Succeeded)
        {
            throw new ApplicationException("Invalid login attempt");
        }

        var user = _signInManager.UserManager.Users.FirstOrDefault(u => u.Email == dto.Email);

        var token = await _tokenService.GenerateToken(user);

        return token;
    }


}
