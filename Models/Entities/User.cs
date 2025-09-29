using Models.Enums;

namespace Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!; 
        public string PasswordHash { get; set; } = default!;
        public UserRole Role { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
