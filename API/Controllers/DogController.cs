using Application.Commands.Dogs;
using Application.Commands.Dogs.UpdateDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly ILogger<DogController> _logger;

        internal readonly IMediator _mediator;
        public DogController(IMediator mediator, ILogger<DogController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        [ProducesResponseType(typeof(List<Dog>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDogs()
        {
            var dogList = await _mediator.Send(new GetAllDogsQuery());

            if (dogList.Count <= 0)
            {
                return NotFound("No dogs found");
            }

            return Ok(dogList);
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            var getDogResult = await _mediator.Send(new GetDogByIdQuery(dogId));

            if (getDogResult == null)
                return NotFound($"Dog with ID {dogId} not found");

            return Ok(getDogResult);
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        //[Authorize]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog, IValidator<AddDogCommand> validator)
        {
            var addDogCommand = new AddDogCommand(newDog);

            var validatorResult = await validator.ValidateAsync(addDogCommand);

            if (!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(addDogCommand);

            return Ok(addDogCommand);
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        //[Authorize]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDogById([FromBody] DogDto updatedDog, Guid dogId, IValidator<UpdateDogByIdCommand> validator)
        {
            var updateDogCommand = new UpdateDogByIdCommand(updatedDog, dogId);
            var updatedDogResult = await _mediator.Send(updateDogCommand);

            if (updatedDogResult == null)
                return NotFound($"Dog with ID {dogId} not found");

            var validatorResult = await validator.ValidateAsync(updateDogCommand);

            if (!validatorResult.IsValid)
                return ValidationProblem(validatorResult.ToString());

            return Ok(updateDogCommand);
        }

        // Delete dog by id
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDogById(Guid dogId)
        {
            var dogToDelete = await _mediator.Send(new DeleteDogByIdCommand(dogId));

            if (dogToDelete == null)
                return NotFound($"Dog with ID {dogId} not found");

            return Ok(dogToDelete);
        }

    }
}
