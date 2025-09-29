using Models.Entities;
using Models.Enums;
using Repository.Interfaces;
using Service.Authorization;
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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role
            };

            return await _userRepository.CreateAsync(user);
        }

        public async Task<bool> UpdateUserRoleAsync(int userId, UserRole newRole)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.Role = newRole;
            return await _userRepository.UpdateAsync(user);
        }
    }
}
