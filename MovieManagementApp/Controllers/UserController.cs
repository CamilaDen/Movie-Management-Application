using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace MovieManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
          _userService = userService;
        }

        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            return Ok(_userService.RegisterAsync(email, password));
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var (user, token) = _userService.LoginAsync(email, password).Result;
            if (user == null || token == null)
            {
                return Unauthorized("Invalid credentials");
            }
            return Ok(new { User = user, Token = token });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok("User details");
        }
    }
}
