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
using Application.Exceptions;
using Serilog;

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
        public async Task<IActionResult> GetAllDogs(string? sortByBreed = null, int? sortByWeight = null)
        {
            try
            {
                var dogListResault = await _mediator.Send(new GetAllDogsQuery(sortByBreed, sortByWeight));

                Log.Information("Dog List found: {@dogListResault}", dogListResault);
                return Ok(dogListResault);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return BadRequest(ex.Message);
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
                {
                    Log.Warning($"Dog with Id {dogId} not found.");

                    throw new EntityNotFoundException("Dog", dogId);
                }

                Log.Information("Dog found: {@getDogResult}", getDogResult);
                return Ok(getDogResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Dog not found with Id {dogId}");

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
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
            try
            {
                Log.Information("Validating DogDto");

                var validatorResult = _dogValidator.Validate(newDog);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of DogDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(newDog, validationErrors);
                }

                Log.Information("DogDto validation successful. Adding new dog: {@newDog}", newDog);

                return Ok(await _mediator.Send(new AddDogCommand(newDog)));
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when adding dog");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing AddDogCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
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
            try
            {
                Log.Information($"Updating dog with ID {dogId}");

                var validatorResult = _dogValidator.Validate(updatedDog);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of DogDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(updatedDog, validationErrors);
                }

                var updatedDogResult = await _mediator.Send(new UpdateDogByIdCommand(updatedDog, dogId));

                if (updatedDogResult == null)
                {
                    Log.Warning("No dog found to update");

                    throw new EntityNotFoundException("Dog", dogId);
                }

                Log.Information("Successfully updated dog, updated dog: {@updatedDogResult}", updatedDogResult);
                return Ok(updatedDogResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Dog not found with Id {dogId} during update");

                return NotFound(ex.Message);
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when updating dog");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An unexpected error occurred while updating dog with ID {dogId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
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
                Log.Information($"Deleting dog with ID {dogId}");
                var dogToDelete = await _mediator.Send(new DeleteDogByIdCommand(dogId));

                if (dogToDelete == null)
                {
                    Log.Warning("No dog found to remove");

                    throw new EntityNotFoundException("Dog", dogId);
                }

                Log.Information("Successfully removed dog, removed dog: {@dogToDelete}", dogToDelete);
                return Ok(dogToDelete);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Dog not found with Id {dogId} during remove");

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
