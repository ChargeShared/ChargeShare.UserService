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


    public async Task<IEnumerable<IdentityError>> RegisterUser(UserRegisterDTO dataDto)
    {
        var newUser = new ChargeSharedUserModel
        {
            FirstName = dataDto.FirstName,
            LastName = dataDto.LastName,
            Email = dataDto.Email,
            MiddleName = dataDto.MiddleName,
            DateOfBirth = dataDto.DateOfBirth,
            UserName = dataDto.Email
            
        };

        var result = await _userManager.CreateAsync(newUser, dataDto.Password);

        //Care for Null reference, this will either send the errors back to be used for modelstate or send null back which the ModelState should have no issue with.
        return !result.Succeeded ? result.Errors : null;
    }
}