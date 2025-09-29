using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Services.Interfaces;

namespace MovieManagementApp.Controllers
{
    /// <summary>
    /// Controller for user management and authentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
          _userService = userService;
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>Registered user information</returns>
        /// <response code="200">User successfully registered</response>
        /// <response code="400">If registration data is invalid</response>
        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            var user = _userService.RegisterAsync(email, password).Result;
            return Ok(user);
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>Object containing user information and authentication token</returns>
        /// <response code="200">Successful login</response>
        /// <response code="401">Invalid credentials</response>
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

        /// <summary>
        /// Updates the role of an existing user
        /// </summary>
        /// <param name="userId">ID of the user to modify</param>
        /// <param name="userRole">New role to assign</param>
        /// <returns>Confirmation message</returns>
        /// <response code="200">Role successfully updated</response>
        /// <response code="404">User not found</response>
        /// <response code="403">User not authorized to perform this action</response>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserRole(int userId, UserRole userRole)
        {
            var success = await _userService.UpdateUserRoleAsync(userId, userRole);
            
            if (!success)
            {
                return NotFound("User not found");
            }
            
            return Ok("Role updated successfully");
        }
    }
}
