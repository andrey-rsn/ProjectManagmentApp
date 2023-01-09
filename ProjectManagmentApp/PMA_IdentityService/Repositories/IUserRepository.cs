using PMA_IdentityService.Models.DTOs;

namespace PMA_IdentityService.Repositories
{
    public interface IUserRepository : IBaseRepository<UserDTO>
    {
        Task<UserDTO> GetByLoginInfo(string Login, string PasswordHash);
    }
}
