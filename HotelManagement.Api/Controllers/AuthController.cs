using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var register = await _authService.Register(user);
            if(register.ToString().Contains("Successfully")) return Ok(register);
            return BadRequest(register);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var login = await _authService.Login(model);
            if (login.ToString().Contains("Wrong")) return BadRequest(login);
            return Ok(login);
        }

        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {            
            var result = await _authService.ChangePassword(model);
            if(result.ToString().Contains("login")) return Unauthorized(result);
            if(result.ToString().Contains("character")) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {            
            var result = await _authService.ForgottenPassword(model);
            return Ok(result);
        }

        [HttpPost("Reset-Update-Password")]
        public async Task<IActionResult> ResetUpdatePassword([FromBody] UpdatePasswordDTO model, string Token)
        {
            var result = await _authService.ResetPasswordAsync(model);
            return Ok(result);
        }
    }
}
