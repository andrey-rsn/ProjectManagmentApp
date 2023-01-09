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

        private readonly byte[] _passwordKey;

        public AccountService(IUserRepository userRepository, IOptions<SecretKeys> PasswordKeys)
        {
            _userRepository = userRepository;
            _passwordKey = Encoding.ASCII.GetBytes(PasswordKeys.Value.localKey); 
        }

        public async Task<int> Login(string Login, string Password)
        {
            var PasswordHash = HashService.Encrypt(Password, _passwordKey);

            var User = await _userRepository.GetByLoginInfo(Login, PasswordHash);

            if(User == null)
            {
                return -1;
            }

            return User.User_Id;
        }

        public async Task<bool> Register(UserDTO UserInfo)
        {
            UserInfo.Password = HashService.Encrypt(UserInfo.Password, _passwordKey);

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
