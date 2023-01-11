using AutoMapper;
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
            var UserId = await _accountService.Login(UserModel.Login, UserModel.Password);

            if(UserId == -1)
            {
                return NotFound();
            }

            var Token = _authService.CreateToken(UserModel.Login);

            return Ok(new
            {
                access_token = Token,
                user_name = UserModel.Login,
                user_Id = UserId
            });
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

    }
}
