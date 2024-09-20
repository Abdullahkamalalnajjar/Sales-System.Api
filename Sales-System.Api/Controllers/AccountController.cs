using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sales_System.Core.Dtos.Identity;
using Sales_System.Core.Entities.Identity;
using Sales_System.Core.Services;
using System.Linq.Expressions;

namespace Sales_System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
           _userManager = userManager;
            _signInManager = signInManager;
           _tokenService=tokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromQuery] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);   
            if (user == null)  return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }   

            var userDto = new UserDto() { 
            
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _tokenService.CreateTokenAsync(user,_userManager),
            };

            return Ok(userDto);

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromQuery] RegisterDto model) {

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            var userDto = new UserDto() {
            
            DisplayName= user.DisplayName,
            Email = user.Email,
            Token=await _tokenService.CreateTokenAsync(user, _userManager),
            };

            return userDto;
        }
      }
}
