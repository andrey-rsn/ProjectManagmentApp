using Microsoft.IdentityModel.Tokens;
using PMA_IdentityService.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PMA_IdentityService.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> CreateToken(string UserName,string UserId,string userRole , TimeSpan expires)
        {
            return await Task<string>.Run(() =>
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName), new Claim("UserId", UserId), new Claim("UserRole", userRole) };

                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(expires),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return encodedJwt;
            });

            
        }

        public async Task<LoginResponseViewModel> CreateLoginRequest(string UserName, int UserId, string userRole) 
        {
            var UserIdString = Convert.ToString(UserId);
            
            var AccessToken = await CreateToken(UserName, UserIdString, userRole, TimeSpan.FromMinutes(30));
            var RefreshToken = await CreateToken(UserName, UserIdString, userRole, TimeSpan.FromHours(5));

            var LoginResponse = new LoginResponseViewModel()
            {
                access_token = AccessToken,
                refresh_token = RefreshToken
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

                var UserRole = encodedJwt.Claims.FirstOrDefault(x => x.Type == "UserRole").Value;

                var AccessToken = await CreateToken(UserName, UserId, UserRole, TimeSpan.FromMinutes(30));
                var RefreshToken = await CreateToken(UserName, UserId, UserRole, TimeSpan.FromHours(5));

                var LoginResponse = new LoginResponseViewModel()
                {
                    access_token = AccessToken,
                    refresh_token = RefreshToken
                };
                return LoginResponse;
            }

            return new LoginResponseViewModel();
        }

    }
}
