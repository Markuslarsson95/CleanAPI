using Application.Commands.Users.LoginUser;
using Application.Dtos;
using Application.Validators.UserValidators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly LoginValidator _loginValidator;

        public AuthController(IMediator mediator, LoginValidator loginValidator)
        {
            _mediator = mediator;
            _loginValidator = loginValidator;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginUser)
        {
            var validatorResult = _loginValidator.Validate(loginUser);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));

            try
            {
                var loginCommandResult = await _mediator.Send(new LoginUserCommand(loginUser));

                if (loginCommandResult == null)
                    return NotFound("User not found");

                return Ok(loginCommandResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
