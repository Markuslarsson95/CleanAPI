using Domain.Models;
using Infrastructure.RealDatabase;
using Infrastructure.Repositories.Password;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly IConfiguration _configuration;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public LoginRepository(SqlDbContext sqlDbContext, IConfiguration configuration, IPasswordEncryptor passwordEncryptor)
        {
            _sqlDbContext = sqlDbContext;
            _configuration = configuration;
            _passwordEncryptor = passwordEncryptor;
        }
        public async Task<string> Login(string userName, string password)
        {
            try
            {
                Log.Information($"Attempting login for user: {userName}");

                var wantedUser = _sqlDbContext.Users.FirstOrDefault(u => u.UserName == userName);

                if (wantedUser == null || !_passwordEncryptor.Verify(password, wantedUser.Password))
                {
                    Log.Warning($"Login failed for user: {userName}");
                    return await Task.FromResult<string>(null!);
                }

                var token = CreateToken(wantedUser);

                return await Task.FromResult(token);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing user login");
                throw new Exception("An error occurred while processing user login", ex);
            }
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
