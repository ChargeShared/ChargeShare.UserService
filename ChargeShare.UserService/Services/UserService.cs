using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Shared.Models;
using System.Numerics;

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


    public async Task<ChargeSharedUserModel> RegisterUser(UserRegisterDTO dataDto)
    {

        var userExists = await _userManager.FindByEmailAsync(dataDto.Email);
        if (userExists != null)
            throw new Exception("User already exists!");

        var newUser = new ChargeSharedUserModel
        {
            FirstName = dataDto.FirstName,
            LastName = dataDto.LastName,
            Email = dataDto.Email,
            MiddleName = dataDto.MiddleName,
            DateOfBirth = dataDto.DateOfBirth,
            UserName = dataDto.Email,
            
        };

        newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, dataDto.Password);

        var result = await _userManager.CreateAsync(newUser);

        if (!result.Succeeded)
        {
            throw new Exception("Could not register user!");
        }

        //Care for Null reference, this will either send the errors back to be used for modelstate or send null back which the ModelState should have no issue with.
        return newUser;
    }
}