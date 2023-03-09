using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMA_IdentityService.Models.DTOs;
using PMA_IdentityService.Models.ViewModels;
using PMA_IdentityService.Services;


namespace PMA_IdentityService.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public IdentityController(IAuthService authService, IAccountService accountService, IMapper mapper)
        {
            _authService = authService;
            _accountService = accountService;
            _mapper = mapper;
        }

        // POST api/v1/identity/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserViewModel UserModel)
        {
            var User = await _accountService.Login(UserModel.Login, UserModel.Password);

            if(User.User_Id == 0)
            {
                return NotFound();
            }

            var Result = await _authService.CreateLoginRequest(UserModel.Login, User.User_Id, User.Role);

            return Ok(Result);
        }

        // POST api/v1/identity/registration
        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<string>> Register(UserRegistrationViewModel UserModel)
        {
            var User = _mapper.Map<UserDTO>(UserModel);

            var Result = await _accountService.Register(User);

            if (Result)
            {
                return Ok($"User {User.Login} was succesfully created");
            }

            return BadRequest("Some error occured during registration process");
        }

        // POST api/v1/identity/refresh
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<LoginResponseViewModel>> RefreshToken(string RefreshToken)
        {
            var Result = await _authService.RefreshTokens(RefreshToken);

            if (string.IsNullOrEmpty(Result.refresh_token))
            {
                return Unauthorized();
            }

            return Ok(Result);
        }

        // POST api/v1/identity/validation
        [HttpPost]
        [Authorize]
        [Route("validation")]
        public async Task<ActionResult> ValidateToken()
        {
            return Ok();
        }

    }
}
