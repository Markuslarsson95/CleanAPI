using Application.Commands.Users.AddUser;
using Application.Queries.Users.GetById;
using Application.Queries.Users.GetAll;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Application.Commands.Users.UpdateUser;
using FluentValidation;
using Application.Commands.Users.DeleteUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all users from database
        [HttpGet]
        [Route("getAllUsers")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _mediator.Send(new GetAllUsersQuery());

            if (userList.IsNullOrEmpty())
                return NotFound("No users found");

            return Ok(userList);
        }

        // Get a User by Id
        [HttpGet]
        [Route("getUserById/{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var getUserResult = await _mediator.Send(new GetUserByIdQuery(userId));

            if (getUserResult == null)
                return NotFound($"User with ID {userId} not found");

            return Ok(getUserResult);
        }

        // Create a new user 
        [HttpPost]
        [Route("addNewUser")]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser, IValidator<AddUserCommand> validator)
        {
            var validatorResult = validator.Validate(new AddUserCommand(newUser));

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.ToString());
            }

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
        public async Task<IActionResult> UpdateUserById([FromBody] UserDto updatedUser, Guid userId, IValidator<UpdateUserByIdCommand> validator)
        {
            var updateUserCommand = new UpdateUserByIdCommand(updatedUser, userId);
            var validatorResult = validator.Validate(updateUserCommand);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.ToString());

            var updatedUserResult = await _mediator.Send(updateUserCommand);

            if (updatedUserResult == null)
                return NotFound($"User with ID {userId} not found");

            return Ok(updatedUserResult);
        }

        // Delete user by id
        [HttpDelete]
        [Route("deleteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserById(Guid userId)
        {
            var userToDelete = await _mediator.Send(new DeleteUserByIdCommand(userId));

            if (userToDelete == null)
                return NotFound($"User with ID {userId} not found");

            return Ok(userToDelete);
        }
    }
}
