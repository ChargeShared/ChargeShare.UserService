using ChargeShare.UserService.DAL.DTOs;

namespace ChargeShare.UserService.Services;

public interface IUserService
{
    Task RegisterUser(UserRegisterDTO dataDto);
}