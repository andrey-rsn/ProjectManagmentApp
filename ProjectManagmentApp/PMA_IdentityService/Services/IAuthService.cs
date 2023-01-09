namespace PMA_IdentityService.Services
{
    public interface IAuthService
    {
        string CreateToken(string UserName);
    }
}
