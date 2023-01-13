using PMA_IdentityService.Models.ViewModels;

namespace PMA_IdentityService.Services
{
    public interface IAuthService
    {
        Task<string> CreateToken(string UserName, string UserId, TimeSpan expires);
        Task<LoginResponseViewModel> CreateLoginRequest(string UserName, int UserId);
        Task<LoginResponseViewModel> RefreshTokens(string OldRefreshToken);
    }
}
