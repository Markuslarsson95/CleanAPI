using Application.Commands.Users.AddUser;
using Application.Queries.Users.GetById;
using Application.Queries.Users.GetAll;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Application.Commands.Users.UpdateUser;
using Application.Commands.Users.DeleteUser;
using Application.Validators.UserValidators;
using Serilog;
using Application.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly UserCreateValidator _userCreateValidator;
        internal readonly UserValidator _userValidator;

        public UserController(IMediator mediator, UserCreateValidator userCreateValidator, UserValidator userValidator)
        {
            _mediator = mediator;
            _userCreateValidator = userCreateValidator;
            _userValidator = userValidator;
        }

        // Get all users from the database
        [HttpGet]
        [Route("getAllUsers")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userListResault = await _mediator.Send(new GetAllUsersQuery());

                Log.Information("User List found: {@userListResault}", userListResault);
                return Ok(userListResault);
            }
            catch (ArgumentException e)
            {
                Log.Error(e, "An unexpected error occurred while getting all users.");
                return BadRequest(e.Message);
            }
        }

        // Get a User by Id
        [HttpGet]
        [Route("getUserById/{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var getUserResult = await _mediator.Send(new GetUserByIdQuery(userId));

                if (getUserResult == null)
                {
                    Log.Warning($"User with Id {userId} not found.");

                    throw new EntityNotFoundException("User", userId);
                }

                Log.Information("User found: {@getUserResult}", getUserResult);
                return Ok(getUserResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"User not found with Id {userId}");

                return NotFound(ex.Message);
            }
            catch (ArgumentException e)
            {
                Log.Error(e, $"An unexpected error occurred while getting user with ID {userId}.");
                return BadRequest(e.Message);
            }
        }

        // Create a new user 
        [HttpPost]
        [Route("addNewUser")]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] UserCreateDto newUser)
        {
            try
            {
                Log.Information("Validating UserCreateDto");

                var validatorResult = _userCreateValidator.Validate(newUser);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of UserCreateDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(newUser, validationErrors);
                }

                Log.Information("UserCreateDto validation successful. Adding new user: {@newUser}", newUser);

                return Ok(await _mediator.Send(new AddUserCommand(newUser)));
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when adding user");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing AddUserCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        // Update a specific user
        [HttpPut]
        [Route("updateUser/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserById([FromBody] UserDto updatedUser, Guid userId)
        {
            try
            {
                Log.Information($"Updating user with ID {userId}");

                var validatorResult = _userValidator.Validate(updatedUser);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of UserDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(updatedUser, validationErrors);
                }

                var updatedUserResult = await _mediator.Send(new UpdateUserByIdCommand(updatedUser, userId));

                if (updatedUserResult == null)
                {
                    Log.Warning("No user found to update");

                    throw new EntityNotFoundException("User", userId);
                }

                Log.Information("Successfully updated user, updated user: {@updatedUserResult}", updatedUserResult);
                return Ok(updatedUserResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"User not found with Id {userId} during update");

                return NotFound(ex.Message);
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when updating dog");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An unexpected error occurred while updating user with ID {userId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        // Delete user by id
        [HttpDelete]
        [Route("deleteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserById(Guid userId)
        {
            try
            {
                Log.Information($"Deleting user with ID {userId}");
                var userToDelete = await _mediator.Send(new DeleteUserByIdCommand(userId));

                if (userToDelete == null)
                {
                    Log.Warning("No dog found to remove");

                    throw new EntityNotFoundException("User", userId);
                }

                Log.Information("Successfully removed user, removed user: {@userToDelete}", userToDelete);
                return Ok(userToDelete);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"User not found with Id {userId} during remove");

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while deleting user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
