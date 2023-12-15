using Application.Commands.Users.AddAnimalToUser;
using Application.Commands.Users.RemoveAnimalFromUser;
using Application.Dtos;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public AnimalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Add animal to user 
        [HttpPut]
        [Route("addAnimalToUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAnimalToUser([FromBody] AnimalUserDto animalToUserDto, IValidator<AnimalUserDto> validator)
        {
            var validatorResult = validator.Validate(animalToUserDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new AddAnimalToUserCommand(animalToUserDto)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Remove animal from user 
        [HttpPut]
        [Route("removeAnimalFromUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveAnimalFromUser([FromBody] AnimalUserDto animalToUserDto, IValidator<AnimalUserDto> validator)
        {
            var validatorResult = validator.Validate(animalToUserDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new RemoveAnimalFromUserCommand(animalToUserDto)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
