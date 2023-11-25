using Application.Commands.Users.AddUser;
using Application.Commands.Users.LoginUser;
using Application.Dtos;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        // Create a new user 
        [HttpPost]
        [Route("addNewUser")]
        //[ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser, IValidator<AddUserCommand> validator)
        {
            var addUserCommand = new AddUserCommand(newUser);

            var validatorResult = await validator.ValidateAsync(addUserCommand);

            if (!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(addUserCommand);

            return Ok(addUserCommand);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDto loginUser)
        {
            var loginUserCommand = new LoginUserCommand(loginUser);
            var loginCommandResult = await _mediator.Send(loginUserCommand);

            if (loginCommandResult == null)
                return NotFound("Password or username is wrong");

            var token = CreateToken(loginCommandResult);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            { new Claim(ClaimTypes.Name, user.UserName) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
