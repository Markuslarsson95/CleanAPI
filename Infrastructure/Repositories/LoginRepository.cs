using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MySqlDB _mySqlDb;
        private readonly IConfiguration _configuration;

        public LoginRepository(MySqlDB mySqlDb, IConfiguration configuration)
        {
            _mySqlDb = mySqlDb;
            _configuration = configuration;
        }
        public async Task<string> Login(string userName, string password)
        {
            var wantedUser = _mySqlDb.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (wantedUser == null)
            {
                return await Task.FromResult<string>(null!);
            }

            var token = CreateToken(wantedUser);

            return await Task.FromResult(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            { new Claim(ClaimTypes.Name, user.UserName) };

            if (user.UserName.ToLower() == "admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
                claims.Add(new Claim(ClaimTypes.Role, "User"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("AppSettings:Issuer").Value,
                audience: _configuration.GetSection("AppSettings:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
