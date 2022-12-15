using ChargeShare.UserService.DAL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ChargeShare.UserService.Services;

public interface IUserService
{
    /// <summary>
    /// Used to register a user in the database
    /// </summary>
    /// <param name="dataDto">This is the IdentityUser/UserReigserDTO object, Make sure to check ModelState validity before saving</param>
    /// <returns>Either: <br/>- NULL on a successful save, Which ModelState can handle itself<br/>
    /// - An IdentityError IEnumerable if it fails ,which needs to be added to ModelStateErrors </returns>
    Task<IEnumerable<IdentityError>> RegisterUser(UserRegisterDTO dataDto);
}