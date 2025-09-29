using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Services.Interfaces;

namespace MovieManagementApp.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios y autenticación
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
        /// Registra un nuevo usuario en el sistema
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Información del usuario registrado</returns>
        /// <response code="200">Usuario registrado exitosamente</response>
        /// <response code="400">Si los datos de registro son inválidos</response>
        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            var user = _userService.RegisterAsync(email, password).Result;
            return Ok(user);
        }

        /// <summary>
        /// Autentica un usuario y genera un token JWT
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Objeto con la información del usuario y el token de autenticación</returns>
        /// <response code="200">Login exitoso</response>
        /// <response code="401">Credenciales inválidas</response>
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
        /// Actualiza el rol de un usuario existente
        /// </summary>
        /// <param name="userId">ID del usuario a modificar</param>
        /// <param name="userRole">Nuevo rol a asignar</param>
        /// <returns>Mensaje de confirmación</returns>
        /// <response code="200">Rol actualizado correctamente</response>
        /// <response code="404">Usuario no encontrado</response>
        /// <response code="403">Usuario no autorizado para realizar esta acción</response>
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
