using Models.Entities;
using Models.Enums;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<(User? User, string? Token)> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string email, string password, UserRole role = UserRole.Regular);
        Task<bool> UpdateUserRoleAsync(Guid userId, UserRole newRole);
        Task<User?> GetUserByIdAsync(Guid id);
    }
}
