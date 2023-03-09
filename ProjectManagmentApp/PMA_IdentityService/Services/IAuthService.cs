using PMA_IdentityService.Models.ViewModels;

namespace PMA_IdentityService.Services
{
    public interface IAuthService
    {
        Task<string> CreateToken(string UserName, string UserId, string UserRole, TimeSpan expires);
        Task<LoginResponseViewModel> CreateLoginRequest(string UserName, int UserId, string UserRole);
        Task<LoginResponseViewModel> RefreshTokens(string OldRefreshToken);
    }
}
