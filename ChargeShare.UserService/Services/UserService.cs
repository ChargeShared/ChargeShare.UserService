using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace ChargeShare.UserService.Services;

public class UserService : IUserService
{
    private readonly UserManager<ChargeSharedUserModel> _userManager;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<ChargeSharedUserModel> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
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

        /*if (result.Succeeded)
        {
            await _userRepository.AddAsync(newUser);
        }*/
    }
}