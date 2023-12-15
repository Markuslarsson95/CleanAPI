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

        // Get all users from database
        [HttpGet]
        [Route("getAllUsers")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllUsersQuery()));
            }
            catch (ArgumentException e)
            {
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
                    return NotFound($"User with ID {userId} not found");

                return Ok(getUserResult);
            }
            catch (ArgumentException e)
            {
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
            var validatorResult = _userCreateValidator.Validate(newUser);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));

            try
            {
                return Ok(await _mediator.Send(new AddUserCommand(newUser)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            var validatorResult = _userValidator.Validate(updatedUser);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));

            try
            {
                var updatedUserResult = await _mediator.Send(new UpdateUserByIdCommand(updatedUser, userId));

                if (updatedUserResult == null)
                    return NotFound($"User with ID {userId} not found");

                return Ok(updatedUserResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
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
                var userToDelete = await _mediator.Send(new DeleteUserByIdCommand(userId));

                if (userToDelete == null)
                    return NotFound($"User with ID {userId} not found");

                return Ok(userToDelete);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
