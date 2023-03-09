using PMA_IdentityService.Models.DTOs;

namespace PMA_IdentityService.Services
{
    public interface IAccountService
    {
        Task<UserDTO> Login(string Login, string Password);
        Task<bool> Register(UserDTO UserInfo);
    }
}
