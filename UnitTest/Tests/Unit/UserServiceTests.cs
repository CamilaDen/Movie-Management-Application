using Microsoft.Extensions.Configuration;
using Models.Entities;
using Models.Enums;
using Moq;
using Repository.Interfaces;
using Service.Authorization;
using Services;

namespace MovieManagementApp.Tests.Unit
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }
            
        [Fact]
        public async Task RegisterAsync_NewUser_ReturnsCreatedUser()
        {
            // Arrange
            var email = "test@test.com";
            var password = "password123";
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email))
                             .ReturnsAsync((User?)null);
            _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>()))
                             .ReturnsAsync((User u) => u);

            // Mock JWTAuthorization solo para cumplir la dependencia
            var jwtMock = new Mock<JWTAuthorization>(MockBehavior.Loose, new Mock<IConfiguration>().Object);

            var userService = new UserService(_userRepositoryMock.Object, jwtMock.Object);

            // Act
            var result = await userService.RegisterAsync(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
            Assert.Equal(UserRole.Regular, result.Role);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsUserAndToken()
        {
            // Arrange
            var email = "test@test.com";
            var password = "password123";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Email = email, PasswordHash = hashedPassword };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["Jwt:Secret"]).Returns("clave-secreta-para-tests-1234567890123456");
            configMock.Setup(x => x["Jwt:Issuer"]).Returns("test-issuer");
            configMock.Setup(x => x["Jwt:Audience"]).Returns("test-audience");

            var jwt = new JWTAuthorization(configMock.Object);

            // Act
            var userService = new UserService(_userRepositoryMock.Object, jwt);
            var result = await userService.LoginAsync(email, password);

            // Assert
            Assert.NotNull(result.User);
            Assert.NotNull(result.Token);
            Assert.Equal(email, result.User.Email);
            Assert.Equal(hashedPassword, result.User.PasswordHash);
            _userRepositoryMock.Verify(x => x.GetByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var email = "test@test.com";
            var password = "password123";
            var wrongPassword = "wrongpass";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Email = email, PasswordHash = hashedPassword };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            var jwtMock = new Mock<JWTAuthorization>(MockBehavior.Loose, new Mock<IConfiguration>().Object);

            var userService = new UserService(_userRepositoryMock.Object, jwtMock.Object);

            // Act
            var result = await userService.LoginAsync(email, wrongPassword);

            // Assert
            Assert.Null(result.User);
            Assert.Null(result.Token);
        }

        [Fact]
        public async Task UpdateUserRoleAsync_ExistingUser_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Role = UserRole.Regular };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
                             .ReturnsAsync(user);
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                             .ReturnsAsync(true);

            var jwtMock = new Mock<JWTAuthorization>(MockBehavior.Loose, new Mock<IConfiguration>().Object);

            var userService = new UserService(_userRepositoryMock.Object, jwtMock.Object);

            // Act
            var result = await userService.UpdateUserRoleAsync(userId, UserRole.Admin);

            // Assert
            Assert.True(result);
            Assert.Equal(UserRole.Admin, user.Role);
        }
    }
  
}