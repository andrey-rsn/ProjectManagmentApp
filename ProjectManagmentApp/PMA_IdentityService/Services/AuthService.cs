﻿using Microsoft.IdentityModel.Tokens;
using PMA_IdentityService.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PMA_IdentityService.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> CreateToken(string UserName,string UserId, TimeSpan expires)
        {
            return await Task<string>.Run(() =>
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName), new Claim("UserId", UserId) };

                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(expires),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return encodedJwt;
            });

            
        }

        public async Task<LoginResponseViewModel> CreateLoginRequest(string UserName, int UserId) 
        {
            var UserIdString = Convert.ToString(UserId);
            
            var AccessToken = await CreateToken(UserName, UserIdString, TimeSpan.FromMinutes(15));
            var RefreshToken = await CreateToken(UserName, UserIdString, TimeSpan.FromHours(1));

            var LoginResponse = new LoginResponseViewModel()
            {
                access_token = AccessToken,
                refresh_token = RefreshToken,
                user_name = UserName,
                user_id = UserId
            };

            return LoginResponse;
        }

        public async Task<LoginResponseViewModel> RefreshTokens(string OldRefreshToken)
        {
            var encodedJwt = new JwtSecurityTokenHandler().ReadJwtToken(OldRefreshToken);
            
            if(encodedJwt != null && encodedJwt.ValidTo > DateTime.UtcNow)
            {
                var UserName = encodedJwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

                var UserId = encodedJwt.Claims.FirstOrDefault(x => x.Type == "UserId").Value;

                var AccessToken = await CreateToken(UserName, UserId, TimeSpan.FromMinutes(15));
                var RefreshToken = await CreateToken(UserName, UserId, TimeSpan.FromHours(1));

                var LoginResponse = new LoginResponseViewModel()
                {
                    access_token = AccessToken,
                    refresh_token = RefreshToken,
                    user_name = UserName,
                    user_id = Int32.Parse(UserId)
                };
                return LoginResponse;
            }

            return new LoginResponseViewModel();
        }

    }
}
