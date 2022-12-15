using ChargeShare.UserService.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ChargeShare.UserService.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext _userContext;

    public UserRepository(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<IEnumerable<ChargeSharedUserModel>> GetAllAsync()
    {
        return _userContext.Users;
    }

    public async Task<ChargeSharedUserModel> AddAsync(ChargeSharedUserModel user)
    {
        _userContext.Users.Add(user); // dit doet nog geen query uitvoeren
        await _userContext.SaveChangesAsync(); // deze wel
        return user;
    }
}
