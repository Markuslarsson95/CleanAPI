using Application.Commands.Dogs;
using Application.Commands.Dogs.UpdateDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Animals;
using Application.Validators.DogValidators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly DogValidator _dogValidator;
        public DogController(IMediator mediator, DogValidator dogValidator)
        {
            _mediator = mediator;
            _dogValidator = dogValidator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        [ProducesResponseType(typeof(List<Dog>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDogs()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllDogsQuery()));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            try
            {
                var getDogResult = await _mediator.Send(new GetDogByIdQuery(dogId));

                if (getDogResult == null)
                    return NotFound($"Dog with ID {dogId} not found");

                return Ok(getDogResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        [Authorize]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            var validatorResult = _dogValidator.Validate(newDog);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new AddDogCommand(newDog)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        [Authorize]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDogById([FromBody] DogDto updatedDog, Guid dogId)
        {
            var validatorResult = _dogValidator.Validate(updatedDog);

            if (!validatorResult.IsValid)
                return ValidationProblem(validatorResult.ToString());

            try
            {
                var updatedDogResult = await _mediator.Send(new UpdateDogByIdCommand(updatedDog, dogId));

                if (updatedDogResult == null)
                    return NotFound($"Dog with ID {dogId} not found");

                return Ok(updatedDogResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Delete dog by id
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDogById(Guid dogId)
        {
            try
            {
                var dogToDelete = await _mediator.Send(new DeleteDogByIdCommand(dogId));

                if (dogToDelete == null)
                    return NotFound($"Dog with ID {dogId} not found");

                return Ok(dogToDelete);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
