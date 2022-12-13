using ChargeShare.UserService.DAL.Contexts;
using Shared.Models;

namespace ChargeShare.UserService.DAL.Repositories;

public class UserRepository
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
}
