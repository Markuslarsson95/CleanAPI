using Application.Commands.Users.LoginUser;
using Application.Dtos;
using Application.Exceptions;
using Application.Validators.UserValidators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginUser)
        {
            try
            {
                Log.Information("Validating LoginDto");

                var validatorResult = _loginValidator.Validate(loginUser);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of LoginDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(loginUser, validationErrors);
                }

                var loginCommandResult = await _mediator.Send(new LoginUserCommand(loginUser));

                if (loginCommandResult == null)
                    return NotFound("User not found");

                Log.Information($"Successfully logged in to user: {loginUser.UserName}");
                return Ok(loginCommandResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing LoginUserCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }
    }
}
