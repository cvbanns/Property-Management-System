using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            _authService = authService;
            _userManager = userManager;
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
        [HttpPost("Verify Email with Token")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var user = await _userManager.ConfirmEmailAsync(new Core.Domains.User(), token);
            return Ok(user);
        }
    }
}
