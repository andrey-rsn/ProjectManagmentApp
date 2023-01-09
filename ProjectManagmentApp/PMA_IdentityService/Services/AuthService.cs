using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PMA_IdentityService.Services
{
    public class AuthService : IAuthService
    {
        public string CreateToken(string UserName)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
