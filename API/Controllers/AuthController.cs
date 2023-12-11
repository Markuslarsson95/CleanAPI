using Application.Commands.Users.LoginUser;
using Application.Dtos;
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

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginUser)
        {
            var loginCommandResult = await _mediator.Send(new LoginUserCommand(loginUser));

            if (loginCommandResult == null)
                return NotFound("User not found");

            return Ok(loginCommandResult);
        }
    }
}
