using Application.Commands.Users.AddUser;
using Application.Commands.Users.LoginUser;
using Application.Dtos;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create a new user 
        [HttpPost]
        [Route("addNewUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromBody] UserDto userDto)
        {
            var loginUserCommand = new LoginUserCommand(userDto);
            var jwtToken = await _mediator.Send(loginUserCommand);

            if (jwtToken is null)
                return NotFound("User not found");

            return Ok(jwtToken);
        }
    }
}
