using Shared.Models;

namespace ChargeShare.UserService.DAL.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<ChargeSharedUserModel>> GetAllAsync();
    Task<ChargeSharedUserModel> AddAsync(ChargeSharedUserModel user);
}