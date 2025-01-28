using InternIntelligence_UserLogin.DTOs;
using InternIntelligence_UserLogin.Models;
using InternIntelligence_UserLogin.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternIntelligence_UserLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;

        public UserLoginController(LoginService loginService, UserManager<User> userManager, UserService userService)
        {
            _loginService = loginService;
            _userManager = userManager;
            _userService = userService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(LoginDto dto)
        {
            var user = new User
            {
                UserName = dto.Username,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                return Ok(new { Status = "Success", Message = "User created successfully!" });
            }

            return Ok(new { Status = "Error", Message = "User creation failed!", Errors = result.Errors });
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto dto)
        { 

            if (string.IsNullOrWhiteSpace(dto.Username))
                return BadRequest(new { Status = "Error", Message = "username can`t be empty!" });

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { Status = "Error", Message = "password can`t be empty!" });

            var result = await _loginService.SignInAsync(dto.Username, dto.Password);
            if (result.Success)
            {
                return Ok(new { Token = result.Token, Expiration = result.Expiration });
            }

            return Unauthorized(new { Status = "Error", Message = "Invalid username or password" });
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> Logout([FromBody] string password)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetByIdUser(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found!" });
            }
            var check = await _userManager.CheckPasswordAsync(user, password);
            if (check)
            {
                await _loginService.Logout();
                return Ok(new { message = "Logout successful!" });
            }
            return BadRequest(new { message = "Password dont correct!" });

        }
    }
}
