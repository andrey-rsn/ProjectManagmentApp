using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PMA_IdentityService.Services
{
    public static class AuthOptions
    {
        public const string ISSUER = ""; 
        public const string AUDIENCE = ""; 
        const string KEY = "mysupersecret_secretkey!123";   
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
