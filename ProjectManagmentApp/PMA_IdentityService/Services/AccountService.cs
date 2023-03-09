using Microsoft.Extensions.Options;
using PMA_IdentityService.Models;
using PMA_IdentityService.Models.DTOs;
using PMA_IdentityService.Repositories;
using System;
using System.Text;

namespace PMA_IdentityService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        private readonly string _passwordKey;

        public AccountService(IUserRepository userRepository, IOptions<SecretKeys> PasswordKeys)
        {
            _userRepository = userRepository;
            _passwordKey = PasswordKeys.Value.localKey; 
        }

        public async Task<UserDTO> Login(string Login, string Password)
        {
            var User = await _userRepository.GetByLogin(Login);

            if (User != null)
            {
                var DecryptPassword = HashService.Decrypt(User.Password, _passwordKey);

                if (string.Equals(DecryptPassword, Password))
                {
                    return User;
                }

            }

            return new UserDTO();
        }

        public async Task<bool> Register(UserDTO UserInfo)
        {
            UserInfo.Password = HashService.Encrypt(UserInfo.Password, _passwordKey);

            var IsAlreadyExists = (await Login(UserInfo.Login, UserInfo.Password)).User_Id != 0;

            if(!IsAlreadyExists)
            {
                await _userRepository.Add(UserInfo);
                return true;
            }

            return false;
        }
    }
}
