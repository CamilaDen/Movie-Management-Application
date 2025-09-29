using Microsoft.Extensions.Configuration;
using Models.Entities;
using Models.Enums;

namespace Repository
{
    public static class DbInitializer
    {
        public static void SeedAdmin(ApplicationDbContext context, IConfiguration configuration)
        {
            if (!context.Users.Any())
            {
                var adminEmail = configuration["AdminUser:Email"];
                var adminPassword = configuration["AdminUser:Password"];
                var adminRole = Enum.Parse<UserRole>(configuration["AdminUser:Role"]);

                var admin = new User
                {
                    Email = adminEmail,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                    Role = adminRole,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}