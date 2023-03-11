using PMA_IdentityService.Models.ViewModels;

namespace PMA_IdentityService.Services
{
    public interface IUserInfoService
    {
        Task<UserInfoViewModel> GetUserInfo(int User_Id);
        Task<IEnumerable<UserInfoViewModel>> GetUsersInfosByIds(int[] Users_Ids);
    }
}
