using PMA_IdentityService.Models.DTOs;
using System.Collections.Generic;

namespace PMA_IdentityService.Repositories
{
    public interface IUserRepository : IBaseRepository<UserDTO>
    {
        Task<UserDTO> GetByLogin(string Login);
        Task<UserDTO> GetByEmail(string Email);
    }
}
