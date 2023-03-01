using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using P04Sklep.API.Data;
using P05Sklep.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace P04Sklep.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly DataContext context;


        public AuthService(IConfiguration configuration, DataContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<ServiceReponse<string>> Login(string email, string passwrod)
        {

            // tutaj docelowo sprawdzimy w bazie czy istnieje uzytnik 
            // o podanym adresie email i hasle ....

            //User user = new User()
            //{
            //    Id = 1,
            //    Email = "user@user.pl",
            //    DateCreated = DateTime.Now,
            //    Role="Admin"
            //};
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());


            var response = new ServiceReponse<string>();
            if (user == null)
            {
                response.Success = false;
                response.Message = "user not found";
            }else if(!VerifyPasswordHash(passwrod, user.PasswordHash, user.PasswrodSalt))
            {
                response.Success = false;
                response.Message = "wrong passwrod";
            }
            else
            {
                string token = createToken(user);
                response.Data = token;
            }            
           
            return response;
        }

        private async Task<bool> userExists(string email)
        {
            if (await context.Users.AnyAsync(x=>x.Email.ToLower() == email.ToLower()))
                return true;
           
            return false;
        }

        public async Task<ServiceReponse<int>> Register(string email, string passwrod)
        {

            if (await userExists(email))
                return new ServiceReponse<int> { Success = false, Message="User already exists" };

            User user = new User();
            user.Email = email;


            // potrzebujemy tera metody do wygnenrowania hashu hasła i soli dla dane hasła 
            CreatePasswordHash(passwrod, out byte[] passwordHash, out byte[] passwordSal);
            user.PasswordHash = passwordHash;
            user.PasswrodSalt = passwordSal;

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new ServiceReponse<int> { Data = user.Id, Message = "Success registration" };
        }

        // hasło:  "ala ma kota" -> 01


        public bool VerifyPasswordHash(string UserPassword, byte[] fromDbPasswordHash,  byte[] fromDbpasswordSalt)
        {
            using (var hmac = new HMACSHA512(fromDbpasswordSalt))
            {
               var passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(UserPassword));

                return passwordHash.SequenceEqual(fromDbPasswordHash);
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string createToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("myCustomDate", user.DateCreated.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken(
                claims : claims,    // meta dane uzytkownika
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
