using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace ChargeShare.UserService.Services;

public class UserService
{
    private readonly UserManager<ChargeSharedUserModel> _userManager;
    private readonly SignInManager<ChargeSharedUserModel> _signInManager;
    private readonly UserRepository _userRepository;

    public UserService(UserManager<ChargeSharedUserModel> userManager, SignInManager<ChargeSharedUserModel> signInManager, UserRepository userRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
    }

    public async Task RegisterUser(UserRegisterDTO dataDto)
    {
        var newUser = new ChargeSharedUserModel
        {
            FirstName = dataDto.FirstName,
            LastName = dataDto.LastName,
            Email = dataDto.Email,
            MiddleName = dataDto.MiddleName,
            DateOfBirth = dataDto.DateOfBirth
        };

        var result = await _userManager.CreateAsync(newUser, dataDto.Password);

        if (result.Succeeded)
        {
            _userRepository.AddAsync(newUser);
        }
    }
}