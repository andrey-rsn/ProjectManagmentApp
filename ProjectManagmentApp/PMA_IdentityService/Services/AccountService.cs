using PMA_IdentityService.Models;
using PMA_IdentityService.Models.DTOs;
using PMA_IdentityService.Repositories;

namespace PMA_IdentityService.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserRepository _userRepository;

        public AccountService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Login(string Login, string Password)
        {
            var PasswordHash = HashService.Encrypt(Password, new byte[] { 1, 2, 3 });

            var User = await _userRepository.GetByLoginInfo(Login, PasswordHash);

            if(User == null)
            {
                return -1;
            }

            return User.User_Id;
        }

        public async Task<bool> Register(UserDTO UserInfo)
        {
            UserInfo.Password = HashService.Encrypt(UserInfo.Password, new byte[] { 1, 2, 3 });

            var IsAlreadyExists = (await Login(UserInfo.Email, UserInfo.Password) == -1);

            if(!IsAlreadyExists)
            {
                await _userRepository.Add(UserInfo);
                return true;
            }

            return false;
        }
    }
}
