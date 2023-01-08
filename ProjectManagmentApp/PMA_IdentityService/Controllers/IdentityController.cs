using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PMA_IdentityService.Models.ViewModels;
using PMA_IdentityService.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PMA_IdentityService.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        // POST api/v1/identity/login
        [HttpPost("/login")]
        public async Task<ActionResult<string>> Login(UserViewModel userModel)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userModel.UserName) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),  // действие токена истекает через 2 минуты
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                access_token= encodedJwt,
                User= userModel.UserName
            });
        }

        [Authorize]
        [HttpGet("/test")]
        public async Task<string> Test()
        {
            await Task.Delay(1000);
            return "ok";
        }

    }
}
