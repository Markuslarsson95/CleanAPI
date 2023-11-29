using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Authentication;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Commands.Users.LoginUser
{
    public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly JwtOptions _options;
        private readonly MockDatabase _mockDatabase;
        private readonly IConfiguration _configuration;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(MockDatabase mockDatabase, IConfiguration configuration, IJwtProvider jwtProvider, IOptions<JwtOptions> options)
        {
            _mockDatabase = mockDatabase;
            _configuration = configuration;
            _jwtProvider = jwtProvider;
            _options = options.Value;
        }

        public Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var wantedUser = _mockDatabase.Users.FirstOrDefault(x => x.UserName == request.UserLogin.UserName);

            if (wantedUser == null || wantedUser.Password != request.UserLogin.Password)
            {
                return Task.FromResult<string>(null!);
            }

            var token = CreateToken(wantedUser);

            return Task.FromResult(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName)
            };

            if (user.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
