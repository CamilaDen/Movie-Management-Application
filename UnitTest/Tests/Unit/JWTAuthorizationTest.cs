using Microsoft.Extensions.Configuration;
using Models.Entities;
using Models.Enums;
using Moq;
using Service.Authorization;
using System.Security.Claims;

namespace UnitTest.Tests.Unit
{
    public class JWTAuthorizationTests
    {
        [Fact]
        public void GenerateToken_ValidUser_ReturnsToken()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["Jwt:Secret"]).Returns("clave-secreta-para-tests-1234567890123456");
            configMock.Setup(x => x["Jwt:Issuer"]).Returns("test-issuer");
            configMock.Setup(x => x["Jwt:Audience"]).Returns("test-audience");

            var jwt = new JWTAuthorization(configMock.Object);
            var user = new User { Id = 1, Email = "test@test.com", Role = UserRole.Regular };

            // Act
            var token = jwt.GenerateToken(user);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void ValidateToken_ValidToken_ReturnsClaimsPrincipal()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["Jwt:Secret"]).Returns("clave-secreta-para-tests-1234567890123456");
            configMock.Setup(x => x["Jwt:Issuer"]).Returns("test-issuer");
            configMock.Setup(x => x["Jwt:Audience"]).Returns("test-audience");

            var jwt = new JWTAuthorization(configMock.Object);
            var user = new User { Id = 1, Email = "test@test.com", Role = UserRole.Regular };
            var token = jwt.GenerateToken(user);

            // Act
            var principal = jwt.ValidateToken(token);

            // Assert
            Assert.NotNull(principal);
            Assert.Contains(principal.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Regular");
        }
    }
}
