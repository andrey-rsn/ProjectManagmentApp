using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMA_IdentityService.Models.ViewModels;
using PMA_IdentityService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_IdentityService.Controllers
{
    [Route("api/v1/userInfo")]
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        // GET: api/v1/userInfo/{User_Id}
        [HttpGet("{User_Id}")]
        public async Task<ActionResult<UserInfoViewModel>> GetUserInfo(int User_Id)
        {
            var UserInfo = await _userInfoService.GetUserInfo(User_Id);

            if(!string.IsNullOrEmpty(UserInfo.Position)) 
            {
                return Ok(UserInfo);
            }

            return NotFound();
        }

    }
}
