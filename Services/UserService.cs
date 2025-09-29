using System.Security.Cryptography;
using System.Text;
using Models.Entities;
using Models.Enums;
using Service.Authorization;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTAuthorization _jwtAuthorization;

        public UserService(IUserRepository userRepository, JWTAuthorization jwtAuthorization)
        {
            _userRepository = userRepository;
            _jwtAuthorization = jwtAuthorization;
        }

        public async Task<(User? User, string? Token)> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return (null, null);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return (null, null);

            var token = _jwtAuthorization.GenerateToken(user);
            return (user, token);
        }

        public async Task<User> RegisterAsync(string email, string password, UserRole role = UserRole.Regular)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new InvalidOperationException("The user already exist");

            var user = new User
            {
                Email = email,
                PasswordHash = HashPassword(password),
                Role = role
            };

            return await _userRepository.CreateAsync(user);
        }

        public async Task<bool> UpdateUserRoleAsync(Guid userId, UserRole newRole)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.Role = newRole;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
