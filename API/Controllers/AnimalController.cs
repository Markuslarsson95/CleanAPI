using Application.Commands.Users.AddAnimalToUser;
using Application.Commands.Users.RemoveAnimalFromUser;
using Application.Dtos;
using Application.Exceptions;
using Application.Validators;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly AnimalUserValidator _validator;

        public AnimalController(IMediator mediator, AnimalUserValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        // Add animal to user 
        [HttpPut]
        [Route("addAnimalToUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAnimalToUser([FromBody] AnimalUserDto animalUserDto)
        {
            try
            {
                var validatorResult = _validator.Validate(animalUserDto);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of AnimalUserDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(animalUserDto, validationErrors);
                }

                var command = await _mediator.Send(new AddAnimalToUserCommand(animalUserDto));

                if (command == null)
                {
                    Log.Error("Animal or user not found");

                    return BadRequest("Animal or user not found");
                }

                return Ok(command);
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when adding animal to user");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing AddAnimalToUserCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        // Remove animal from user 
        [HttpPut]
        [Route("removeAnimalFromUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveAnimalFromUser([FromBody] AnimalUserDto animalUserDto)
        {
            try
            {
                var validatorResult = _validator.Validate(animalUserDto);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of AnimalUserDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(animalUserDto, validationErrors);
                }

                var command = await _mediator.Send(new RemoveAnimalFromUserCommand(animalUserDto));

                if (command == null)
                {
                    Log.Error("Animal or user not found");

                    return BadRequest("Animal or user not found");
                }

                return Ok(command);
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when removing animal from user");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing RemoveAnimalFromUserCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }
    }
}
