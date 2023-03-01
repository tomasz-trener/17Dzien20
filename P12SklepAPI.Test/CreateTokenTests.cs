
using Microsoft.Extensions.Configuration;
using Moq;
using P04Sklep.API.Data;
using P04Sklep.API.Services.AuthService;
using P05Sklep.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace P12SklepAPI.Test
{
    public class CreateTokenTests
    {
        private AuthService authService;
        private readonly Mock<DataContext> mockDataContext = new Mock<DataContext>();
        private readonly Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        public CreateTokenTests()
        {
            // potrzebujemy paczki MoQ
            authService = new AuthService(mockConfiguration.Object, mockDataContext.Object);
        }

        [Fact]
        public void CreateToken_ShouldReturnVaildJwtToken()
        {
            //Arrange 
            var user = new User()
            {
                Id = 1,
                Email = "jan@wp.pl",
                Role = "admin",
                DateCreated = DateTime.Now
            };

            mockConfiguration
                .Setup(x => x.GetSection("AppSettings:Token").Value)
                .Returns("my super secret key");

            // Act 
            var result = authService.createToken(user);

            //Assert 

            //odczytanie tokenu 

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(result);

            var roleFromToken = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            Assert.Equal("admin", roleFromToken);

        }


        [Theory]
        [InlineData(1,"jan@wp.pl","admin","2023-01-01", "my top secret key")]
        [InlineData(2, "jan2@wp.pl", "admin", "2023-01-01", "my top secret key")]
        [InlineData(3, "jan2@wp.pl", "admin", "2023-01-01", "my top secret key2")]
        public void CreateToken_ShouldReturnVaildJwtTokenParamaters(
            int id, string email, string role, string date, string tokenKey
            )
        {
            //Arrange 
            var user = new User()
            {
                Id = id,
                Email = email,
                Role = role,
                DateCreated = DateTime.Parse(date)
            };

            mockConfiguration
                .Setup(x => x.GetSection("AppSettings:Token").Value)
                .Returns(tokenKey);

            // Act 
            var result = authService.createToken(user);

            //Assert 

            //odczytanie tokenu 

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(result);

            var roleFromToken = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            Assert.Equal("admin", roleFromToken);
        }
    }
}