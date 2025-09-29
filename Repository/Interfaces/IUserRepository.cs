using Models.Entities;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
